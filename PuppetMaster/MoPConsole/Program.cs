using DADSTORM.PuppetMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoPConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PuppetMaster MoP = new PuppetMaster();
            MoP.start(10);
            MoP.crash("tcp://1.2.3.4:5650/OP");
            Console.ReadLine();
        }
    }
}
