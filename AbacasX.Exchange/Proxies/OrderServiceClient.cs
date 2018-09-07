using AbacasX.Exchange.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Exchange.Proxies
{
    public class OrderServiceClient : DuplexClientBase<IOrderService>, IOrderService
    {
        public OrderServiceClient(InstanceContext instanceContext, string endpointName)
            : base(instanceContext, endpointName)
        { }

        public OrderServiceClient(InstanceContext instanceContext, Binding binding, EndpointAddress address)
            : base(instanceContext, binding, address)
        { }

        public List<BlockChainData> GetClientBlockChainTransactions(int clientId)
        {
            return Channel.GetClientBlockChainTransactions(clientId);
        }

        public List<ClientPositionData> GetClientPositions(int clientId)
        {
            return Channel.GetClientPositions(clientId);
        }


        public List<OrderData> GetClientOrders(int clientId)
        {
            return Channel.GetClientOrders(clientId);
        }

        public List<OrderData> GetClientHistoricalOrders(int clientId)
        {
            return Channel.GetClientOrders(clientId);
        }

        public OrderData AddOrder(OrderData orderData)
        {
            return Channel.AddOrder(orderData);
        }

        public OrderData GetOrder(int OrderId)
        {
            return Channel.GetOrder(OrderId);
        }

        public int SuspendOrder(int OrderID)
        {
            return Channel.SuspendOrder(OrderID);
        }

        public int ActivateOrder(int OrderID)
        {
            return Channel.ActivateOrder(OrderID);
        }

        public int CancelOrder(int OrderID)
        {
            return Channel.CancelOrder(OrderID);
        }
    }
}
