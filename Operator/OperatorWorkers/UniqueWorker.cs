using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.Operator.OperatorWorkers
{
    class UniqueWorker : OperatorWorker
    {

        internal HashSet<string> UniqueField = new HashSet<string>();
        internal int Field;


        public UniqueWorker (Operator op, int field) : base(op)
        {
            this.Field = field-1;
        }

        public override void processTuple(Tuple tup)
        {
            List<string> tuple = tup.tuple;
            //FIXME checkSize(tuple);
            if(!UniqueField.Contains(tuple[Field]))
            {
                UniqueField.Add(tuple[Field]);
                Op.addTupleToSend(tup.uuid, tuple);
            }
        }
    }
}
