using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DADSTORM.PuppetMaster.Services
{
    delegate void ServiceWork();

    public sealed class Singleton
    {
        private static volatile Singleton instance;
        private static object syncRoot = new Object();

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Singleton();
                    }
                }

                return instance;
            }
        }
    }

    sealed class ServiceThreads
    {
        private readonly int numberThreads = 10;
        private readonly int bufferSize = 20;
        private static volatile ServiceThreads instance;
        private static object syncRoot = new Object();
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
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ServiceThreads();
                    }
                }

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
