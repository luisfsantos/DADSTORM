using System.Collections.Generic;

namespace DADSTORM.RemoteInterfaces {
    public delegate void sendAsync(List<string> tuple, string uuid, string address);
    public delegate void ackAsync(string uuid);
    public interface IOperator
    {
        void send(List<string> tuple, string uuid, string address);
        void addDownstreamOperator(string address, string routing, string DownstreamID, int replica);
        void ack(string uuid);
    }
}
