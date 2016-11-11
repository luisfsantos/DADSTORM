using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DADSTORM.Operator.OperatorWorkers {
    class CustomWorker : OperatorWorker {
        private string Dll;
        private string Class;
        private string Method;

        public CustomWorker(Operator op, string dll, string @class, string method) : base (op){
            this.Dll = dll.Trim('"');
            this.Class = @class.Trim('"');
            this.Method = method.Trim('"');
        }

        public override void processTuple(List<string> tuple) {
            byte[] code = File.ReadAllBytes(Dll);
            Assembly assembly = Assembly.Load(code);

            foreach (Type type in assembly.GetTypes()) {
                if (type.IsClass) {
                    if (type.FullName.EndsWith("." + Class)) {
                        object ClassObj = Activator.CreateInstance(type);

                        object[] args = new object[] { tuple };
                        object resultObject = type.InvokeMember(Method, 
                            BindingFlags.Default | BindingFlags.InvokeMethod, 
                            null, 
                            ClassObj, 
                            args);

                        IList<List<string>> result = (IList<List<string>>)resultObject;

                        foreach (List<string> resultTuple in result) {
                            Op.addTupleToSend(resultTuple);
                        }
                        break;
                    }
                }
            }
        }
    }
}
