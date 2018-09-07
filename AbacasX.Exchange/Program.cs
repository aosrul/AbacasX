using AbacasX.Exchange.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbacasX.Exchange
{
    class Program
    {
        static SynchronizationContext _SyncContext = null;

        static void Main(string[] args)
        {
            _SyncContext = SynchronizationContext.Current;


            // Start the Exchange
            Console.WriteLine("Current UI Thread is {0}", Thread.CurrentThread.ManagedThreadId);
            ServiceHost hostOrderManager = new ServiceHost(typeof(OrderManager));
            hostOrderManager.Open();
            Console.WriteLine("Abacas Exchange Order Services Started...");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();


            hostOrderManager.Close();
        }
    }
}
