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

        #region Operator
        public void addDownstreamOperator()
        {
            throw new NotImplementedException();
        }

        public void send(List<string> tuple)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region MoP Debug
        public void crash(string process)
        {
            throw new NotImplementedException();
        }

        public void freeze(string process)
        {
            throw new NotImplementedException();
        }

        public void interval(string op_id, int ms)
        {
            throw new NotImplementedException();
        }

        public void start(string op_id)
        {
            throw new NotImplementedException();
        }

        public void status()
        {
            throw new NotImplementedException();
        }

        public void unfreeze(string process)
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
