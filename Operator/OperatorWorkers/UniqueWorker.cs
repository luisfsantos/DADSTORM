using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.Operator.OperatorWorkers
{
    class UniqueWorker : OperatorWorker
    {

        private HashSet<string> UniqueField = new HashSet<string>();
        private int Field;


        public UniqueWorker (Operator op, int field) : base(op)
        {
            this.Field = field;
        }

        public override void processTuple(List<string> tuple)
        {
            //FIXME checkSize(tuple);
            if(!UniqueField.Contains(tuple[Field]))
            {
                UniqueField.Add(tuple[Field]);
                Op.addTupleToSend(tuple);
            }
        }
    }
}
