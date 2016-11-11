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
        public override int Route(int totalReplicas, List<string> tuple)
        {
            return 0;
        }
    }
}
