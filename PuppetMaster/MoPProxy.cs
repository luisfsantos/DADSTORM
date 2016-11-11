using System;
using System.Collections.Generic;
using DADSTORM.RemoteInterfaces;
using System.Windows.Forms;

namespace DADSTORM.PuppetMaster {
    [Serializable]
    public class MoPProxy : MarshalByRefObject, ILogger
    {
        DelegateUpdateInfo updateHistory;
        public void sendInfo(string OpAddress, List<string> tuple) {
            string output = "tuple " + OpAddress + ", <" + String.Join(", ", tuple.ToArray()) + ">";
            PuppetMaster.Log.Debug(output);
            Form.ActiveForm.Invoke(updateHistory, new object[] { output });
        }

        public void notify(string name, string[] param) {
            string output = name + " " + String.Join(" ", param);
            PuppetMaster.Log.Debug(output);
            Form.ActiveForm.Invoke(updateHistory, new object[] { output });

        }

        internal void addDelegateUpdateInfo(DelegateUpdateInfo del) {
            updateHistory = del;
        }
    }
}
