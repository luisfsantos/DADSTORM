using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.Operator {
    class OutputOperator {
        public static void writeToFile(List<string> tuple) {
            string outputFile = @".\output.txt";
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(outputFile, false)) {
                string line = String.Join(" ", tuple.ToArray());
                file.WriteLine(line);
            }
        }
    }
}

