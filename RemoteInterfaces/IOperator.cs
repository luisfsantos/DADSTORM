using System.Collections.Generic;

namespace DADSTORM.RemoteInterfaces {
    public interface IOperator
    {
        void send(List<string> tuple);
        void addDownstreamOperator(string address, string routing, string DownstreamID, int replica);
        void ack(string uuid);
    }
}
