using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DADSTORM.RemoteInterfaces;

namespace DADSTORM.Operator.RoutingStrategy
{
    public class PrimaryRouting : Routing
    {
        public override IOperator Route(List<IOperator> downstream, List<string> tuple)
        {
            return downstream[0];
        }
    }
}
