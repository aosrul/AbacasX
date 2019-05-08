﻿using AbacasX.Model.ViewModels;
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
        void UnSubscribeToTokenPairRateUpdate(string Token1, string Token2);

        [OperationContract(IsOneWay = false)]
        void UnSubscribeAllRateUpdates();
    }

    public interface IRateServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void AssetRateUpdate(AssetRateData AssetRateRecord);

        [OperationContract(IsOneWay = true)]
        void CurrencyPairRateUpdate(CurrencyPairRateData CurrencyPairRateRecord);

        [OperationContract(IsOneWay = true)]
        void TokenPairRateUpdate(TokenPairRateData TokenPairRateRecord);
    }


}
