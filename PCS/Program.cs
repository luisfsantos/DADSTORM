using RemoteInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace PCS
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpChannel channel = new TcpChannel(PCSConstants.PORT);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(
                                    typeof(PCSProxy),
                                    PCSConstants.NAME,
                                    WellKnownObjectMode.Singleton);
            Console.WriteLine("Press anykey to shutdown the Process Creation Service...");
            Console.ReadLine();
        }
    }
}
