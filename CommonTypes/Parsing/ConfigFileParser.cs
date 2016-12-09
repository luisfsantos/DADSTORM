using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using static DADSTORM.CommonTypes.Parsing.TopologicalSorter;

namespace DADSTORM.CommonTypes.Parsing {

    public class ConfigFileParser {
        ConcurrentBag<OperatorData> operators = new ConcurrentBag<OperatorData>();
        private string path;
        string lines;

        public ConfigFileParser(string pathToFile) {
            this.path = pathToFile;
        }

        private void readLines() {
            if (String.IsNullOrEmpty(lines)) {
                lines = File.ReadAllText(path).Replace("localhost", "127.0.0.1");
            }
        }

        public Dictionary<string, OperatorData> GetOperatorsData() {
            readLines();

            const string pattern = @"(?<=\n)\w+\s+input_ops\s+.+?\s+" +
                @"rep_fact\s+\d+\s+routing\s+\w+\s+" +
                @"address\s+.+?" +
                @"operator_spec.+\n";
            Regex regex = new Regex(pattern);

            if (regex.IsMatch(lines)) {
                MatchCollection matches = regex.Matches(lines);

                List<Thread> thr = new List<Thread>(matches.Count);
                foreach (Match m in matches) {
                    Thread t = new Thread(() => ExtractOperatorInfo(m));
                    t.Start();
                    thr.Add(t);
                }

                foreach (Thread t in thr) {
                    t.Join();
                }

            } else {
                throw new Exception("Incorrectly formatted configuration file.");
            }

            CheckIfAcyclic(operators.ToList());

            return operators.ToDictionary(o => o.Id);
        }

        public IEnumerable<string> GetPcsIps()
        {
            readLines();
            string pattern = @"tcp://(?<ip>(?:[0-9]{1,3}\.){3}[0-9]{1,3}):\d{1,5}/op";
            Regex regex = new Regex(pattern);
            HashSet<string> ips = new HashSet<string>();
            foreach (Match m in regex.Matches(lines)) {
                ips.Add(m.Result("${ip}"));
            }
            return ips;
        }

        private void ExtractOperatorInfo(Match op) {
            readLines();

            OperatorData opdata = new OperatorData();
            Match match;
            string matchstr;
            string pattern;

            #region - match ID -
            pattern = @"\w+(?=\s+input_ops)"; //uses lookahead
            matchstr = Regex.Match(op.ToString(), pattern).ToString();
            opdata.Id = matchstr;
            #endregion

            #region - match INPUT_OPS -
            pattern = @"(?<=input_ops)(.+?)(?=rep_fact)";

            matchstr = Regex.Match(op.ToString(), pattern).ToString();
            matchstr = Regex.Replace(matchstr, @"\s+", "");
            opdata.Input_ops = matchstr.Split(',');

            #endregion

            #region - match REP_FACT -
            pattern = @"(?<=rep_fact\s+)\d+"; //uses lookbehind
            matchstr = Regex.Match(op.ToString(), pattern).ToString();
            int parsed;
            if (!int.TryParse(matchstr, out parsed))
                throw new Exception("OPERATOR " + opdata.Id + ": REP_FACT must be an integer number");
            opdata.Rep_fact = parsed;
            #endregion

            #region - match ROUTING -
            pattern = @"(?<=routing\s+)random|primary|hashing\((?<hash>\d+)\)"; //uses lookbehind
            match = Regex.Match(op.ToString(), pattern);
            opdata.Routing = match.ToString();
            
            #endregion

            #region - match ADDRESSES -
            pattern = @"(?<=address)\s+.+?(?=operator_spec)"; //uses lookbehind
            matchstr = Regex.Match(op.ToString(), pattern).ToString();
            matchstr = Regex.Replace(matchstr, @"\s+", "");
            string[] addresses = matchstr.Split(',');

            if (addresses.Length != opdata.Rep_fact) {
                throw new Exception("OPERATOR " + opdata.Id + ": REP_FACT and number of ADDRESSES specified do not match");
            }

            //validate addresses
            foreach (string address in addresses) {
                match = Regex.Match(address, @"tcp://(?:[0-9]{1,3}\.){3}[0-9]{1,3}:(?<port>\d{1,5})/op");
                if (!match.Success) {
                    throw new Exception("OPERATOR " + opdata.Id + ": Incorrectly formed address " + address);
                }
                int port = int.Parse(match.Result("${port}"));
                if (port < 1024 || port > 65535 || port == 10000 || port == 10001) {
                    throw new Exception("OPERATOR " + opdata.Id + ": Unavailable port for address " + address);
                }
            }
            opdata.Addresses = addresses.ToList();
            #endregion

            #region - match OPERATOR_SPEC -
            pattern = @"(?<=operator_spec)\s+(?<op>\b\w+\b)(?<params>.+?)?\n"; //uses lookbehind

            match = Regex.Match(op.ToString(), pattern);

            Console.WriteLine(match.ToString());
            OperatorSpecification spec = new OperatorSpecification();
            spec.Name = match.Result("${op}");
            matchstr = match.Result("${params}");
            matchstr = Regex.Replace(matchstr, @"\s+", "");
            spec.Params = matchstr.Split(',');
            opdata.OperatorSpec = spec;
            #endregion

            operators.Add(opdata);

        }

        public LoggingLevel GetLogging() {
            readLines();
            const string pattern = @"(?<=\nLoggingLevel\s+)(\blight\b|\bfull\b)";
            string match = Regex.Match(lines, pattern).ToString();

            switch (match) {
                case "light":
                    goto default;
                case "full":
                    return LoggingLevel.Full;
                default:
                    return LoggingLevel.Light;
            }

        }

        public Semantics GetSemantics() {
            readLines();
            const string pattern = @"(?<=\nSemantics\s+)(\bat-most-once\b|\bat-least-once\b|\bexactly-once\b)";
            string match = Regex.Match(lines, pattern).ToString();

            switch (match) {
                case "at-most-once":
                    goto default;
                case "at-least-once":
                    return Semantics.AtLeastOnce;
                case "exactly-once":
                    return Semantics.ExactlyOnce;
                default:
                    return Semantics.AtMostOnce;
            }
        }

        private static void CheckIfAcyclic(List<OperatorData> ops) {

            Dictionary<string, Field> dictionary = new Dictionary<string, Field>();

            // add all input_ops 
            foreach (OperatorData op in ops) {
                foreach (string input_op in op.Input_ops) {
                    if (!dictionary.ContainsKey(input_op))
                        dictionary.Add(input_op, new Field() { Name = input_op });
                }
            }

            // add all dependencies and missing operators
            foreach (OperatorData op in ops) {
                if (!dictionary.ContainsKey(op.Id.ToString())) {
                    dictionary.Add(op.Id,
                        new Field() { Name = op.Id, DependsOn = op.Input_ops });
                } else {
                    dictionary[op.Id].DependsOn = op.Input_ops;
                }
            }

            List<Field> fields = dictionary.Values.ToList();

            TopologicalSorter.getTopologicalSortOrder(fields);
        }
    }
}


