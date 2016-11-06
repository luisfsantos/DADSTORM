using DADSTORM.RemoteInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DADSTORM.Operator.RoutingStrategy {
    public abstract class Routing {


        public static Routing GetInstance(string type)
        {
            
            if (type.Contains("random"))
            {
                return new RandomRouting();
            } else if (type.Contains("primary"))
            {
                return new PrimaryRouting();
            } else if (type.Contains("hashing"))
            {
                string pattern = @"hashing\((?<hash>\d+)\)";
                Match match = Regex.Match(type, pattern);
                int field = Int32.Parse(match.Result("${hash}"));
                return new HashRouting(field);
            } else
            {
                throw new Exception(type + ": is not a supported routing policy.");
            }
        }

        public abstract IOperator Route(List<IOperator> downstream, List<string> tuple);
    }
}
