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
            public void DepositAdded(AssetDepositData depositData)
            {
                Console.WriteLine("Deposit of {0} {1} with Reference {2} Added", depositData.assetId, depositData.amount, depositData.referenceId);
            }

            public void OrderAdded(OrderData orderData)
            {
                Console.WriteLine("Order {0} Added", orderData.OrderId);
            }

            public void WithdrawalAdded(AssetWithdrawalData withdrawalData)
            {
                Console.WriteLine("Withdrawal of {0} {1} with Reference {2} Added", withdrawalData.tokenId, withdrawalData.amount, withdrawalData.referenceId);
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
            return _orderServiceClient.GetClientOrdersAsync(ClientId);
        }

        public Task<OrderData[]> GetClientHistoricalOrdersAsync(int ClientId)
        {
            return _orderServiceClient.GetClientHistoricalOrdersAsync(ClientId);
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
            return _orderServiceClient.GetClientPositionsAsync(ClientId);
        }

        public Task<BlockChainData[]> GetClientBlockChainTransactionsAsync(int ClientId)
        {
            return _orderServiceClient.GetClientBlockChainTransactionsAsync(ClientId);
        }

        public Task<string> GetNewGuidAsync()
        {
            return _orderServiceClient.GetNewGuidAsync();
        }

        public Task<AssetDepositData> AddDepositAsync(AssetDepositData depositNotification)
        {
            return _orderServiceClient.AddDepositAsync(depositNotification);
        }

        public Task<AssetWithdrawalData> AddWithdrawalAsync(AssetWithdrawalData withdrawalRequest)
        {
            return _orderServiceClient.AddWithdrawalAsync(withdrawalRequest);
        }

        public Task<AssetTransferData[]> GetClientTransferActivityAsync(int ClientId)
        {
            return _orderServiceClient.GetClientTransferActivityAsync(ClientId);
        }
    }
}
