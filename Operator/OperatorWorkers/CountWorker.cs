using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.Operator.OperatorWorkers
{
    class CountWorker : OperatorWorker {

        public CountWorker(Operator op) : base(op) {

        }

        public override void processTuple(List<string> tuple) {
            //throw new NotImplementedException();
        }
    }
}
