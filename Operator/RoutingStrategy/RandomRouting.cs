using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DADSTORM.RemoteInterfaces;

namespace DADSTORM.Operator.RoutingStrategy
{
    public class RandomRouting : Routing
    {
        public override IOperator Route(List<IOperator> downstream, List<string> tuple)
        {
            Random randomizer = new Random();
            int randomOperator = randomizer.Next(downstream.Count);
            return downstream[randomOperator];
        }
    }
}
