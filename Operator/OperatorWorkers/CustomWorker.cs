using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.Operator.OperatorWorkers
{
    class CustomWorker : OperatorWorker {
        private string Dll;
        private string Class;
        private string Method;

        public CustomWorker(Operator op, string dll, string @class, string method) : base (op){
            this.Dll = dll;
            this.Class = @class;
            this.Method = method;
        }

        public override void processTuple(List<string> tuple) {
            throw new NotImplementedException();
        }
    }
}
