using DADSTORM.RemoteInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.PuppetMaster.Services
{
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
                replica.start();
            } 
        }
    }
}
