using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace DADSTORM.Operator.FileReader {
    class TuplesReader {
        private string path;
        private int replicaId;
        private int replicaTotal;
        public List<List<string>> ReadTuples { get; private set; }

        public TuplesReader(string path, int replicaId, int replicaTotal) {
            this.path = path;
            this.replicaId = replicaId;
            this.replicaTotal = replicaTotal;   
        }

        public void read(ref ConcurrentQueue<List<string>> outputTo) {

            string [] lines = File.ReadAllLines(path);
            foreach (string line in lines) {
                if (line.StartsWith("%%"))
                    continue;
                outputTo.Enqueue(makeTuple(line));
            }
            
        }
        private List<string> makeTuple(string line) {
            string [] tupleArray = line.Split(',');
            List<string> tuple = new List<string>();
            foreach (string field in tupleArray) {
                tuple.Add(field.Trim(new char[] { ' ', '"' }));
            }
            return tuple;
        }
        
    }
}
