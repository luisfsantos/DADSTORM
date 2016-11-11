using DADSTORM.RemoteInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.Operator
{
    public class OperatorProxy : MarshalByRefObject, IOperator, ICommands
    {
        Operator Op;
        private bool started = false;

        public OperatorProxy(Operator op)
        {
            this.Op = op;
        }

        #region Operator
        public void addDownstreamOperator(string address, string routing, string DownstreamID, int replica)
        {
            IOperator downstream = (IOperator)Activator.GetObject(typeof(IOperator), address);
            Op.addDownstreamOperator(DownstreamID, replica, downstream, routing);
        }

        public void send(List<string> tuple)
        {
            Op.addTupleToProcess(tuple);
        }
        #endregion

        #region MoP Debug

        public void crash()
        {
            RemotingServices.Disconnect(this);
            Process.GetCurrentProcess().Kill();
        }

        public void freeze()
        {
            Op.Frozen.Reset();
        }

        public void interval(int ms)
        {
            Op.interval(ms);
        }

        public void start()
        {
            if (!started) {
                Op.run();
                started = true;
            }
            
        }

        public void status()
        {
            Console.WriteLine(Op.CurrentStatus.ConstructStatus());
        }

        public void unfreeze()
        {
            Op.Frozen.Set();
        }

        public void setLogger(ILogger logger) {
            Op.Logger = logger;
        }
        #endregion

    }
}
