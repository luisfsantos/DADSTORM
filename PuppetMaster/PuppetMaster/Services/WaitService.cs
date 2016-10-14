using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.PuppetMaster.Services
{
    public class WaitService : PuppetMasterService
    {
        private int ms;

        public WaitService(int ms)
        {
            this.ms = ms;
        }

        public override void execute()
        {
            PuppetMaster.Instance.wait(ms);
        }

        public new void assyncexecute()
        {
            throw new NotImplementedException();
        }
    }
}
