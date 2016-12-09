﻿using System;
using DADSTORM.RemoteInterfaces;
using DADSTORM.CommonTypes;
using DADSTORM.Operator.OperatorWorkers;
using DADSTORM.Operator.RoutingStrategy;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.Threading;
using DADSTORM.Operator.FileReader;
using DADSTORM.Operator.Logger;
using System.Net.Sockets;
using DADSTORM.Operator.MetaData;
using System.Linq;

namespace DADSTORM.Operator {
    

    public class Operator {

        internal int WaitTime { get; private set; }  = 0;
        internal bool HasInterval { get; private set; } = false;
        internal ManualResetEvent Frozen { get; private set; } = new ManualResetEvent(true);
        internal ManualResetEvent noDownstream { get; private set; } = new ManualResetEvent(false);

        internal Status CurrentStatus;


        public string OperatorID { get; private set; }

        public int ReplIndex { get; private set; }
        public int ReplTotal { get; private set; }
        public string MyAddress { get; private set; }

        public OperatorWorker Worker { get; private set; }
        public string[] SpecParams { get; private set; }
        public Routing MyRouting { get; private set; }
        public string routing { get; private set; }
        public string[] upstream_addrs { get; set; }
        public string[] replica_addrs { get; set; }


        public LoggingLevel Logging { get; private set; }
        public Semantics ExecutionSemantics { get; private set; }

        internal ILogger Logger = new LocalLogger();

        private List<Thread> WorkingThreads = new List<Thread>();
        private List<IReplica> Replicas = new List<IReplica>();
        private HashSet<string> ProcessedTuples = new HashSet<string>();
        
        internal Dictionary<string, SortedList<int, IOperator>> DownstreamOperators = new Dictionary<string, SortedList<int, IOperator>>();
        internal Dictionary<string,IOperator> UpstreamOperators = new Dictionary<string, IOperator>();
        private Dictionary<string, Routing> downstreamRouting = new Dictionary<string, Routing>();
        private Dictionary<string, Timer> tuplesWaiting = new Dictionary<string, Timer>();

        internal BlockingCollection<List<string>> inputStream = new BlockingCollection<List<string>>(new ConcurrentQueue<List<string>>());
        private BlockingCollection<List<string>> outputStream = new BlockingCollection<List<string>>(new ConcurrentQueue<List<string>>());

        public Operator(string operatorID, int replIndex, int replTotal, string address, string[] upstream_addrs, string[] replica_addrs, string specName, string[] specParams, string routing, string logging, string semantics) {
            this.OperatorID = operatorID;
            this.ReplIndex = replIndex;
            this.ReplTotal = replTotal;
            this.MyAddress = address;
            CurrentStatus = new Status(address);
            this.MyRouting = Routing.GetInstance(routing);
            this.Logging = (LoggingLevel)Enum.Parse(typeof(LoggingLevel), logging);
            this.ExecutionSemantics = (Semantics)Enum.Parse(typeof(Semantics), semantics);
            createWorker(specName, specParams);
            this.routing = routing;
            this.upstream_addrs = upstream_addrs;
            this.replica_addrs = replica_addrs;
        }

        private void createWorker(string workerName, string[] workerParams) {
            switch (workerName) {
                case "UNIQ":
                    Worker = new UniqueWorker(this, Int32.Parse(workerParams[0]));
                    break;
                case "COUNT":
                    Worker = new CountWorker(this);
                    break;
                case "DUP":
                    Worker = new DupWorker(this);
                    break;
                case "FILTER":
                    Worker = new FilterWorker(this, Int32.Parse(workerParams[0]), workerParams[1], workerParams[2]);
                    break;
                case "CUSTOM":
                    Worker = new CustomWorker(this, workerParams[0], workerParams[1], workerParams[2]);
                    break;
                default:
                    throw new Exception("createWorker: no worker specified");
            }
        }

        private void send() {
            List<string> tupleToSend;
            while (true) {
                #region DEBUG
                Frozen.WaitOne();
                if (HasInterval) Thread.Sleep(WaitTime);
                #endregion
                noDownstream.WaitOne();
                tupleToSend = outputStream.Take();
                bool sent = false;
                foreach (KeyValuePair<string, SortedList<int, IOperator>> downstreamPair in DownstreamOperators) {
                    string uuid = generateID();
                    do {
                        int replica = downstreamRouting[downstreamPair.Key].Route(downstreamPair.Value.Count, tupleToSend);
                        try {
                            KeyValuePair<int, IOperator> operatorToSend = downstreamPair.Value.ElementAt(replica);
                            operatorToSend.Value
                                .send(tupleToSend, uuid);
                            #region at-least-once and exactly-once processing
                            if (ExecutionSemantics != Semantics.AtMostOnce) {
                                addToWaitingForAck(tupleToSend, uuid, downstreamPair.Key, operatorToSend.Key);
                            }
                            #endregion
                            CurrentStatus.TupleSent();
                            sent = true;
                        } catch (SocketException) {
                            removeSuspectOperator(downstreamPair.Key, downstreamPair.Value.ElementAt(replica).Key);
                        }
                    } while (!sent && ExecutionSemantics != Semantics.AtMostOnce);
                }
                #region Logger
                if (Logging.Equals(LoggingLevel.Full))
                    Logger.sendInfo(MyAddress, tupleToSend);
                #endregion
            }
        }

