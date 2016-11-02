using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteInterfaces
{
    public interface IDebug
    {
        void sendInfo(string[] info);
    }
}
