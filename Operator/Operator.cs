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

namespace DADSTORM.Operator {

    public class Operator {

        private bool Frozen = false;
        internal int WaitTime { get; private set; }  = 0;
        internal bool HasInterval { get; private set; } = false;

        public string OperatorID { get; private set; }
        public int ReplIndex { get; private set; }
        public int ReplTotal { get; private set; }
        public string MyAddress { get; private set; }
        public OperatorWorker Worker { get; private set; }
        public string[] SpecParams { get; private set; }
        //public Routing Routing { get; private set; }

        public LoggingLevel Logging { get; private set; }
        public Semantics Semantics { get; private set; }

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
            this.Logging = (LoggingLevel)Enum.Parse(typeof(LoggingLevel), logging);
            this.Semantics = (Semantics)Enum.Parse(typeof(Semantics), semantics);
            createWorker(specName, specParams);
            registerAtUpstreamOperators(upstream_addrs, routing);
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
                if (HasInterval) Thread.Sleep(WaitTime);
                tupleToSend = outputStream.Take();

                foreach (KeyValuePair<string, List<IOperator>> downstreamPair in downstreamOperators) {
                    downstreamRouting[downstreamPair.Key]
                        .Route(downstreamPair.Value, tupleToSend)
                        .send(tupleToSend);
                }

                Console.WriteLine(String.Join(",", tupleToSend.ToArray()));
            }
        }

        internal void addTupleToSend(List<string> tuple)
        {
            outputStream.Add(tuple);
        }

        internal void addDownstreamOperator(string operator_id, int replicaIndex, IOperator op, string routing) {
            if (!downstreamOperators.ContainsKey(operator_id)) {
                downstreamOperators.Add(operator_id, new List<IOperator>());
            }
            downstreamOperators[operator_id].Insert(replicaIndex-1, op);
            downstreamRouting.Add(operator_id, Routing.GetInstance(routing));
        }

        internal void addTupleToProcess(List<string> tuple)
        {
            inputStream.Add(tuple);
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

        internal void freeze() {
            foreach(Thread thread in WorkingThreads) {
                thread.Suspend();
            }
        }

        internal void unfreeze() {
            foreach (Thread thread in WorkingThreads) {
                thread.Resume();
            }
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

        private void registerAtUpstreamOperators(string[] addresses, string myRouting) {
            string pattern = @"tcp://(?:[0-9]{1,3}\.){3}[0-9]{1,3}:\d{1,5}/op";
            Regex regex = new Regex(pattern);

            foreach (string address in addresses) {
                if (regex.IsMatch(address)) {
                    IOperator upstream = (IOperator)Activator.GetObject(typeof(IOperator), address);
                    upstream.addDownstreamOperator(MyAddress, myRouting);
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
