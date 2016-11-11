using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DADSTORM.RemoteInterfaces;

namespace DADSTORM.PuppetMaster
{
    [Serializable]
    public class MoPProxy : MarshalByRefObject, ILogger
    {
        public void sendInfo(string OpAddress, List<string> tuple) {
            PuppetMaster.Log.Debug("tuple " + OpAddress + ", " + String.Join(" ", tuple.ToArray()));
        }
    }
}
