using DADSTORM.CommonTypes;
using System.Net.Sockets;

namespace DADSTORM.PuppetMaster.Services {
    public class FreezeService : PuppetMasterService {
        private string OpID;
        private int Replica;

        public FreezeService(string opId, int replica)
        {
            this.OpID = opId;
            this.Replica = replica;
        }

        public override void execute() {
            try {
                PuppetMaster.Instance.GetReplica(OpID, Replica).freeze(); 
            } catch (SocketException e) {
                //TODO: Error checking and verify if it should be removed from Downstream
            }
            PuppetMaster.Instance.logger.notify(Command.FREEZE, new string[] { OpID, Replica.ToString() });

        }
    }
}
