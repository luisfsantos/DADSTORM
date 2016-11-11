using DADSTORM.RemoteInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public void addDownstreamOperator(string address)
        {
            IOperator downstream = (IOperator)Activator.GetObject(typeof(IOperator), address);
            Op.addDownstreamOperator(downstream);
        }

        public void send(List<string> tuple)
        {
            Op.addTupleToProcess(tuple);
        }
        #endregion

        #region MoP Debug

        public void crash()
        {
            Process.GetCurrentProcess().Kill();
        }

        public void freeze()
        {
            Op.freeze();
        }

        public void interval(int ms)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void unfreeze()
        {
            Op.unfreeze();
        }
        #endregion

    }
}
