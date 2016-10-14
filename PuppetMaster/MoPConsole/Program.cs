using DADSTORM.PuppetMaster;
using DADSTORM.PuppetMaster.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoPConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var carryon = true;
            while (carryon) // Loop indefinitely
            {
                Console.WriteLine("Enter input:"); // Prompt
                string line = Console.ReadLine(); // Get string from user
                List<string> result = line.Split(' ').ToList();
                switch (result[0])
                {
                    case Command.START:
                        if (result.Count == Command.ONEINPUT)
                        {
                           new StartService(result[1]).assyncexecute();
                        } else
                        {
                            goto default;
                        }
                        break;
                    case Command.INTERVAL:
                        if (result.Count == Command.TWOINPUT)
                        {
                            new IntervalService(result[1], Int32.Parse(result[2])).assyncexecute();
                        }
                        else
                        {
                            goto default;
                        }
                        break;
                    case Command.STATUS:
                        if (result.Count == Command.ZEROINPUT) {
                            new StatusService().assyncexecute();
                        } else {
                            goto default;
                        }
                        break;
                    case Command.CRASH:
                        if (result.Count == Command.ONEINPUT) {
                            new CrashService(result[1]).assyncexecute();
                        } else {
                            goto default;
                        }
                        break;
                    case Command.FREEZE:
                        if (result.Count == Command.ONEINPUT) {
                            new FreezeService(result[1]).assyncexecute();
                        } else {
                            goto default;
                        }
                        break;
                    case Command.WAIT:
                        if (result.Count == Command.ONEINPUT) {
                            new WaitService(Int32.Parse(result[1])).execute();
                        } else {
                            goto default;
                        }
                        break;
                    case Command.EXIT:
                        carryon = false;
                        Console.WriteLine("Goodbye");
                        break;
                    default:
                        Console.WriteLine("Cannot read command");
                        break;
                }
            }
        }
    }

    class Command
    {
        public const string START = "Start";
        public const string INTERVAL = "Interval";
        public const string STATUS = "Status";
        public const string CRASH = "Crash";
        public const string FREEZE = "Freeze";
        public const string UNFREEZE = "Unfreeze";
        public const string WAIT = "Wait";
        public const string EXIT = "Exit";
        public const int ZEROINPUT = 1;
        public const int ONEINPUT = 2;
        public const int TWOINPUT = 3;
    }
}
