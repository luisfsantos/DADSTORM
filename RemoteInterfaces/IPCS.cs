using DADSTORM.CommonTypes;
using System.Collections.Generic;

namespace DADSTORM.RemoteInterfaces {
    public interface IPCS
    {
        void startProcess(int replIndex, int replTotal, string address, List<string> upstream, OperatorSpecification specs, string routing, LoggingLevel level, Semantics semantics);
    }

    public class PCSConstants
    {
        public static readonly int PORT = 11050;
        public static readonly string NAME = "PCS";
    }
}
