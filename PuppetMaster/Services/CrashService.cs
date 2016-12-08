using DADSTORM.CommonTypes;
using System.Net.Sockets;

namespace DADSTORM.PuppetMaster.Services {
    public class CrashService : PuppetMasterService {
        private string OpID;
        private int Replica;

        public CrashService(string opId, int replica) {
            this.OpID = opId;
            this.Replica = replica;
        }

        public override void execute() {
            try {
                PuppetMaster.Instance.GetReplica(OpID, Replica).crash();
            } catch (SocketException e) {
                //TODO: Error checking and verify if it should be removed from Downstream
            }
            
            PuppetMaster.Instance.logger.notify(Command.CRASH, new string[] { OpID, Replica.ToString() });
        }

    }
}
