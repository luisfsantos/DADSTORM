using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.RemoteInterfaces
{
    public interface ILogger
    {
        void sendInfo(string OPAddress, List<string> tuple);
    }
}
