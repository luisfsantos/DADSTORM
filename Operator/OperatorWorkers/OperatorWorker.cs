using System.Collections.Generic;

namespace DADSTORM.Operator.OperatorWorkers {
    public abstract class OperatorWorker {

        internal Operator Op { get; private set; }

        public OperatorWorker(Operator op)
        {
            this.Op = op;
        }

        public void execute() {
            List<string> tupleToProcess;
            while(true) {
                tupleToProcess = Op.getTupleToProcess();
                processTuple(tupleToProcess);
            }
        }

        public abstract void processTuple(List<string> tuple);
    }
}
