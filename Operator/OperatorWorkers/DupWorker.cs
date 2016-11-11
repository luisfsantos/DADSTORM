using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.Operator.OperatorWorkers
{
    class DupWorker : OperatorWorker {

        public DupWorker(Operator op) : base(op) {}

        public override void processTuple(List<string> tuple) {
            Op.addTupleToSend(tuple);
        }
    }
}
