using AbacasWebX.Rate.Contracts;
using AbacasX.Model.Extensions;
using AbacasX.Model.Models;
using AbacasX.Model.ViewModels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.ServiceModel;
using System.Threading;
using System.Web;

namespace AbacasWebX.Rate.Services
{
    #region Asset Rate Listener
    // Asset Rate Listener
    public class AssetRateListener
    {
        public string SessionId;
        public IRateServiceCallBack rateUpdateCallBack;
        public bool connected;
        public IDisposable disposable;

        public AssetRateListener(IRateServiceCallBack callBack, string sessionId)
        {
            rateUpdateCallBack = callBack;
            SessionId = sessionId;
            connected = true;
        }

        public void PublishUpdate(AssetRate assetRateRecord)
        {
            AssetRateData assetRateData = new AssetRateData();

            // Copy data fields from Asset Rate Record to Transport Equivalent
            assetRateRecord.CopyPropertiesTo(assetRateData);

            if (((ICommunicationObject)rateUpdateCallBack).State == CommunicationState.Closed ||
                ((ICommunicationObject)rateUpdateCallBack).State == CommunicationState.Faulted)
            {
                connected = false;
            }

            if ((rateUpdateCallBack != null) && connected)
            {
                try
                {
                    rateUpdateCallBack.AssetRateUpdate(assetRateData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception on Rate Update to client {0}", ex.Message);
                    connected = false;
                }
            }
        }
    }
    #endregion

    #region Currency Pair Listener
    // Currency Pair Rate Listener

    public class CurrencyPairRateListener
    {
        public HashSet<string> CurrencyPairSubscriptions = new HashSet<string>();
        public string SessionId;
        public IRateServiceCallBack rateUpdateCallBack;
        public bool connected;
        public IDisposable disposable;

        public CurrencyPairRateListener(IRateServiceCallBack callBack, string sessionId)
        {
            rateUpdateCallBack = callBack;
            SessionId = sessionId;
            connected = true;
        }

        public void PublishUpdate(CurrencyPairRateData currencyPairRateRecord)
        {
            if (((ICommunicationObject)rateUpdateCallBack).State == CommunicationState.Closed ||
                ((ICommunicationObject)rateUpdateCallBack).State == CommunicationState.Faulted)
            {
                connected = false;
            }

            if ((rateUpdateCallBack != null) && connected)
            {
                try
                {
                    rateUpdateCallBack.CurrencyPairRateUpdate(currencyPairRateRecord);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception on Currency Pair Rate Update to client {0}", ex.Message);
                    connected = false;
                }
            }
        }
    }

    #endregion

    #region Token Rate Listener
    // Token Rate Listener

    public class TokenRateListener
    {
        public HashSet<string> TokenSubscriptions = new HashSet<string>();
        public string SessionId;
        public IRateServiceCallBack rateUpdateCallBack;
        public bool connected;
        public IDisposable disposable;

        public TokenRateListener(IRateServiceCallBack callBack, string sessionId)
        {
            rateUpdateCallBack = callBack;
            SessionId = sessionId;
            connected = true;
        }

        public void PublishUpdate(TokenRateData tokenRateRecord)
        {
            if (((ICommunicationObject)rateUpdateCallBack).State == CommunicationState.Closed ||
                ((ICommunicationObject)rateUpdateCallBack).State == CommunicationState.Faulted)
            {
                connected = false;
            }

            if ((rateUpdateCallBack != null) && connected)
            {
                try
                {
                    rateUpdateCallBack.TokenRateUpdate(tokenRateRecord);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception on Token Rate Update to client {0}", ex.Message);
                    connected = false;
                }
            }
        }
    }

    #endregion

    #region Token Pair Listener
    public class TokenPairRateListener
    {
        public HashSet<string> TokenPairSubscriptions = new HashSet<string>();
        IRateServiceCallBack rateUpdateCallBack;
        public string SessionId;
        public bool connected;

        public TokenPairRateListener(IRateServiceCallBack callBack, string sessionId)
        {
            rateUpdateCallBack = callBack;
            SessionId = sessionId;
            connected = true;
        }

        public void PublishUpdate(TokenPairRateData tokenPairRateRecord)
        {
            if (((ICommunicationObject)rateUpdateCallBack).State == CommunicationState.Closed ||
                ((ICommunicationObject)rateUpdateCallBack).State == CommunicationState.Faulted)
            {
                connected = false;
            }

            if ((rateUpdateCallBack != null) && connected)
            {
                try
                {
                    rateUpdateCallBack.TokenPairRateUpdate(tokenPairRateRecord);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception on Token Pair Rate Update to client {0}", ex.Message);
                    connected = false;
                }
            }
        }
    }
    #endregion

