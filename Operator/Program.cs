using System;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text.RegularExpressions;

namespace DADSTORM.Operator {
    class Program {
        static void Main(string[] args) {
            string replIndex = args[0],
                replTotal = args[1],
                address = args[2],
                upstreams = args[3],
                specName = args[4],
                specParamStr = args[5],
                routing = args[6],
                logging = args[7],
                semantics = args[8];

            string[] upstream_addrs = upstreams.Trim(new char[] { ']', '[' }).Split(',');
            string[] specParams = specParamStr.Trim(new char[] { ']', '[' }).Split(',');

            #region Get Port
            string pattern = @"tcp://(?:[0-9]{1,3}\.){3}[0-9]{1,3}:(?<port>\d{1,5})/op";
            int port = Int32.Parse(Regex.Match(address, pattern).Result("${port}"));
            #endregion

            TcpChannel channel = new TcpChannel(port);
            ChannelServices.RegisterChannel(channel, false);

            Operator Op = new Operator(Int32.Parse(replIndex), Int32.Parse(replTotal), address, upstream_addrs, specName, specParams, routing, logging, semantics);
            OperatorProxy OpProxy = new OperatorProxy(Op);
            Op.run();

            RemotingServices.Marshal(OpProxy,"op",
                                    typeof(OperatorProxy));

            Console.ReadLine();
            ChannelServices.UnregisterChannel(channel);
            Process.GetCurrentProcess().CloseMainWindow();
            Process.GetCurrentProcess().Close();
        }
    }
}
