using AbacasX.Model.DataContracts;
using AbacasX.Model.ViewModels;
using System.Collections.Generic;
using System.ServiceModel;


namespace AbacasX.Rate.Contracts
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IRateServiceCallBack))]
    interface IRateService
    {
        [OperationContract(IsOneWay = false)]
        void SubscribeToAssetRateUpdate(string AssetId);

        [OperationContract(IsOneWay = false)]
        void UnSubscribeToAssetRateUpdate(string AssetId);

        [OperationContract(IsOneWay = false)]
        void SubscribeToCurrencyPairRateUpdate(string Currency1, string Currency2);

        [OperationContract(IsOneWay = false)]
        void UnSubscribeToCurrencyPairRateUpdate(string Currency1, string Currency2);

        [OperationContract(IsOneWay = false)]
        void SubscribeToTokenPairRateUpdate(string Token1, string Token2);

        [OperationContract(IsOneWay = false)]
        void UnSubscribeToTokenPairRateUpdate(string Token, string Token2);

        [OperationContract(IsOneWay = false)]
        void SubscribeToTokenRateUpdate(string TokenId);

        [OperationContract(IsOneWay = false)]
        void UnSubscribeToTokenRateUpdate(string TokenId);

        [OperationContract(IsOneWay = false)]
        void UnSubscribeAllRateUpdates();

        [OperationContract(IsOneWay = false)]
        TokenDetail GetTokenDetail(string TokenId);

        [OperationContract]
        List<string> GetAssetList();

        [OperationContract]
        List<string> GetTokenList();

        [OperationContract]
        List<AssetRateData> GetAssetRateList();

        [OperationContract]
        List<TokenRateData> GetTokenRateList();

        [OperationContract]
        TokenPairRateData GetTokenPairRate(string Token1Id, string Token2Id);

        [OperationContract]
        TokenRateData GetTokenRate(string Token1Id);
    }

    public interface IRateServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void AssetRateUpdate(AssetRateData AssetRateRecord);

        [OperationContract(IsOneWay = true)]
        void CurrencyPairRateUpdate(CurrencyPairRateData CurrencyPairRateRecord);

        [OperationContract(IsOneWay = true)]
        void TokenRateUpdate(TokenRateData TokenRateRecord);

        [OperationContract(IsOneWay = true)]
        void TokenPairRateUpdate(TokenPairRateData TokenPairRateRecord);
    }
}
