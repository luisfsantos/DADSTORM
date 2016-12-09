using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.Operator.OperatorWorkers
{
    class CountWorker : OperatorWorker {

        internal int seenTuples;
        public CountWorker(Operator op) : base(op) {
            seenTuples = 0;
        }

        public override void processTuple(Tuple tup) {
            seenTuples++;
            List<string> countTuple = new List<string>();
            countTuple.Add(seenTuples.ToString());
            Op.addTupleToSend(tup.uuid, countTuple);
        }
    }
}
