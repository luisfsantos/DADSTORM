using System.Collections.Generic;

namespace DADSTORM.RemoteInterfaces {
    public interface IReplica {
        void sendProcessed(List<string> tuple, string uuid);
    }
}
