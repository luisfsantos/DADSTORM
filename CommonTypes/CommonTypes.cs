using System;
using System.Collections.Generic;
using System.Linq;

namespace DADSTORM.CommonTypes {

    [Serializable]
    public class OperatorData { 
        public string Id { get; set; }
        public string[] Input_ops { get; set; }
        public int Rep_fact { get; set; }
        public string Routing { get; set; }
        public List<string> Addresses { get; set; }
        public OperatorSpecification OperatorSpec { get; set; }
    }

    [Serializable]
    public class OperatorSpecification {
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

    [Serializable]
    public enum Semantics { AtMostOnce, AtLeastOnce, ExactlyOnce }

    [Serializable]
    public enum LoggingLevel { Light, Full }

    public class Command {
        public const string START = "Start";
        public const string INTERVAL = "Interval";
        public const string STATUS = "Status";
        public const string CRASH = "Crash";
        public const string FREEZE = "Freeze";
        public const string UNFREEZE = "Unfreeze";
        public const string WAIT = "Wait";
    }
}
