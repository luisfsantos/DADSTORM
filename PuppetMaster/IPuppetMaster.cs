using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.PuppetMaster
{
    interface IPuppetMaster
    {
        void start(string op_id);

        void interval(string op_id, int ms);

        void status();

        void crash(string process);

        void freeze(string process);

        void unfreeze(string process);

        void wait(int ms);
    }
}
