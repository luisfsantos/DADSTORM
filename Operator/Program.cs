using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text.RegularExpressions;

namespace DADSTORM.Operator {
    class Program {
        static void Main(string[] args) {
            string id = args[0],
                address = args[1],
                upstreams = args[2],
                specName = args[3],
                specParamStr = args[4],
                routing = args[5],
                logging = args[6],
                semantics = args[7];

            string[] upstream_addrs = upstreams.Trim(new char[] { ']', '[' }).Split(',');
            string[] specParams = specParamStr.Trim(new char[] { ']', '[' }).Split(',');
            Operator Op = new Operator(id, upstream_addrs, specName, specParams, routing, logging, semantics);
            OperatorProxy OpProxy = new OperatorProxy(Op);

            #region Get Port
            string pattern = @"tcp://(?:[0-9]{1,3}\.){3}[0-9]{1,3}:(?<port>\d{1,5})/op";
            int port = Int32.Parse(Regex.Match(address, pattern).Result("${port}"));
            #endregion

            TcpChannel channel = new TcpChannel(port);
            ChannelServices.RegisterChannel(channel, false);
            RemotingServices.Marshal(OpProxy,"op",
                                    typeof(OperatorProxy));

            Console.ReadLine();
            ChannelServices.UnregisterChannel(channel);
        }
    }
}
