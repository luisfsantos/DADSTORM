using RemoteInterfaces;
using System;
using DADSTORM.CommonTypes;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PCS
{
    class PCSProxy : MarshalByRefObject, IPCS
    {

        public void startProcess(string id, List<string> upstreams, OperatorSpecification specs, string routing, LoggingLevel level, Semantics semantics)
        {
            Process process = new Process();
            process.StartInfo.FileName = "Operator.exe";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} [{1}] ", id, String.Join(",", upstreams));
            sb.AppendFormat("{0} [{1}] ", specs.Name, String.Join(",", specs.Params));
            sb.AppendFormat("{0} ", routing);
            sb.AppendFormat("{0} {1}", level, semantics);
            process.StartInfo.Arguments = sb.ToString();
            process.Start();
        }

    }
}
