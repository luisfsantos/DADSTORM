using System.Collections.Generic;
using System.Threading;

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
                Op.Frozen.WaitOne();
                if (Op.HasInterval) Thread.Sleep(Op.WaitTime);
                tupleToProcess = Op.getTupleToProcess();
                processTuple(tupleToProcess);
                
                Op.CurrentStatus.TupleProcessed();
            }
        }

        public abstract void processTuple(List<string> tuple);
    }
}
