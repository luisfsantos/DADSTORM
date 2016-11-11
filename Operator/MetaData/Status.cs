using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.Operator.MetaData {
    public class Status {
        string OperatorName;

        Dictionary<string, int> presumedDead = new Dictionary<string, int>();
        int tuplesProcessed = 0;
        int tuplesSent = 0;
        int tuplesRecieved = 0;

        public Status(string operatorName) {
            this.OperatorName = operatorName;
        }

        public void TupleProcessed () {
            tuplesProcessed++;
        }

        public void TupleSent() {
            tuplesSent++;
        }

        public void TupleRecieved() {
            tuplesRecieved++;
        }

        public void PresumedDead(string Op) {
            int currentCount;
            presumedDead.TryGetValue(Op, out currentCount);
            presumedDead[Op] = currentCount + 1;
        }

        public string ConstructStatus() {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} has recieved {1} tuples of which it has processed {2} and sent {3}.\n", OperatorName, tuplesRecieved, tuplesProcessed, tuplesSent);
            foreach(KeyValuePair<string, int> ReplicaPresumedDead in presumedDead) {
                sb.AppendFormat("{0} has {1} replica(s) that are presumed dead.\n", ReplicaPresumedDead.Key, ReplicaPresumedDead.Value);
            }
            return sb.ToString();
        }
    }
}
