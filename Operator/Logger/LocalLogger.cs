using DADSTORM.RemoteInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.Operator.Logger {
    class LocalLogger : ILogger {

        public void sendInfo(string OpAddress, List<string> tuple) {
            //Log to file if possible
            Console.WriteLine("tuple " + OpAddress + ", <" + String.Join(", ", tuple.ToArray()) + ">");
        }
    }
}
