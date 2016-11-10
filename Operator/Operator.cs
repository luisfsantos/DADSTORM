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

        private bool frozen = false;
        private bool started = false;
        private int waittime = 0;

        public int ReplIndex { get; private set; }
        public int ReplTotal { get; private set; }
        public string Address { get; private set; }
        public OperatorWorker Worker { get; private set; }
        public string[] SpecParams { get; private set; }
        public Routing Routing { get; private set; }

        public LoggingLevel Logging { get; private set; }
        public Semantics Semantics { get; private set; }
        
        private List<IOperator> downstreamOperators = new List<IOperator>();

        internal BlockingCollection<List<string>> inputStream = new BlockingCollection<List<string>>(new ConcurrentQueue<List<string>>());
        private BlockingCollection<List<string>> outputStream = new BlockingCollection<List<string>>(new ConcurrentQueue<List<string>>());



        public Operator(int replIndex, int replTotal, string address, string[] upstream_addrs, string specName, string[] specParams, string routing, string logging, string semantics) {
            this.ReplIndex = replIndex;
            this.ReplTotal = replTotal;
            this.Routing = RoutingStrategy.Routing.GetInstance(routing);
            this.Logging = (LoggingLevel)Enum.Parse(typeof(LoggingLevel), logging);
            this.Semantics = (Semantics)Enum.Parse(typeof(Semantics), semantics);
            createWorker(specName, specParams);
            registerAtUpstreamOperators(upstream_addrs);
        }

        private void createWorker(string workerName, string[] workerParams) {
            //throw new NotImplementedException();
            switch (workerName) {
                case "UNIQ":
                    Worker = new UniqueWorker(this, Int32.Parse(workerParams[0]));
                    break;
                default:
                    Console.WriteLine("default");
                    break;
            }
        }

        private void send() {
            List<string> tupleToSend;
            while (true) {
                tupleToSend = outputStream.Take();
                //Routing.Route(downstreamOperators, tupleToSend).send(tupleToSend);
                Console.WriteLine(String.Join(",", tupleToSend.ToArray()));
            }
        }

        internal void addTupleToSend(List<string> tuple)
        {
            outputStream.Add(tuple);
        }

        internal void addDownstreamOperator(IOperator op) {
            downstreamOperators.Add(op);
        }

        internal void addTupleToProcess(List<string> tuple)
        {
            inputStream.Add(tuple);
        }

        internal List<string> getTupleToProcess() {
            return inputStream.Take();
        }

        public void run() {
            Thread processThread = new Thread(Worker.execute);
            processThread.Start();

            Thread sendThread = new Thread(send);
            sendThread.Start();

        }

        private void registerAtUpstreamOperators(string[] addresses) {
            string pattern = @"tcp://(?:[0-9]{1,3}\.){3}[0-9]{1,3}:\d{1,5}/op";
            Regex regex = new Regex(pattern);

            foreach (string address in addresses) {
                if (regex.IsMatch(address)) {
                    IOperator upstream = (IOperator)Activator.GetObject(typeof(IOperator), address);
                    upstream.addDownstreamOperator(Address);
                } else {
                    //FIXME need to handle the case when the address is actually a filename
                    TuplesReader tuplesReader = new TuplesReader(this, address, ReplIndex, ReplTotal);
                    Thread tuplesReaderThread = new Thread(tuplesReader.read);
                    //tuplesReaderThread.IsBackground = true;
                    tuplesReaderThread.Start();
                }

            }
        }
    }
}
