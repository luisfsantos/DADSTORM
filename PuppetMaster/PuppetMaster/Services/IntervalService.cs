using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.PuppetMaster.Services
{
    public class IntervalService : PuppetMasterService
    {
        private string op_id;
        private int ms;

        public IntervalService(string op_id, int ms)
        {
            this.op_id = op_id;
            this.ms = ms;
        }

        public override void execute()
        {
            PuppetMaster.Instance.interval(op_id, ms);
        }
    }
}
