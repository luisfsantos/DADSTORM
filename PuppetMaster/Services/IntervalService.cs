﻿using DADSTORM.CommonTypes;
using DADSTORM.RemoteInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DADSTORM.PuppetMaster.Services
{
    public class IntervalService : PuppetMasterService
    {
        private string OpID;
        private int ms;

        public IntervalService(string opID, int ms)
        {
            this.OpID = opID;
            this.ms = ms;
        }

        public override void execute()
        {
            List<ICommands> replicas = PuppetMaster.Instance.GetOperator(OpID);
            foreach (ICommands replica in replicas) {
                try {
                    replica.interval(ms);
                } catch (SocketException e) {
                    //TODO: Error checking and verify if it should be removed from Downstream
                }
                
            }
            PuppetMaster.Instance.logger.notify(Command.INTERVAL, new string[] { OpID, ms.ToString() });

        }
    }
}
