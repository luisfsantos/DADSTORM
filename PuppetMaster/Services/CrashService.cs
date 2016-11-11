using DADSTORM.CommonTypes;

namespace DADSTORM.PuppetMaster.Services {
    public class CrashService : PuppetMasterService {
        private string OpID;
        private int Replica;

        public CrashService(string opId, int replica) {
            this.OpID = opId;
            this.Replica = replica;
        }

        public override void execute() {
            PuppetMaster.Instance.GetReplica(OpID, Replica).crash();
            PuppetMaster.Instance.logger.notify(Command.CRASH, new string[] { OpID, Replica.ToString() });
        }

    }
}
