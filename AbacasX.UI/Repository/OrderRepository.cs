using OrderService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AbacasX.Repository
{
    public class OrderRepository : IOrderService
    {
        OrderServiceClient _orderServiceClient;

        public OrderRepository()
        {
            
            _orderServiceClient = new OrderServiceClient();
            
        }

        [CallbackBehavior(UseSynchronizationContext = false)]
        public class OrderServiceCallBack : IOrderServiceCallback
        {
            public void OrderAdded(OrderData orderData)
            {
                Console.WriteLine("Order {0} Added", orderData.OrderId);
            }
        }

       
        public Task<int> ActivateOrderAsync(int OrderID)
        {
            return _orderServiceClient.ActivateOrderAsync(OrderID);
        }

       
        public Task<int> CancelOrderAsync(int OrderID)
        {
            return _orderServiceClient.CancelOrderAsync(OrderID);
        }

        public Task<int> SuspendOrderAsync(int OrderID)
        {
            return _orderServiceClient.SuspendOrderAsync(OrderID);
        }

        
        public Task<OrderData[]> GetClientOrdersAsync(int ClientId)
        {
            // Client Id initially not utilized as this call is really meant
            // for the Web Publishing Service, not the order manager
            return _orderServiceClient.GetClientOrdersAsync(0);
        }

        public Task<OrderData[]> GetClientHistoricalOrdersAsync(int ClientId)
        {
            return _orderServiceClient.GetClientHistoricalOrdersAsync(0);
        }

        Task<OrderData> IOrderService.GetOrderAsync(int OrderId)
        {
            return _orderServiceClient.GetOrderAsync(0);
        }

        Task<OrderData> IOrderService.AddOrderAsync(OrderData orderData)
        {
            return _orderServiceClient.AddOrderAsync(orderData);
        }

        public Task<ClientPositionData[]> GetClientPositionsAsync(int ClientId)
        {
            return _orderServiceClient.GetClientPositionsAsync(0);
        }

        public Task<BlockChainData[]> GetClientBlockChainTransactionsAsync(int ClientId)
        {
            return _orderServiceClient.GetClientBlockChainTransactionsAsync(0);
        }
    }
}
