using System.Collections.Generic;

namespace DADSTORM.RemoteInterfaces {
    public interface IOperator
    {
        void send(List<string> tuple);
        void addDownstreamOperator(/*may have parameters*/);
    }
}
