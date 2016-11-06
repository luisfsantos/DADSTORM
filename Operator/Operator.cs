using System;
using DADSTORM.RemoteInterfaces;
using DADSTORM.CommonTypes;
using DADSTORM.Operator.OperatorWorkers;
using DADSTORM.Operator.RoutingStrategy;

namespace DADSTORM.Operator {

    public class Operator : IOperator {
        public static readonly string PROGRAM_NAME = "Operator.exe";


        public Operator(string id, string[] upstream_addrs, string specName, string[] specParams, string routing, string logging, string semantics) {
            this.Id = id;
            defineRouting(routing);
            createWorker(specName, specParams);
            this.Logging = (LoggingLevel) Enum.Parse(typeof(LoggingLevel), logging);
            this.Semantics = (Semantics) Enum.Parse(typeof(Semantics), semantics);
            registerAtUpstreamOperators(upstream_addrs);
        }


        private void defineRouting(string routing) {
            throw new NotImplementedException();
        }

        private void createWorker(string workerName, string[] workerParams) {
            throw new NotImplementedException();
        }

        public string Id { get; set; }
        public OperatorWorker Worker { get; set; }
        public string[] SpecParams { get; set; }
        public Routing Routing { get; set; }
        public LoggingLevel Logging { get; set; }
        public Semantics Semantics { get; set; }


        public void send(string tuple) {
            throw new NotImplementedException();
        }

        
        public void registerAtUpstreamOperators(string [] addresses) {
            throw new NotImplementedException();
        }

        public void addDownstreamOperator() {
            throw new NotImplementedException();
        }
    }
}
