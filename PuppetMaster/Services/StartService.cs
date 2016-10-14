using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.PuppetMaster.Services
{
    public class StartService : PuppetMasterService
    {
        private string op_id;

        public StartService(string op_id)
        {
            this.op_id = op_id;
        }

        public override void execute()
        {
            PuppetMaster.Instance.start(op_id);  
        }
    }
}
