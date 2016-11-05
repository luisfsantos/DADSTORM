using DADSTORM.CommonTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteInterfaces
{
    public interface IPCS
    {
        void startProcess(string id, List<string> upstream, OperatorSpecification specs, string routing, LoggingLevel level, Semantics semantics);
    }

    public class PCSConstants
    {
        public static readonly int PORT = 11050;
        public static readonly string NAME = "PCS";
    }
}