    /// <summary>
    /// The TokenRateViewModel manages the subscriptions on an individual token rate as well as subscribing
    /// to the base asset rate of the token for rate changes.  Each rate change is applied within the token by multiplying 
    /// by the defined multiple within the token definition.
    /// </summary>
    public class TokenRateViewModel
    {
        Subject<TokenRateData> _subject;
        TokenRateData tokenRateRecord;
        IDisposable disposeOfAssetSubscription;

        public TokenRateViewModel(string tokenId, AssetRate assetRateRecord, IObservable<AssetRate> rateSubscription)
        {
            // Create Subscription 
            _subject = new Subject<TokenRateData>();

            // Initialize the Token Rate Record from the Asset Rate Record
            assetRateRecord.CopyPropertiesTo(tokenRateRecord);

            tokenRateRecord.TokenId = tokenId;
            tokenRateRecord.Multiplier = 1.0;
            tokenRateRecord.AssetBidRate = assetRateRecord.BidRate;
            tokenRateRecord.AssetAskRate = assetRateRecord.AskRate;
            tokenRateRecord.BidRate = tokenRateRecord.AssetBidRate * tokenRateRecord.Multiplier;
            tokenRateRecord.AskRate = tokenRateRecord.AssetAskRate * tokenRateRecord.Multiplier;

            if (assetRateRecord.RateTerms == RateTermsEnum.AssetPerCurrency)
                tokenRateRecord.RateTerms = TokenRateTermsEnum.TokenPerCurrency;
            else
                tokenRateRecord.RateTerms = TokenRateTermsEnum.CurrencyPerToken;


            disposeOfAssetSubscription = rateSubscription.Subscribe(updateRateData);
        }


        public IDisposable subscribe()
        {
            return _subject.Subscribe();
        }

        public void dispose()
        {
            // Dispose of the Asset Rate Updates
            disposeOfAssetSubscription.Dispose();

            // Dispose of the Token Rate publishing
            _subject.Dispose();
        }

        public void updateRateData(AssetRate assetRateRecord)
        {
            tokenRateRecord.AssetBidRate = assetRateRecord.BidRate;
            tokenRateRecord.AssetAskRate = assetRateRecord.AskRate;
            tokenRateRecord.BidRate = tokenRateRecord.AssetBidRate * tokenRateRecord.Multiplier;
            tokenRateRecord.AskRate = tokenRateRecord.AssetAskRate * tokenRateRecord.Multiplier;

            // Publish updates on the Token Rates
            _subject.OnNext(tokenRateRecord);
        }
    }


    /// <summary>
    /// The CurrencyPairRateViewModel manages a subscription if two asset rates (one for each currency).
    /// If one of the currencies in the pair is USD, then the currency pair is really just an asset rate listener.
    /// 
    /// If both of the currencies are not USD, then both currency asset rates are monitored and the currency pair rate
    /// published as updates are received.
    /// 
    /// </summary>
    public class CurrencyPairRateViewModel
    {

    }

    /// <summary>
    /// The TokenPairRateViewModel watches two token rates and the associated currency rates in the event that 
    /// the tokens do not use the same base asset rate.
    /// If only one of the tokens is priced in a non USD currency, then only 1 FX rate must be monitored.
    /// If both of the tokens are priced in a non USD currency, then the FX cross rate is monitored and 
    /// incorporated into the Token Pair rate updates.
    /// </summary>
    public class TokenPairRateViewModel
    {

    }

    public class TokenRateManager
    {
    }

    public class CurrencyPairRateManager
    {


    }

    public class TokenPairRateManager
    {



    }


