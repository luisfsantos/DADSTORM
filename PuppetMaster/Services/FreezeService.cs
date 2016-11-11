using DADSTORM.CommonTypes;

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
            PuppetMaster.Instance.GetReplica(OpID, Replica).freeze();
            PuppetMaster.Instance.logger.notify(Command.FREEZE, new string[] { OpID, Replica.ToString() });
        }
    }
}
