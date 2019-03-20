using AbacasX.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbacasX.Repository
{
    class Program
    {
        static SynchronizationContext _SyncContext = null;

        static void Main(string[] args)
        {
            _SyncContext = SynchronizationContext.Current;

            Console.Title = "Abacas Repository Service";
            // Start the Exchange
            Console.WriteLine("Current UI Thread is {0}", Thread.CurrentThread.ManagedThreadId);
            ServiceHost hostOrderManager = new ServiceHost(typeof(RepositoryManager));
            hostOrderManager.Open();
            Console.WriteLine("Abacas Repository Manager Service Started...");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();


            hostOrderManager.Close();
        }
    }
}
