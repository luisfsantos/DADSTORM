using DADSTORM.RemoteInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.Operator
{
    public class OperatorProxy : MarshalByRefObject, IOperator, ICommands
    {
        Operator Op;

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
            Op.addTuple(tuple);
        }
        #endregion

        #region MoP Debug

        public void crash()
        {
            throw new NotImplementedException();
        }

        public void freeze()
        {
            throw new NotImplementedException();
        }

        public void interval(int ms)
        {
            throw new NotImplementedException();
        }

        public void start()
        {
            throw new NotImplementedException();
        }

        public void status()
        {
            throw new NotImplementedException();
        }

        public void unfreeze()
        {
            throw new NotImplementedException();
        }

        public void wait(int ms)
        {
            throw new NotImplementedException();
        } 
        #endregion

    }
}
