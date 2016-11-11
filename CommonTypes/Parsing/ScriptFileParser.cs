using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DADSTORM.CommonTypes.Parsing {
    public class ScriptFileParser {

        private string path;
        private Queue<KeyValuePair<string, string[]>> commands = new Queue<KeyValuePair<string, string[]>>();

        public ScriptFileParser(string path) {
            this.path = path;
        }

        public void parse() {
            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines) {
                if (!line.StartsWith("%%"))
                    commands.Enqueue(parseLine(line));
            }
        }

        public bool hasNextCommand() {
            return commands.Count != 0;
        }

        private KeyValuePair<string, string[]> parseLine(string line) {
            string[] tokens = line.Split(' ');
            return new KeyValuePair<string, string[]>(tokens[0], tokens.Skip(1).ToArray());
        }

        public KeyValuePair<string, string[]> nextCommand() {
            return commands.Dequeue();
        }
    }
}
