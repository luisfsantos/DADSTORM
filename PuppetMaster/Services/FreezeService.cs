using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.PuppetMaster.Services {
    public class FreezeService : PuppetMasterService {
        private string process;

        public FreezeService(string process) {
            this.process = process;
        }

        public override void execute() {
            PuppetMaster.Instance.freeze(process);
        }
    }
}
