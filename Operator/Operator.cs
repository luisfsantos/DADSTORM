using System;
using DADSTORM.RemoteInterfaces;
using DADSTORM.CommonTypes;
using DADSTORM.Operator.OperatorWorkers;
using DADSTORM.Operator.RoutingStrategy;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.Threading;

namespace DADSTORM.Operator {

    public class Operator {

        private bool frozen = false;
        private bool started = false;
        private int waittime = 0;

        public string Id { get; private set; }
        public string Address { get; private set; }
        public OperatorWorker Worker { get; private set; }
        public string[] SpecParams { get; private set; }
        public Routing Routing { get; private set; }

        public LoggingLevel Logging { get; private set; }
        public Semantics Semantics { get; private set; }
        
        private List<IOperator> downstreamOperators = new List<IOperator>();

        internal ConcurrentQueue<List<string>> inputStream = new ConcurrentQueue<List<string>>();
        private ConcurrentQueue<List<string>> outputStream = new ConcurrentQueue<List<string>>(); //TODO check if it necessary to maintain output stream


        public Operator(string id, string address, string[] upstream_addrs, string specName, string[] specParams, string routing, string logging, string semantics) {
            this.Id = id;
            this.Routing = RoutingStrategy.Routing.GetInstance(routing);
            this.Logging = (LoggingLevel)Enum.Parse(typeof(LoggingLevel), logging);
            this.Semantics = (Semantics)Enum.Parse(typeof(Semantics), semantics);
            createWorker(specName, specParams);
            registerAtUpstreamOperators(upstream_addrs);
        }

        private void createWorker(string workerName, string[] workerParams) {
            //throw new NotImplementedException();
        }

        private void send() {
            List<string> tupleToSend;
            while (true) {
                outputStream.TryDequeue(out tupleToSend);
                Routing.Route(downstreamOperators, tupleToSend).send(tupleToSend);
            }
        }

        internal void addToSend(List<string> tuple)
        {
            outputStream.Enqueue(tuple);
        }

        internal void addDownstreamOperator(IOperator op) {
            downstreamOperators.Add(op);
        }

        internal void addToProcess(List<string> tuple)
        {
            inputStream.Enqueue(tuple);
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
                }

            }
        }
    }
}
