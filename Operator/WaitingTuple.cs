using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.Operator {
    class WaitingTuple {
        public List<string> tuple;
        public string uuid;
        public int retrys;
        private int maxcount = 3;
        private string operatorID;
        private int replicaID;

        public WaitingTuple(List<string> tuple, string uuid) {
            retrys = 0;
            this.tuple = tuple;
            this.uuid = uuid;
        }

        public WaitingTuple(List<string> tuple, string uuid, string operatorID, int replicaID) : this(tuple, uuid) {
            this.operatorID = operatorID;
            this.replicaID = replicaID;
        }

        // This method is called by the timer delegate.
        public void CheckStatus(Object stateInfo) {
            Operator op = (Operator)stateInfo;
            retrys += 1;
            if (retrys == maxcount) {
                op.removeSuspectOperator(operatorID, replicaID);
                //op.ackTuple(uuid);
                retrys = 0;
                op.retrySendTuple(tuple, uuid, operatorID, replicaID);
            } else {
                op.retrySendTuple(tuple, uuid, operatorID, replicaID);
            }
        }
    }
}