    [ServiceBehavior(UseSynchronizationContext = false, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RateManager : IRateService
    {
        List<string> assetList;
        List<string> tokenList;
        List<TokenRateData> tokenRateDataList;

        TokenRateManager tokenRateManager = new TokenRateManager();

        ConcurrentDictionary<string, AssetRate> assetRateList = new ConcurrentDictionary<string, AssetRate>();

        static public Dictionary<string, AssetRateListener> assetRateSubscriptions = new Dictionary<string, AssetRateListener>();
        static public Dictionary<string, TokenRateListener> tokenRateSubscriptions = new Dictionary<string, TokenRateListener>();
        static public Dictionary<string, CurrencyPairRateListener> currencyPairSubscriptions = new Dictionary<string, CurrencyPairRateListener>();
        static public Dictionary<string, TokenPairRateListener> tokenPairSubscriptions = new Dictionary<string, TokenPairRateListener>();


        static public object AssetRateListenerLock = new object();
        static public object AssetListLock = new object();

        static public object TokenRateListenerLock = new object();
        static public object TokenListLock = new object();

        static public object CurrencyPairRateListenerLock = new object();
        static public object CurrencyPairListLock = new object();

        static public object TokenPairRateListenerLock = new object();
        static public object TokenPairPairListLock = new object();


        private readonly Random _updateOrNotRandom = new Random();
        private readonly Random random = new Random();
        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(150);
        private readonly Timer _timer;
        private Subject<AssetRate> _subject = new Subject<AssetRate>();
        private volatile bool _updatingAssetPrices = false;

        public RateManager()
        {
            assetRateList = new ConcurrentDictionary<string, AssetRate>();

            assetRateList.TryAdd("AAPL", new AssetRate { AssetId = "AAPL", PriceCurrency = "USD", RateTerms = AbacasX.Model.Models.RateTermsEnum.AssetPerCurrency, BidRate = 211, AskRate = 211 });
            assetRateList.TryAdd("GOOG", new AssetRate { AssetId = "GOOG", PriceCurrency = "USD", RateTerms = AbacasX.Model.Models.RateTermsEnum.AssetPerCurrency, BidRate = 1165, AskRate = 1165 });
            assetRateList.TryAdd("MSFT", new AssetRate { AssetId = "MSFT", PriceCurrency = "USD", RateTerms = AbacasX.Model.Models.RateTermsEnum.AssetPerCurrency, BidRate = 127, AskRate = 127 });
            assetRateList.TryAdd("GOLD", new AssetRate { AssetId = "GOLD", PriceCurrency = "USD", RateTerms = AbacasX.Model.Models.RateTermsEnum.AssetPerCurrency, BidRate = 1270, AskRate = 1270 });
            assetRateList.TryAdd("BTC", new AssetRate { AssetId = "BTC", PriceCurrency = "USD", RateTerms = AbacasX.Model.Models.RateTermsEnum.AssetPerCurrency, BidRate = 5362, AskRate = 5362 });
            assetRateList.TryAdd("USD", new AssetRate { AssetId = "USD", PriceCurrency = "USD", RateTerms = AbacasX.Model.Models.RateTermsEnum.AssetPerCurrency, BidRate = 1, AskRate = 1 });
            assetRateList.TryAdd("ETH", new AssetRate { AssetId = "ETH", PriceCurrency = "USD", RateTerms = AbacasX.Model.Models.RateTermsEnum.AssetPerCurrency, BidRate = 160, AskRate = 160 });
            assetRateList.TryAdd("BNP", new AssetRate { AssetId = "BNP", PriceCurrency = "EUR", RateTerms = AbacasX.Model.Models.RateTermsEnum.AssetPerCurrency, BidRate = 48, AskRate = 48 });
            assetRateList.TryAdd("EUR", new AssetRate { AssetId = "EUR", PriceCurrency = "USD", RateTerms = AbacasX.Model.Models.RateTermsEnum.CurrencyPerAsset, BidRate = 1.12, AskRate = 1.12 });


            // Create list of token rates, and asset Ids from asset rate list
            tokenRateDataList = new List<TokenRateData>();
            assetList = new List<string>();
            tokenList = new List<string>();

            foreach (AssetRate a in assetRateList.Values)
            {
                TokenRateData tokenRateRecord = new TokenRateData();

                assetList.Add(a.AssetId);

                a.CopyPropertiesTo(tokenRateRecord);

                tokenRateRecord.TokenId = '@' + a.AssetId;
                tokenRateRecord.Multiplier = 1.0;
                tokenRateRecord.AssetBidRate = a.BidRate;
                tokenRateRecord.AssetAskRate = a.AskRate;
                tokenRateRecord.BidRate = a.BidRate * tokenRateRecord.Multiplier;
                tokenRateRecord.AskRate = a.AskRate * tokenRateRecord.Multiplier;

                if (a.RateTerms == RateTermsEnum.AssetPerCurrency)
                    tokenRateRecord.RateTerms = TokenRateTermsEnum.TokenPerCurrency;
                else
                    tokenRateRecord.RateTerms = TokenRateTermsEnum.CurrencyPerToken;

                tokenRateDataList.Add(tokenRateRecord);

                tokenList.Add(tokenRateRecord.TokenId);
            }

            _timer = new Timer(UpdateAssetPrices, null, _updateInterval, _updateInterval);
        }

        private void UpdateAssetPrices(object state)
        {
            lock (AssetListLock)
            {
                if (!_updatingAssetPrices)
                {
                    _updatingAssetPrices = true;

                    foreach (var assetRateRecord in assetRateList.Values)
                    {
                        TryUpdateAssetPrice(assetRateRecord);
                        _subject.OnNext(assetRateRecord);
                        //Console.WriteLine("Update Asset Price for {0} to {1}/{2}", assetRateRecord.AssetId, assetRateRecord.BidRate, assetRateRecord.AskRate);
                    }

                    _updatingAssetPrices = false;
                }
            }
        }

        private bool TryUpdateAssetPrice(AssetRate assetRateRecord)
        {
            // Randomly choose whether to update this stock or not
            var r = _updateOrNotRandom.NextDouble();

            if (r > .1)
            {
                return false;
            }
            double marketRatePoint = Math.Pow(10.0, -4.0);
            double marketRateDelta = 3.0 * marketRatePoint;
            double marketRateDeltaHalf = marketRateDelta + (0.5 * marketRatePoint);

            // Positive or Negative move
            if (random.Next(100) < 50)
            {
                // Negative Move in Price


                // If < 50, then the Bid is changed, otherwise the Ask is Changed
                if (random.Next(100) < 50)
                {
                    assetRateRecord.BidRate -= random.Next(100) * .01 * marketRatePoint;


                    if (assetRateRecord.BidRate < (assetRateRecord.AskRate - marketRateDelta))
                    {
                        assetRateRecord.AskRate = assetRateRecord.BidRate + marketRateDeltaHalf;
                    }
                }
                // Ask Rate Changed
                else
                {
                    assetRateRecord.AskRate -= random.Next(100) * .01 * marketRatePoint;


                    if ((assetRateRecord.AskRate - marketRateDelta) < assetRateRecord.BidRate)
                    {
                        assetRateRecord.BidRate = assetRateRecord.AskRate - marketRateDeltaHalf;
                    }
                }
            }

            else
            {
                // Positive Move in Price

                if (random.Next(100) < 50)
                {
                    assetRateRecord.BidRate += random.Next(100) * .01 * marketRatePoint;

                    if ((assetRateRecord.BidRate + marketRateDelta) > assetRateRecord.AskRate)
                    {
                        assetRateRecord.AskRate = assetRateRecord.BidRate + marketRateDeltaHalf;
                    }
                }
                else
                {
                    assetRateRecord.AskRate += random.Next(100) * .01 * marketRatePoint;

                    if (assetRateRecord.BidRate < (assetRateRecord.AskRate - marketRateDelta))
                    {
                        assetRateRecord.BidRate = assetRateRecord.AskRate - marketRateDeltaHalf;
                    }
                }
            }

            return true;
        }

        #region Rate List Retrieval

        public List<string> GetAssetList()
        {
            return assetList;
        }

        public List<AssetRateData> GetAssetRateList()
        {
            List<AssetRateData> assetRateDataList = new List<AssetRateData>();

            foreach (AssetRate a in assetRateList.Values)
            {
                AssetRateData assetRateDataRecord = new AssetRateData();
                a.CopyPropertiesTo(assetRateDataRecord);
                assetRateDataList.Add(assetRateDataRecord);
            }

            return assetRateDataList;
        }

        public List<string> GetTokenList()
        {
            return tokenList;
        }

        public List<TokenRateData> GetTokenRateList()
        {
            return tokenRateDataList;
        }
        #endregion

        #region Subscription Section

        public void SubscribeToAssetRateUpdate(string AssetId)
        {
            AssetRateListener assetRateListenerRecord;

            lock (AssetRateListenerLock)
            {
                if (assetRateSubscriptions.TryGetValue(OperationContext.Current.SessionId, out assetRateListenerRecord) == false)
                {
                    IRateServiceCallBack callBack = OperationContext.Current.GetCallbackChannel<IRateServiceCallBack>();

                    assetRateListenerRecord = new AssetRateListener(OperationContext.Current.GetCallbackChannel<IRateServiceCallBack>(), OperationContext.Current.SessionId);
                    assetRateListenerRecord.disposable = _subject.Subscribe(assetRateListenerRecord.PublishUpdate);
                    assetRateSubscriptions.Add(OperationContext.Current.SessionId, assetRateListenerRecord);
                }
            }
        }

        public void SubscribeToCurrencyPairRateUpdate(string Currency1, string Currency2)
        {
            String CurrencyPairKey = (Currency1.CompareTo(Currency2) < 0) ? Currency1.Trim() + Currency2.Trim() : Currency2.Trim() + Currency1.Trim();

            throw new NotImplementedException();
        }

        public void SubscribeToTokenPairRateUpdate(string Token1, string Token2)
        {
            String TokenPairKey = (Token1.CompareTo(Token2) < 0) ? Token1.Trim() + Token2.Trim() : Token2.Trim() + Token1.Trim();


            throw new NotImplementedException();
        }

        public void SubscribeToTokenRateUpdate(string TokenId)
        {
            throw new NotImplementedException();
        }

        public void UnSubscribeAllRateUpdates()
        {
            throw new NotImplementedException();
        }

        public void UnSubscribeToAssetRateUpdate(string AssetId)
        {
            throw new NotImplementedException();
        }

        public void UnSubscribeToCurrencyPairRateUpdate(string Currency1, string Currency2)
        {
            throw new NotImplementedException();
        }

        public void UnSubscribeToTokenPairRateUpdate(string Token1, string Token2)
        {
            throw new NotImplementedException();
        }

        public void UnSubscribeToTokenRateUpdate(string TokenId)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}