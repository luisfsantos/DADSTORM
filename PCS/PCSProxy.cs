using DADSTORM.RemoteInterfaces;
using System;
using DADSTORM.CommonTypes;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DADSTORM.PCS {
    class PCSProxy : MarshalByRefObject, IPCS
    {
        public static readonly string PROGRAM_NAME = "Operator.exe";

        public void startProcess(string id, string address, List<string> upstreams, OperatorSpecification specs, string routing, LoggingLevel logging, Semantics semantics)
        {
            Process process = new Process();
            process.StartInfo.FileName = PROGRAM_NAME;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} {1} [{2}] ", id, address, String.Join(",", upstreams));
            sb.AppendFormat("{0} [{1}] ", specs.Name, String.Join(",", specs.Params));
            sb.AppendFormat("{0} ", routing);
            sb.AppendFormat("{0} {1}", logging, semantics);
            process.StartInfo.Arguments = sb.ToString();
            process.Start();
        }

    }
}
