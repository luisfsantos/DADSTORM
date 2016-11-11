using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DADSTORM.RemoteInterfaces;
using System.Security.Cryptography;

namespace DADSTORM.Operator.RoutingStrategy
{
    public class HashRouting : Routing
    {
        public int Field { get; private set; }

        public HashRouting () : this(0) { }

        public HashRouting (int field)
        {
            this.Field = field;
        }

        public override int Route(int totalReplica, List<string> tuple)
        {
            MD5 md5Hasher = MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(tuple[Field]));
            int integerHash = Math.Abs(BitConverter.ToInt32(hashed, 0));
            int hashedOperator = integerHash % totalReplica;
            return hashedOperator;
        }
    }
}
