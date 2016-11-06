using System;
using DADSTORM.RemoteInterfaces;
using DADSTORM.CommonTypes;
using DADSTORM.Operator.OperatorWorkers;
using DADSTORM.Operator.RoutingStrategy;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace DADSTORM.Operator {

    public class Operator {

        private bool frozen = false;
        private bool started = false;
        private int waittime = 0;

        public string Id { get; set; }
        public OperatorWorker Worker { get; set; }
        public string[] SpecParams { get; set; }
        public Routing Routing { get; set; }
        public LoggingLevel Logging { get; set; }
        public Semantics Semantics { get; set; }

        private List<IOperator> downstreamOperators = new List<IOperator>();

        private ConcurrentQueue<List<string>> inputStream = new ConcurrentQueue<List<string>>();
        private ConcurrentQueue<List<string>> outputStream = new ConcurrentQueue<List<string>>(); //TODO check if it necessary to maintain output stream


        public Operator(string id, string[] upstream_addrs, string specName, string[] specParams, string routing, string logging, string semantics) {
            this.Id = id;
            this.Routing = RoutingStrategy.Routing.GetInstance(routing);
            this.Logging = (LoggingLevel) Enum.Parse(typeof(LoggingLevel), logging);
            this.Semantics = (Semantics) Enum.Parse(typeof(Semantics), semantics);
            createWorker(specName, specParams);
            registerAtUpstreamOperators(upstream_addrs);
        }

        private void createWorker(string workerName, string[] workerParams) {
            //throw new NotImplementedException();
        }

        private void send(List<string> tuple) {
            Routing.Route(downstreamOperators, tuple).send(tuple);
        }

        public void run()
        {

        }
        
        private void registerAtUpstreamOperators(string [] addresses) {
           // throw new NotImplementedException();
        }
    }
}
