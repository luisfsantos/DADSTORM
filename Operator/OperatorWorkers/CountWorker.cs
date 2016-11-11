using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.Operator.OperatorWorkers
{
    class CountWorker : OperatorWorker {

        private int seenTuples;
        public CountWorker(Operator op) : base(op) {
            seenTuples = 0;
        }

        public override void processTuple(List<string> tuple) {
            seenTuples++;
            List<string> countTuple = new List<string>();
            countTuple.Add(seenTuples.ToString());
            Op.addTupleToSend(countTuple);
        }
    }
}
