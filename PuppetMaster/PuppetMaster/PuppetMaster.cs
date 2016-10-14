using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.PuppetMaster {
    public class PuppetMaster : IPuppetMaster {
        private static readonly log4net.ILog log =
                    log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        KeyValuePair<string, List<string>> operators = new KeyValuePair<string, List<string>>();
        private static readonly PuppetMaster instance = new PuppetMaster();

        private PuppetMaster() { }

        public static PuppetMaster Instance {
            get {
                return instance;
            }
        }

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


    }
}
