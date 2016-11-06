using DADSTORM.RemoteInterfaces;
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace DADSTORM.PCS {
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
