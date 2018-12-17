using AbacasX.Exchange.Contracts;
using AbacasX.Exchange.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
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

            using (ServiceHost hostOrderManager = new ServiceHost(typeof(OrderManager), new Uri("http://localhost:8084")))
            {
                //ServiceHost hostOrderManager = new ServiceHost(typeof(OrderManager));
                
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                hostOrderManager.Description.Behaviors.Add(smb);

                hostOrderManager.AddServiceEndpoint(typeof(IOrderService), new NetHttpBinding(), "");
                hostOrderManager.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

                hostOrderManager.Open();
                Console.WriteLine("Abacas Exchange Order Services Started...");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                hostOrderManager.Close();
            }
        }
    }
}
