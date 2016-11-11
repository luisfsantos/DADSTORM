using System;
using System.Collections.Generic;
using DADSTORM.RemoteInterfaces;
using DADSTORM.CommonTypes;
using DADSTORM.CommonTypes.Parsing;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Text.RegularExpressions;
using System.Collections;
using System.Runtime.Serialization.Formatters;
using System.Threading;

namespace DADSTORM.PuppetMaster {

    public delegate void DelegateUpdateInfo(string msg);

    public class PuppetMaster {
        internal static readonly log4net.ILog Log =
                    log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly int PORT = 10001;
        private static readonly string NAME = "MoP";
        Dictionary<string, List<ICommands>> operators = new Dictionary<string, List<ICommands>>();
        Dictionary<string, IPCS> pcs = new Dictionary<string, IPCS>();
        LoggingLevel loggingLevel;
        Semantics semantics;
        internal MoPProxy logger;

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

        public void setLoggerOutputDestination(DelegateUpdateInfo del) {
            logger.addDelegateUpdateInfo(del);
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
            Log.Debug("Parse Config Done");
            #region Create Debug
            logger = new MoPProxy();
            if (loggingLevel.Equals(LoggingLevel.Full))
            {
                BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
                provider.TypeFilterLevel = TypeFilterLevel.Full;
                IDictionary props = new Hashtable();
                props["port"] = PuppetMaster.PORT;
                TcpChannel channel = new TcpChannel(props, null, provider);
                ChannelServices.RegisterChannel(channel, false);
                RemotingServices.Marshal(logger, PuppetMaster.NAME,
                                    typeof(MoPProxy));
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

            Thread.Sleep(1000);
            #region SendLogger
            if (loggingLevel.Equals(LoggingLevel.Full)) {
                foreach (List<ICommands> op in operators.Values) {
                    foreach (ICommands replica in op) {
                        replica.setLogger(logger);
                    }
                }
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
                pcs[ip].startProcess(op.Value.Id, i, op.Value.Addresses.Length, 
                    address, 
                    upstream, 
                    op.Value.OperatorSpec, 
                    op.Value.Routing, 
                    loggingLevel, 
                    semantics);
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

    }
}
