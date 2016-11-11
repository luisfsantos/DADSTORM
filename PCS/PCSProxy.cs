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

        public void startProcess(string operatorID, int replIndex, int replTotal, string address, List<string> upstreams, OperatorSpecification specs, string routing, LoggingLevel logging, Semantics semantics)
        {
            Process process = new Process();
            process.StartInfo.FileName = PROGRAM_NAME;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} {1} {2} ", operatorID, replIndex, replTotal);
            sb.AppendFormat("{0} [{1}] ", address, String.Join(",", upstreams));
            sb.AppendFormat("{0} [{1}] ", specs.Name, String.Join(",", specs.Params).Replace("\"", "\\\"")); //HACK need to convert " to \" because of Operator.exe Main args
            sb.AppendFormat("{0} ", routing);
            sb.AppendFormat("{0} {1}", logging, semantics);
            process.StartInfo.Arguments = sb.ToString();
            process.Start();
        }

    }
}
