using System;
using System.Collections.Generic;
using DADSTORM.RemoteInterfaces;
using DADSTORM.CommonTypes;
using DADSTORM.CommonTypes.Parsing;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Text.RegularExpressions;

namespace DADSTORM.PuppetMaster {
    public class PuppetMaster {
        private static readonly log4net.ILog log =
                    log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly int PORT = 10001;
        private static readonly string NAME = "MoP";
        Dictionary<string, List<ICommands>> operators = new Dictionary<string, List<ICommands>>();
        Dictionary<string, IPCS> pcs = new Dictionary<string, IPCS>();
        LoggingLevel loggingLevel;
        Semantics semantics;

        private static readonly PuppetMaster instance = new PuppetMaster();

        private PuppetMaster() { }

        public static PuppetMaster Instance {
            get {
                return instance;
            }
        }

        public ICommands GetReplica(string operatorID, int replica) {
            List<ICommands> result;
            if (operators.TryGetValue(operatorID, out result)) {
                return result[replica];
            } else {
                return null;
            }
        }

        public List<ICommands> GetOperator(string operatorID) {
            List<ICommands> result;
            if (operators.TryGetValue(operatorID, out result)) {
                return result;
            } else {
                return null;
            }
        }

        public bool init(string pathToFile)
        {
            //TODO needs exception handling
            #region Parse Config File
            ConfigFileParser parser = new ConfigFileParser(pathToFile);
            Dictionary<string, OperatorData> operatorsData = parser.GetOperatorsData();
            loggingLevel = parser.GetLogging();
            semantics = parser.GetSemantics();
            #endregion

            #region Create Debug
            if (loggingLevel.Equals(LoggingLevel.Full))
            {
                TcpChannel channel = new TcpChannel(PuppetMaster.PORT);
                ChannelServices.RegisterChannel(channel, false);
                RemotingConfiguration.RegisterWellKnownServiceType(
                                        typeof(MoPProxy),
                                        PuppetMaster.NAME,
                                        WellKnownObjectMode.Singleton);
            } else
            {
                TcpChannel channel = new TcpChannel();
                ChannelServices.RegisterChannel(channel, false);
            }
            
            #endregion

            #region Connect to PCSs
            foreach (string ip in parser.GetPcsIps())
            {
                string url = "tcp://" + ip + ":"+ PCSConstants.PORT + "/" + PCSConstants.NAME;

                IPCS auxPCS = (IPCS)Activator.GetObject(
                                        typeof(IPCS),
                                        url);
                pcs.Add(ip, auxPCS);
            }
            #endregion

            #region Create Operators & Replicas
            foreach (KeyValuePair<string, OperatorData> op in operatorsData) {
                launchOperator(op, operatorsData);
                connectToOperator(op);
            }
            #endregion

            return true; //FIXME

        }

        private void connectToOperator(KeyValuePair<string, OperatorData> op)
        {
            List<ICommands> replicas = new List<ICommands>();
            foreach (string address in op.Value.Addresses)
            {
                replicas.Add((ICommands)Activator.GetObject(
                                        typeof(ICommands),
                                        address));
            }
            operators.Add(op.Key, replicas);
        }

        private void launchOperator(KeyValuePair<string, OperatorData> op, Dictionary<string, OperatorData> operatorsData) {
            string pattern = @"tcp://(?<ip>(?:[0-9]{1,3}\.){3}[0-9]{1,3}):\d{1,5}/op";
            Regex regex = new Regex(pattern);
            int i = 1;
            foreach (string address in op.Value.Addresses) {
                Match match = regex.Match(address);
                string ip = match.Result("${ip}");
                List<string> upstream = getUpstream(op.Value.Input_ops, operatorsData);
                pcs[ip].startProcess(i, op.Value.Addresses.Length, address, upstream, op.Value.OperatorSpec, op.Value.Routing, loggingLevel, semantics);
                i++;
            }
        }

        private List<string> getUpstream(string[] input_ops, Dictionary<string, OperatorData> operatorsData)
        {
            List<string> result = new List<string>();
            foreach (string op in input_ops)
            {
                if (op.Contains("."))
                {
                    result.Add(op);
                } else
                {
                    result.AddRange(operatorsData[op].Addresses);
                }  
            }
            return result;
        }



        #region TO REMOVE
        public void start(string op_id) {
            log.Info("Start " + op_id);
            //TODO
        }

        public void interval(string op_id, int ms) {
            log.Info("Interval " + op_id + " " + ms);
            //TODO
        }

        public void status() {
            log.Info("Status");
            //TODO
        }

        public void crash(string process) {
            log.Info("Crash " + process);
            //TODO
        }

        public void freeze(string process) {
            log.Info("Freeze " + process);
            //TODO
        }

        public void unfreeze(string process) {
            log.Info("Unfreeze " + process);
            //TODO
        }

        public void wait(int ms) {
            log.Info("Wait " + ms);
            //TODO
        }
        #endregion


    }
}
