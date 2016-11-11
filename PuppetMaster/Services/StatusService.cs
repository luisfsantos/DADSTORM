using DADSTORM.CommonTypes;
using DADSTORM.RemoteInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
                try {
                    replica.status();
                } catch (SocketException e) {
                    //TODO: Error checking and verify if it should be removed from Downstream
                }
            }
            PuppetMaster.Instance.logger.notify(Command.STATUS, new string[] { });
        }
    }
}
