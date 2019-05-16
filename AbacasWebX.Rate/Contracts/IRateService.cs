﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AbacasWebX.Rate.Contracts
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

        [OperationContract]
        List<string> GetAssetList();

        [OperationContract]
        List<string> GetTokenList();

        [OperationContract]
        List<AssetRateData> GetAssetRateList();

        [OperationContract]
        List<TokenRateData> GetTokenRateList();
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