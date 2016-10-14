using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.PuppetMaster.Services {
    public class CrashService : PuppetMasterService {
        private string process;

        public CrashService(string process) {
            this.process = process;
        }

        public override void execute() {
            PuppetMaster.Instance.crash(process);
        }

    }
}
