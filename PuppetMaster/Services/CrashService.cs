﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.PuppetMaster.Services {
    public class CrashService : PuppetMasterService {
        private string OpId;
        private int Replica;

        public CrashService(string opId, int replica) {
            this.OpId = opId;
            this.Replica = replica;
        }

        public override void execute() {
            PuppetMaster.Instance.crash(OpId);
        }

    }
}
