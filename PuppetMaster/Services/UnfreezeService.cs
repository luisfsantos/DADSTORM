using DADSTORM.CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.PuppetMaster.Services
{
    public class UnfreezeService : PuppetMasterService
    {
        private string OpID;
        private int Replica;

        public UnfreezeService(string opId, int replica)
        {
            this.OpID = opId;
            this.Replica = replica;
        }

        public override void execute()
        {
            try {
                PuppetMaster.Instance.GetReplica(OpID, Replica).unfreeze();
            } catch (SocketException e) {
                //TODO: Error checking and verify if it should be removed from Downstream
            }
            PuppetMaster.Instance.logger.notify(Command.UNFREEZE, new string[] { OpID, Replica.ToString() });
        }
    }
}
