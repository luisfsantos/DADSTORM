using System;

namespace DADSTORM.Operator {
    class Program {
        static void Main(string[] args) {
            string id = args[0],
                address = args[1],
                upstreams = args[2],
                specName = args[3],
                specParamStr = args[4],
                routing = args[5],
                logging = args[6],
                semantics = args[7];

            string[] upstream_addrs = upstreams.Trim(new char[] { ']', '[' }).Split(',');
            string[] specParams = specParamStr.Trim(new char[] { ']', '[' }).Split(',');
            Operator op = new Operator(id, upstream_addrs, specName, specParams, routing, logging, semantics);
            
            Console.ReadLine();
        }
    }
}
