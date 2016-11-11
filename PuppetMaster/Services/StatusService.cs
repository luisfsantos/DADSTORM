using DADSTORM.RemoteInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.PuppetMaster.Services
{
    public class StatusService : PuppetMasterService
    {
        public override void execute()
        {
            List<ICommands> allReplicas = PuppetMaster.Instance.GetAllOperators();
            foreach (ICommands replica in allReplicas) {
                replica.status();
            }
        }
    }
}
