using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.CommonTypes {

    public class OperatorData { //FIXME will change when we design the operators
        public string Id { get; set; }
        public string[] Input_ops { get; set; }
        public int Rep_fact { get; set; }
        public Routing Routing { get; set; }
        public string[] Addresses { get; set; }
        public OperatorSpec Operator_spec { get; set; }
    }

    public class OperatorSpec { //FIXME will change when we design the operators
        private string name;
        public string Name {
            get { return name; }
            set {
                string[] valid = { "UNIQ", "COUNT", "DUP", "FILTER", "CUSTOM" };
                if (valid.Contains(value))
                    name = value;
            }
        }
        public string[] Params { get; set; }
    }

    public enum Routing { Primary, Random, Hashing } //FIXME routing will be a class, Strategy pattern
    public enum Semantics { AtMostOnce, AtLeastOnce, ExactlyOnce }
    public enum LoggingLevel { Light, Full }
}
