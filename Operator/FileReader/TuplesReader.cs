using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace DADSTORM.Operator.FileReader {
    class TuplesReader {
        private Operator op;
        private string path;
        private int replicaId;
        private int replicaTotal;
        public List<List<string>> ReadTuples { get; private set; }

        public TuplesReader(Operator op, string path, int replicaId, int replicaTotal) {
            this.op = op;
            this.path = path;
            this.replicaId = replicaId-1;
            this.replicaTotal = replicaTotal;   
        }

        public void read() {

            string [] lines = File.ReadAllLines(path);
            foreach (string line in lines) {
                if (line.StartsWith("%%"))
                    continue;
                List<string> tuple = makeTuple(line);
                if (replicaId == op.MyRouting.Route(replicaTotal, tuple)) {
                    //string uuid = op.generateID();
                    op.addTupleToProcess(tuple, "-1", null);
                }
                    
            }
            
        }
        private List<string> makeTuple(string line) {
            string [] tupleArray = line.Split(',');
            List<string> tuple = new List<string>();
            foreach (string field in tupleArray) {
                tuple.Add(field.Trim(' '));
            }
            return tuple;
        }
        
    }
}
