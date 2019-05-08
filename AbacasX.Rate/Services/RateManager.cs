using AbacasX.Model.Extensions;
using AbacasX.Model.ViewModels;
using AbacasX.Rate.Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace AbacasX.Rate.Services
{
   
    public class TokenPairRateListener
    {
        public HashSet<string> TokenPairSubscriptions = new HashSet<string>();
        IRateServiceCallBack RateServiceCallBack;
        public string SessionId;

        public TokenPairRateListener(IRateServiceCallBack callBack, string sessionId)
        {
            RateServiceCallBack = callBack;
            SessionId = sessionId;
        }

        public void PublishUpdate(TokenPairRateVM tokenPairRateRecord)
        {
            TokenPairRateData TokenPairRateDataRecord = new TokenPairRateData();

            tokenPairRateRecord.CopyPropertiesTo<TokenPairRateVM, TokenPairRateData>(TokenPairRateDataRecord);

            if (RateServiceCallBack != null)
            {
                try
                {
                    RateServiceCallBack.TokenPairRateUpdate(TokenPairRateDataRecord);
                }
                catch (Exception e)
                {
                    throw new Exception("Unable to Publish Token Pair Rate Update",e);
                }
            }
        }
    }


    [ServiceBehavior(UseSynchronizationContext = false, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    class RateManager : IRateService
    {
        public Dictionary<string, TokenPairRateListener> tokenPairSubscriptions = new Dictionary<string, TokenPairRateListener>();
        public Dictionary<string, AssetRateVM> assetRates;

        public RateManager()
        {
            initializeAssetRateRecords();
        }

        public void initializeAssetRateRecords()
        {
            assetRates = new Dictionary<string, AssetRateVM>();
        }

        #region Subscriptions

        public void SubscribeToAssetRateUpdate(string AssetId)
        {
            throw new System.NotImplementedException();
        }

        public void SubscribeToCurrencyPairRateUpdate(string Currency1, string Currency2)
        {
            throw new System.NotImplementedException();
        }

        public void SubscribeToTokenPairRateUpdate(string Token1, string Token2)
        {
            throw new System.NotImplementedException();
        }

        public void UnSubscribeAllRateUpdates()
        {
            throw new System.NotImplementedException();
        }

        public void UnSubscribeToAssetRateUpdate(string AssetId)
        {
            throw new System.NotImplementedException();
        }

        public void UnSubscribeToCurrencyPairRateUpdate(string Currency1, string Currency2)
        {
            throw new System.NotImplementedException();
        }

        public void UnSubscribeToTokenPairRateUpdate(string Token1, string Token2)
        {
            throw new System.NotImplementedException();
        }

        #endregion





    }
}
