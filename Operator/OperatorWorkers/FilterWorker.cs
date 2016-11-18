using System;
using System.Collections.Generic;

namespace DADSTORM.Operator.OperatorWorkers {
    class FilterWorker : OperatorWorker {

        private int Field;
        private string Condiditon;
        private string Value;

        public FilterWorker(Operator op, int field, string condition, string value) : base(op) {
            this.Field = field-1;
            this.Condiditon = condition.Trim('"');
            this.Value = value;
        }

        public override void processTuple(List<string> tuple) {
            int valueInt;
            int tupleInt;
            int compareResult;

            // parses string(9)=>"9" to int, does not parse string("9")=>"\"9\"" to int
            bool isInt = Int32.TryParse(Value, out valueInt);
            
            if (isInt) {
                tupleInt = Int32.Parse(tuple[Field]);
                compareResult = tupleInt.CompareTo(valueInt);
            } else {
                compareResult = tuple[Field].CompareTo(Value);
            }

            switch (Condiditon) {
                case "<":
                    if (compareResult < 0)
                        Op.addTupleToSend(tuple);
                    break;
                case ">":
                    if (compareResult > 0)
                        Op.addTupleToSend(tuple);
                    break;
                case "=":
                    if (compareResult == 0)
                        Op.addTupleToSend(tuple);
                    break;
                default:
                    throw new Exception("FILTER: condition must be >, < or =");
            }
        }
    }
}
