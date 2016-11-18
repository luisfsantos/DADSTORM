using System;
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

namespace DADSTORM.Operator {

    public class Operator {

        internal int WaitTime { get; private set; }  = 0;
        internal bool HasInterval { get; private set; } = false;
        internal ManualResetEvent Frozen { get; private set; } = new ManualResetEvent(true);

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


        public LoggingLevel Logging { get; private set; }
        public Semantics Semantics { get; private set; }

        internal ILogger Logger = new LocalLogger();

        private List<Thread> WorkingThreads = new List<Thread>();
        
        internal Dictionary<string, List<IOperator>> downstreamOperators = new Dictionary<string, List<IOperator>>();
        private Dictionary<string, Routing> downstreamRouting = new Dictionary<string, Routing>();

        internal BlockingCollection<List<string>> inputStream = new BlockingCollection<List<string>>(new ConcurrentQueue<List<string>>());
        private BlockingCollection<List<string>> outputStream = new BlockingCollection<List<string>>(new ConcurrentQueue<List<string>>());



        public Operator(string operatorID, int replIndex, int replTotal, string address, string[] upstream_addrs, string specName, string[] specParams, string routing, string logging, string semantics) {
            this.OperatorID = operatorID;
            this.ReplIndex = replIndex;
            this.ReplTotal = replTotal;
            this.MyAddress = address;
            CurrentStatus = new Status(address);
            this.MyRouting = Routing.GetInstance(routing);
            this.Logging = (LoggingLevel)Enum.Parse(typeof(LoggingLevel), logging);
            this.Semantics = (Semantics)Enum.Parse(typeof(Semantics), semantics);
            createWorker(specName, specParams);
            this.routing = routing;
            this.upstream_addrs = upstream_addrs;
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
                tupleToSend = outputStream.Take();
                #region Logger
                if (Logging.Equals(LoggingLevel.Full)) Logger.sendInfo(MyAddress, tupleToSend);
                #endregion
                foreach (KeyValuePair<string, List<IOperator>> downstreamPair in downstreamOperators) {
                    try {

                        downstreamPair.Value
                            [downstreamRouting[downstreamPair.Key]
                                .Route(downstreamPair.Value.Count, tupleToSend)]
                            .send(tupleToSend);
                        CurrentStatus.TupleSent();

                    } catch (SocketException e) {
                        CurrentStatus.PresumedDead(downstreamPair.Key);
                        //TODO: Error checking and verify if it should be removed from Downstream
                    }
                }
                if (downstreamOperators.Count == 0) {
                    sendToOutputOperator(tupleToSend);
                }
            }
        }

        private void sendToOutputOperator(List<string> tuple) {
            Thread thread = new Thread(() => OutputOperator.writeToFile(tuple));
            thread.Start();
            thread.Join();
        }

        internal void addTupleToSend(List<string> tuple)
        {
            outputStream.Add(tuple);
        }

        internal void addDownstreamOperator(string operator_id, int replicaIndex, IOperator op, string routing) {
            if (!downstreamOperators.ContainsKey(operator_id)) {
                downstreamOperators.Add(operator_id, new List<IOperator>());
            }
            downstreamOperators[operator_id].Add(op);
            downstreamOperators[operator_id].Sort(
                (op1,op2) => ((OperatorProxy)op1).ReplIndex.CompareTo(((OperatorProxy)op2).ReplIndex));
            if (!downstreamRouting.ContainsKey(operator_id)) {
                downstreamRouting.Add(operator_id, Routing.GetInstance(routing));
            }
        }

        internal void addTupleToProcess(List<string> tuple)
        {
            inputStream.Add(tuple);
            CurrentStatus.TupleRecieved();
        }

        internal List<string> getTupleToProcess() {
            return inputStream.Take();
        }

        #region Commands

        public void run() {
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

        public void registerAtUpstreamOperators(string[] addresses, string myRouting) {
            string pattern = @"tcp://(?:[0-9]{1,3}\.){3}[0-9]{1,3}:\d{1,5}/op";
            Regex regex = new Regex(pattern);

            //Thread.Sleep(5000);
            foreach (string address in addresses) {
                if (regex.IsMatch(address)) {
                    IOperator upstream = (IOperator)Activator.GetObject(typeof(IOperator), address);
                    upstream.addDownstreamOperator(MyAddress, myRouting, OperatorID, ReplIndex);
                } else {
                    TuplesReader tuplesReader = new TuplesReader(this, address /*address is path*/, ReplIndex, ReplTotal);
                    Thread tuplesReaderThread = new Thread(tuplesReader.read);
                    //tuplesReaderThread.IsBackground = true;
                    tuplesReaderThread.Start();
                }

            }
        }
    }
}
