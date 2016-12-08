using System.Collections.Generic;

namespace DADSTORM.RemoteInterfaces {
    public interface IReplica {
        void sendProcessed(string uuid);
    }
}
