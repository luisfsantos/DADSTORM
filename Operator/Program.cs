﻿using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using System.Text.RegularExpressions;

namespace DADSTORM.Operator {
    class Program {
        static void Main(string[] args) {
            string operatorID = args[0],
                replIndex = args[1],
                replTotal = args[2],
                address = args[3],
                upstreams = args[4],
                replicas = args[5],
                specName = args[6],
                specParamStr = args[7],
                routing = args[8],
                logging = args[9],
                semantics = args[10];

            string[] upstream_addrs = upstreams.Trim(new char[] { ']', '[' }).Split(',');
            string[] replica_addrs = replicas.Trim(new char[] { ']', '[' }).Split(',');
            string[] specParams = specParamStr.Trim(new char[] { ']', '[' }).Split(',');
            //Debugger.Launch();
            Console.Title = "Operator: " + operatorID + " & Replica " + replIndex + " of " + replTotal;

            #region Get Port
            string pattern = @"tcp://(?:[0-9]{1,3}\.){3}[0-9]{1,3}:(?<port>\d{1,5})/op";
            int port = Int32.Parse(Regex.Match(address, pattern).Result("${port}"));
            #endregion
            BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
            provider.TypeFilterLevel = TypeFilterLevel.Full;
            IDictionary props = new Hashtable();
            props["port"] = port;
            TcpChannel channel = new TcpChannel(props, null, provider);
            ChannelServices.RegisterChannel(channel, false);

            Operator Op = new Operator(operatorID, 
                Int32.Parse(replIndex), 
                Int32.Parse(replTotal), 
                address, upstream_addrs, replica_addrs, 
                specName, specParams, 
                routing, logging, 
                semantics);

            OperatorProxy OpProxy = new OperatorProxy(Op);
            //TODO done in the PuppetMaster Op.run();

            RemotingServices.Marshal(OpProxy,"op",
                                    typeof(OperatorProxy));


            Console.ReadLine();
            ChannelServices.UnregisterChannel(channel);
            Process.GetCurrentProcess().CloseMainWindow();
            Process.GetCurrentProcess().Close();
        }
    }
}
