using AbacasX.Model.DataContracts;
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
        decimal GetClientTokenBalance(int ClientId, string TokenId);

        [OperationContract]
        List<OrderData> GetClientOrders(int ClientId);

        [OperationContract]
        List<OrderData> GetClientHistoricalOrders(int ClientId);

        [OperationContract]
        List<AssetTransferData> GetClientTransferActivity(int ClientId);

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

        [OperationContract]
        string GetNewGuid();

        [OperationContract]
        AssetDepositData AddDeposit(AssetDepositData depositNotification);

        [OperationContract]
        AssetWithdrawalData AddWithdrawal(AssetWithdrawalData withdrawalRequest);
    }

    public interface IOrderServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void OrderAdded(OrderData orderData);

        [OperationContract(IsOneWay = true)]
        void DepositAdded(AssetDepositData depositData);

        [OperationContract(IsOneWay = true)]
        void WithdrawalAdded(AssetWithdrawalData withdrawalData);

    }
}
