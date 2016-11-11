using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.PuppetMaster.Services
{
    public class UnfreezeService : PuppetMasterService
    {
        private string OpId;
        private int Replica;

        public UnfreezeService(string opId, int replica)
        {
            this.OpId = opId;
            this.Replica = replica;
        }

        public override void execute()
        {
            PuppetMaster.Instance.GetReplica(OpId, Replica).unfreeze();
        }
    }
}
