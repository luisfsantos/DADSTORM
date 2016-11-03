using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteInterfaces;
using DADSTORM.CommonTypes;
using DADSTORM.CommonTypes.Parsing;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;

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

        public bool init(string pathToFile)
        {
            #region Parse Config File
            ConfigFileParser parser = new ConfigFileParser(pathToFile);
            Dictionary<string, OperatorData> operatorData = parser.GetOperatorsData();
            loggingLevel = parser.GetLogging();
            semantics = parser.GetSemantics();
            #endregion

            #region Create Debug
            if (loggingLevel.Equals(LoggingLevel.Full))
            {
                TcpChannel channel = new TcpChannel(PuppetMaster.PORT);
                ChannelServices.RegisterChannel(channel, true);
                RemotingConfiguration.RegisterWellKnownServiceType(
                                        typeof(MoPProxy),
                                        PuppetMaster.NAME,
                                        WellKnownObjectMode.Singleton);
            } else
            {
                TcpChannel channel = new TcpChannel();
                ChannelServices.RegisterChannel(channel, true);
            }
            
            #endregion

            #region Communicate with PCSs
            foreach (string ip in parser.GetPcsIps())
            {
                string url = "tcp://" + ip + ":10000/PCS";

                IPCS auxPCS = (IPCS)Activator.GetObject(
                                        typeof(IPCS),
                                        url);
                pcs.Add(url, auxPCS);
            }
            #endregion

            #region Create Operators & Replicas
            foreach (KeyValuePair<string, OperatorData> op in operatorData) {
                launchOperator(op);
            }
            #endregion
            return true; //FIXME

        }

        private void launchOperator(KeyValuePair<string, OperatorData> op) {
            string pattern = @"tcp://(?:[0-9]{1,3}\.){3}[0-9]{1,3}(?=:\d{1,5})/op";
            foreach (string address in op.Value.Addresses) {

            }
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
