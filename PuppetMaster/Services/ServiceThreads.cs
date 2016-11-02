using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DADSTORM.PuppetMaster.Services
{
    delegate void ServiceWork();

    sealed class ServiceThreads
    {
        private readonly int numberThreads = 10;
        private readonly int bufferSize = 20;
        private static volatile ServiceThreads instance = new ServiceThreads();
        private Queue<ServiceWork> workBuffer;

        public ServiceThreads()
        {
            workBuffer = new Queue<ServiceWork>(bufferSize);

            for (int i = 0; i < numberThreads; i++)
            {
                Thread t = new Thread(Consume);
                t.IsBackground = true; // makes threads close automatically when Main() finishes
                t.Start();
            }
        }

        public static ServiceThreads Instance
        {
            get
            {
                return instance;
            }
        }

        private void Consume()
        {
            while (true)
            {
                lock (workBuffer)
                {
                    while (workBuffer.Count == 0)
                    {
                        Monitor.Wait(workBuffer);
                    }
                    ServiceWork rem = workBuffer.Dequeue();
                    rem.BeginInvoke(null, null); //asynchronous call
                    Monitor.Pulse(workBuffer);
                }
            }
        }

        public void AssyncInvoke(ServiceWork service)
        {
            lock (workBuffer)
            {
                while (workBuffer.Count == bufferSize)
                {
                    Monitor.Wait(workBuffer);
                }
                workBuffer.Enqueue(service);
                Monitor.Pulse(workBuffer);
            }
        }
    }
}
