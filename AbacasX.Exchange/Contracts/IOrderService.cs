using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AbacasX.Exchange.Contracts
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IOrderServiceCallBack))]
    public interface IOrderService
    {
        [OperationContract]
        List<BlockChainData> GetClientBlockChainTransactions(int ClientId);

        [OperationContract]
        List<ClientPositionData> GetClientPositions(int ClientId);

        [OperationContract]
        List<OrderData> GetClientOrders(int ClientId);

        [OperationContract]
        List<OrderData> GetClientHistoricalOrders(int ClientId);

        [OperationContract]
        OrderData AddOrder(OrderData orderData);

        [OperationContract]
        OrderData GetOrder(int OrderId);

        [OperationContract]
        int SuspendOrder(int OrderId);

        [OperationContract]
        int CancelOrder(int OrderId);

        [OperationContract]
        int ActivateOrder(int OrderId);
    }

    public interface IOrderServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void OrderAdded(OrderData orderData);
    }
}
