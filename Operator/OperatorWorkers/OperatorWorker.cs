using System.Collections.Generic;
using System.Threading;
using static DADSTORM.Operator.Operator;

namespace DADSTORM.Operator.OperatorWorkers {
    public abstract class OperatorWorker {

        internal Operator Op { get; private set; }

        public OperatorWorker(Operator op)
        {
            this.Op = op;
        }

        public void execute() {
            Tuple tupleToProcess;
            while(true) {
                Op.Frozen.WaitOne();
                if (Op.HasInterval) Thread.Sleep(Op.WaitTime);
                tupleToProcess = Op.getTupleToProcess();
                processTuple(tupleToProcess);
                
                Op.CurrentStatus.TupleProcessed();
            }
        }

        public abstract void processTuple(Tuple tup);
    }
}
