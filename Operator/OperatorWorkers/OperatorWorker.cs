using System.Collections.Generic;

namespace DADSTORM.Operator.OperatorWorkers {
    public abstract class OperatorWorker {

        internal Operator Op { get; private set; }

        public OperatorWorker(Operator op)
        {
            this.Op = op;
        }

        public abstract void execute();
        public abstract void processTuple(List<string> tuple);
    }
}
