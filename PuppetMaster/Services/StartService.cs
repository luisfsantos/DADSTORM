using DADSTORM.CommonTypes;
using DADSTORM.RemoteInterfaces;
using System.Collections.Generic;
using System.Net.Sockets;

namespace DADSTORM.PuppetMaster.Services {
    public class StartService : PuppetMasterService
    {
        private string OpID;

        public StartService(string opID)
        {
            this.OpID = opID;
        }

        public override void execute()
        {
            List<ICommands> replicas = PuppetMaster.Instance.GetOperator(OpID);
            foreach( ICommands replica in replicas) {
                try {
                    replica.start();
                } catch (SocketException e) {
                    //TODO: Error checking and verify if it should be removed from Downstream
                }
                
            }
            PuppetMaster.Instance.logger.notify(Command.START, new string[] { OpID });

        }
    }
}