        internal void removeSuspectOperator(string operatorID, int replicaID) {
            DownstreamOperators[operatorID].Remove(replicaID);
            CurrentStatus.PresumedDead(operatorID);
            if (DownstreamOperators[operatorID].Count == 0) {
                noDownstream.Reset();
            }
        }

        internal void retrySendTuple(List<string> tuple, string uuid, string operatorID, int replicaID) {
            bool sent = false;
            noDownstream.WaitOne();
            int replica = DownstreamOperators[operatorID].IndexOfKey(replicaID) >= 0 ? DownstreamOperators[operatorID].IndexOfKey(replicaID): downstreamRouting[operatorID].Route(DownstreamOperators[operatorID].Count, tuple);
            
            do {
                
                try {
                    KeyValuePair<int, IOperator> operatorToSend = DownstreamOperators[operatorID].ElementAt(replica);
                    operatorToSend.Value
                        .send(tuple, uuid);
                    CurrentStatus.TupleSent();
                    sent = true;
                } catch (SocketException) {
                    CurrentStatus.PresumedDead(operatorID);
                    DownstreamOperators[operatorID].RemoveAt(replica);
                    replica = downstreamRouting[operatorID].Route(DownstreamOperators[operatorID].Count, tuple);
                }
            } while (!sent && ExecutionSemantics != Semantics.AtMostOnce);
        }

        private void addToWaitingForAck(List<string> tupleToSend, string uuid, string operatorID, int replicaID) {
            WaitingTuple wt = new WaitingTuple(tupleToSend, uuid, operatorID, replicaID);
            tuplesWaiting.Add(uuid, new Timer(wt.CheckStatus, this,
                                   30000, 30000));
        }

        internal void ackTuple(string uuid) {
            tuplesWaiting[uuid].Dispose();
            tuplesWaiting.Remove(uuid);
        }

        internal void addTupleToSend(List<string> tuple)
        {
            outputStream.Add(tuple);
        }

        internal void addDownstreamOperator(string operator_id, int replicaIndex, IOperator op, string routing) {
            if (!DownstreamOperators.ContainsKey(operator_id)) {
                DownstreamOperators.Add(operator_id, new SortedList<int, IOperator>());
            }
            if (!DownstreamOperators[operator_id].ContainsKey(((OperatorProxy)op).ReplIndex)) {
                DownstreamOperators[operator_id].Add(((OperatorProxy)op).ReplIndex, op);
                noDownstream.Set();
            }
            if (!downstreamRouting.ContainsKey(operator_id)) {
                downstreamRouting.Add(operator_id, Routing.GetInstance(routing));
            }
            
        }

        internal void addProcessed(string uuid) {
            ProcessedTuples.Add(uuid);
        }

        internal void addTupleToProcess(List<string> tuple, string uuid, string upstream_addr)
        {
            if (ExecutionSemantics == Semantics.ExactlyOnce) {
                //check if this has been processed before
                inputStream.Add(tuple);
                CurrentStatus.TupleRecieved();
            } else {
                //need to only ack if all the tuples relating to this one have been sent!
                inputStream.Add(tuple);
                CurrentStatus.TupleRecieved();
                //if (upstream_addr != null)
                //    UpstreamOperators[upstream_addr].ack(uuid);
            }
            
        }

        public string generateID() {
            return Guid.NewGuid().ToString("N");
        }

        internal List<string> getTupleToProcess() {
            return inputStream.Take();
        }

        #region Commands

        public void run() {
            registerAtUpstreamOperators(upstream_addrs, routing);
            if (ExecutionSemantics == Semantics.ExactlyOnce) {
                comunicateWithReplicas(replica_addrs);
            }
            Thread processThread = new Thread(Worker.execute);
            WorkingThreads.Add(processThread);
            processThread.Start();

            Thread sendThread = new Thread(send);
            WorkingThreads.Add(sendThread);
            sendThread.Start();
        }

        internal void interval(int ms) {
            if (ms <= 0) {
                HasInterval = false;
            } else {
                HasInterval = true;
                WaitTime = ms;
            }
        }

        #endregion

        private void comunicateWithReplicas(string[] addresses) {
            string pattern = @"tcp://(?:[0-9]{1,3}\.){3}[0-9]{1,3}:\d{1,5}/op";
            Regex regex = new Regex(pattern);

            foreach (string address in addresses) {
                if (regex.IsMatch(address) && !address.Equals(MyAddress)) {
                    Replicas.Add((IReplica)Activator.GetObject(typeof(IReplica), address));
                }
            }
        }


        public void registerAtUpstreamOperators(string[] addresses, string myRouting) {
            string pattern = @"tcp://(?:[0-9]{1,3}\.){3}[0-9]{1,3}:\d{1,5}/op";
            Regex regex = new Regex(pattern);

            foreach (string address in addresses) {
                if (regex.IsMatch(address)) {
                    IOperator upstream = (IOperator)Activator.GetObject(typeof(IOperator), address);
                    UpstreamOperators.Add(address, upstream);
                    upstream.addDownstreamOperator(MyAddress, myRouting, OperatorID, ReplIndex);
                } else {
                    TuplesReader tuplesReader = new TuplesReader(this, address /*address is path*/, ReplIndex, ReplTotal);
                    Thread tuplesReaderThread = new Thread(tuplesReader.read);
                    tuplesReaderThread.Start();
                }

            }
        }
    }
}
