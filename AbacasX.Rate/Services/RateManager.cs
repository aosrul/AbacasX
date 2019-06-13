using AbacasX.Model.DataContracts;
using AbacasX.Model.Extensions;
using AbacasX.Model.Models;
using AbacasX.Model.ViewModels;
using AbacasX.Rate.Contracts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.ServiceModel;
using System.Threading;

namespace AbacasX.Rate.Services
{
    #region Asset Rate Listener
    // Asset Rate Listener

    public class AssetSubscription
    {
        public string AssetId { get; set; }
        public IDisposable disposable;
    }

    public class AssetRateListener
    {
        public ConcurrentDictionary<string, AssetSubscription> assetSubscriptions = new ConcurrentDictionary<string, AssetSubscription>();
        public string SessionId;
        public IRateServiceCallBack rateUpdateCallBack;
        public bool connected;


        public AssetRateListener(IRateServiceCallBack callBack, string sessionId)
        {
            rateUpdateCallBack = callBack;
            SessionId = sessionId;
            connected = true;
        }

        public void PublishUpdate(AssetRateData assetRateDataRecord)
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
                    rateUpdateCallBack.AssetRateUpdate(assetRateDataRecord);
                }
                catch (Exception ex)
                {
                    connected = false;
                    throw new Exception(string.Format("Asset Rate Update {0}", ex.Message), ex);
                }
            }
        }

    }
    #endregion

    #region Token Rate Listener
    // Token Rate Listener

    public class TokenSubscription
    {
        public string TokenId { get; set; }
        public IDisposable disposable;
    }

    public class TokenRateListener
    {
        public ConcurrentDictionary<string, TokenSubscription> tokenSubscriptions = new ConcurrentDictionary<string, TokenSubscription>();
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
                    connected = false;
                    throw new Exception(string.Format("Exception on Token Rate Update to client {0}", ex.Message), ex);
                }
            }
        }
    }

    #endregion

    #region Currency Pair Listener
    // Currency Pair Rate Listener

    public class CurrencyPairSubscription
    {
        public string CurrencyPair { get; set; }
        public IDisposable disposable;
    }

    public class CurrencyPairRateListener
    {
        public ConcurrentDictionary<string, CurrencyPairSubscription> currencyPairSubscriptions = new ConcurrentDictionary<string, CurrencyPairSubscription>();
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
                    connected = false;
                    throw new Exception(String.Format("Exception on Currency Pair Rate Update to client {0}", ex.Message),ex);
                }
            }
        }
    }

    #endregion

    #region Token Pair Listener

    public class TokenPairSubscription
    {
        public string TokenPair { get; set; }
        public IDisposable disposable;
    }

    public class TokenPairRateListener
    {
        public ConcurrentDictionary<string, TokenPairSubscription> tokenPairSubscriptions = new ConcurrentDictionary<string, TokenPairSubscription>();
        public string SessionId;
        public IRateServiceCallBack rateUpdateCallBack;
        public bool connected;
        public IDisposable disposable;

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
                throw new Exception("No Rate Callback connection found");
            }

            if ((rateUpdateCallBack != null) && connected)
            {
                try
                {
                    rateUpdateCallBack.TokenPairRateUpdate(tokenPairRateRecord);
                }
                catch (Exception ex)
                {
                    connected = false;
                    throw new Exception(string.Format("Exception on Token Pair Rate Update {0}", ex.Message), ex);
                }
            }
        }
    }
    #endregion

    public class AssetRateViewModel : IDisposable
    {
        public Subject<AssetRateData> _subject;
        public AssetRateData assetRateDataRecord = new AssetRateData();
        IDisposable disposeOfAssetSubscription;

        public AssetRateViewModel(string assetId, AssetRate assetRateRecord, IObservable<AssetRate> rateSubscription)
        {
            // Create Subscription 
            _subject = new Subject<AssetRateData>();

            // Initialize the Token Rate Record from the Asset Rate Record
            assetRateRecord.CopyPropertiesTo(assetRateDataRecord);

            disposeOfAssetSubscription = rateSubscription.Subscribe(updateRateData);
        }

        public IDisposable subscribe()
        {
            return _subject.Subscribe();
        }

        public void Dispose()
        {
            // Dispose of the Asset Rate Updates
            disposeOfAssetSubscription.Dispose();

            // Dispose of the Token Rate publishing
            _subject.Dispose();
        }

        public void updateRateData(AssetRate assetRateRecord)
        {
            if (assetRateRecord.AssetId == assetRateDataRecord.AssetId)
            {
                assetRateDataRecord.BidRate = assetRateRecord.BidRate;
                assetRateDataRecord.AskRate = assetRateRecord.AskRate;

                // Publish updates on the Token Rates
                _subject.OnNext(assetRateDataRecord);
            }
        }
    }

    /// <summary>
    /// The TokenRateViewModel manages the subscriptions on an individual token rate as well as subscribing
    /// to the base asset rate of the token for rate changes.  Each rate change is applied within the token by multiplying 
    /// by the defined multiple within the token definition.
    /// </summary>
    public class TokenRateViewModel : IDisposable
    {
        public Subject<TokenRateData> _subject;
        public TokenRateData tokenRateRecord = new TokenRateData();
        public IDisposable disposeOfAssetSubscription;
        public string assetId;

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
            assetId = assetRateRecord.AssetId;

            disposeOfAssetSubscription = rateSubscription.Subscribe(updateRateData);
        }

        public IDisposable subscribe()
        {
            return _subject.Subscribe();
        }

        public void Dispose()
        {
            // Dispose of the Asset Rate Updates
            disposeOfAssetSubscription.Dispose();

            // Dispose of the Token Rate publishing
            _subject.Dispose();
        }

        public void updateRateData(AssetRate assetRateRecord)
        {
            if (assetRateRecord.AssetId == assetId)
            {

                tokenRateRecord.AssetBidRate = assetRateRecord.BidRate;
                tokenRateRecord.AssetAskRate = assetRateRecord.AskRate;
                tokenRateRecord.BidRate = tokenRateRecord.AssetBidRate * tokenRateRecord.Multiplier;
                tokenRateRecord.AskRate = tokenRateRecord.AssetAskRate * tokenRateRecord.Multiplier;

                // Publish updates on the Token Rates
                _subject.OnNext(tokenRateRecord);
            }
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
    public class CurrencyPairRateViewModel : IDisposable
    {
        public Subject<CurrencyPairRateData> _subject;
        public CurrencyPairRateData currencyPairRateRecord = new CurrencyPairRateData();

        public IDisposable disposeOfCurrency1Subscription;
        public IDisposable disposeOfCurrency2Subscription;

        private AssetRateData currency1RateRecord = new AssetRateData();
        private AssetRateData currency2RateRecord = new AssetRateData();


        public CurrencyPairRateViewModel(string Currency1, string Currency2, AssetRateData asset1RateRecord, AssetRateData asset2RateRecord, IObservable<AssetRateData> currency1RateSubscription, IObservable<AssetRateData> currency2RateSubscription)
        {
            _subject = new Subject<CurrencyPairRateData>();

            asset1RateRecord.CopyPropertiesTo(currency1RateRecord);
            asset2RateRecord.CopyPropertiesTo(currency2RateRecord);

            currencyPairRateRecord.Currency1 = asset1RateRecord.AssetId;
            currencyPairRateRecord.Currency1BidRate = asset1RateRecord.BidRate;
            currencyPairRateRecord.Currency1AskRate = asset1RateRecord.AskRate;
            currencyPairRateRecord.Currency1RateTerms = asset1RateRecord.RateTerms;

            currencyPairRateRecord.Currency2 = asset2RateRecord.AssetId;
            currencyPairRateRecord.Currency2BidRate = asset2RateRecord.BidRate;
            currencyPairRateRecord.Currency2AskRate = asset2RateRecord.AskRate;
            currencyPairRateRecord.Currency2RateTerms = asset2RateRecord.RateTerms;

            // Cross Currency Rate will be in Currency2 / Currency1 terms
            currencyPairRateRecord.RateTerms = CurrencyPairRateTermsEnum.Currency2PerCurrency1;

            // Recalculate the cross currency rates
            RecalculateRates();

            disposeOfCurrency1Subscription = currency1RateSubscription.Subscribe(UpdateAssetRate);
            disposeOfCurrency2Subscription = currency2RateSubscription.Subscribe(UpdateAssetRate);

        }

        public void RecalculateRates()
        {
            if (currencyPairRateRecord.RateTerms == CurrencyPairRateTermsEnum.Currency2PerCurrency1)
            {
                currencyPairRateRecord.BidRate =
                    (currencyPairRateRecord.Currency1RateTerms == RateTermsEnum.CurrencyPerAsset ? currencyPairRateRecord.Currency1BidRate : 1.0 / currencyPairRateRecord.Currency1AskRate) *
                    (currencyPairRateRecord.Currency2RateTerms == RateTermsEnum.AssetPerCurrency ? currencyPairRateRecord.Currency2BidRate : 1.0 / currencyPairRateRecord.Currency2AskRate);

                currencyPairRateRecord.AskRate =
                    (currencyPairRateRecord.Currency1RateTerms == RateTermsEnum.CurrencyPerAsset ? currencyPairRateRecord.Currency1AskRate : 1.0 / currencyPairRateRecord.Currency1BidRate) *
                    (currencyPairRateRecord.Currency2RateTerms == RateTermsEnum.AssetPerCurrency ? currencyPairRateRecord.Currency2AskRate : 1.0 / currencyPairRateRecord.Currency2BidRate);
            }
            else
            {
                currencyPairRateRecord.BidRate =
                    (currencyPairRateRecord.Currency1RateTerms == RateTermsEnum.AssetPerCurrency ? currencyPairRateRecord.Currency1BidRate : 1.0 / currencyPairRateRecord.Currency1AskRate) *
                    (currencyPairRateRecord.Currency2RateTerms == RateTermsEnum.CurrencyPerAsset ? currencyPairRateRecord.Currency2BidRate : 1.0 / currencyPairRateRecord.Currency2AskRate);

                currencyPairRateRecord.AskRate =
                    (currencyPairRateRecord.Currency1RateTerms == RateTermsEnum.AssetPerCurrency ? currencyPairRateRecord.Currency1AskRate : 1.0 / currencyPairRateRecord.Currency1BidRate) *
                    (currencyPairRateRecord.Currency2RateTerms == RateTermsEnum.CurrencyPerAsset ? currencyPairRateRecord.Currency2AskRate : 1.0 / currencyPairRateRecord.Currency2BidRate);
            }
        }

        public void UpdateAssetRate(AssetRateData assetRateRecord)
        {
            if (assetRateRecord.AssetId == currency1RateRecord.AssetId)
            {
                currency1RateRecord.BidRate = assetRateRecord.BidRate;
                currency1RateRecord.AskRate = assetRateRecord.AskRate;
            }
            else if (assetRateRecord.AssetId == currency2RateRecord.AssetId)
            {
                currency2RateRecord.BidRate = assetRateRecord.BidRate;
                currency2RateRecord.AskRate = assetRateRecord.AskRate;
            }

            RecalculateRates();

            publishCurrencyPairUpdate();
        }

        private void publishCurrencyPairUpdate()
        {
            _subject.OnNext(currencyPairRateRecord);
        }

        public IDisposable subscribe()
        {
            return _subject.Subscribe();
        }

        public void Dispose()
        {
            // Dispose of the Token Rate publishing
            _subject.Dispose();

            // Dispose of the Asset Rate Updates
            disposeOfCurrency1Subscription.Dispose();
            disposeOfCurrency2Subscription.Dispose();
        }
    }

    /// <summary>
    /// The TokenPairRateViewModel watches two token rates and the associated currency rates in the event that 
    /// the tokens do not use the same base asset rate.
    /// If only one of the tokens is priced in a non USD currency, then only 1 FX rate must be monitored.
    /// If both of the tokens are priced in a non USD currency, then the FX cross rate is monitored and 
    /// incorporated into the Token Pair rate updates.
    /// </summary>
    public class TokenPairRateViewModel : IDisposable
    {
        public Subject<TokenPairRateData> _subject;
        public TokenPairRateData tokenPairRateRecord = new TokenPairRateData();

        public IDisposable disposeOfToken1Subscription;
        public IDisposable disposeOfToken2Subscription;
        public IDisposable disposeOfCurrencyPairSubscription;

        private TokenRateData token1RateRecord = new TokenRateData();
        private TokenRateData token2RateRecord = new TokenRateData();
        private CurrencyPairRateData currencyPairRateRecord = new CurrencyPairRateData();


        public TokenPairRateViewModel(string TokenId1, string TokenId2, TokenRateManager tokenRateManager, CurrencyPairRateManager currencyPairRateManager)
        {
            _subject = new Subject<TokenPairRateData>();
            TokenRateViewModel token1RateViewModelRecord;
            TokenRateViewModel token2RateViewModelRecord;
            CurrencyPairRateViewModel currencyPairRateViewModelRecord;


            if (tokenRateManager.tokenRateList.TryGetValue(TokenId1, out token1RateViewModelRecord) == true)
            {
                if (tokenRateManager.tokenRateList.TryGetValue(TokenId2, out token2RateViewModelRecord) == true)
                {
                    string currency1 = token1RateViewModelRecord.tokenRateRecord.PriceCurrency;
                    string currency2 = token2RateViewModelRecord.tokenRateRecord.PriceCurrency;

                    // Find or Define a currency pair rate record
                    if ((currencyPairRateViewModelRecord = currencyPairRateManager.GetCurrencyPairRateView(currency1, currency2)) != null)
                    {
                        token1RateViewModelRecord.tokenRateRecord.CopyPropertiesTo(token1RateRecord);
                        token2RateViewModelRecord.tokenRateRecord.CopyPropertiesTo(token2RateRecord);
                        currencyPairRateViewModelRecord.currencyPairRateRecord.CopyPropertiesTo(currencyPairRateRecord);

                        tokenPairRateRecord.Token1Id = token1RateRecord.TokenId;
                        tokenPairRateRecord.Token1BidRate = token1RateRecord.BidRate;
                        tokenPairRateRecord.Token1AskRate = token1RateRecord.AskRate;
                        tokenPairRateRecord.Token1RateTerms = token1RateRecord.RateTerms;
                        tokenPairRateRecord.Currency1 = token1RateRecord.PriceCurrency;

                        tokenPairRateRecord.Token2Id = token2RateRecord.TokenId;
                        tokenPairRateRecord.Token2BidRate = token2RateRecord.BidRate;
                        tokenPairRateRecord.Token2AskRate = token2RateRecord.AskRate;
                        tokenPairRateRecord.Token2RateTerms = token2RateRecord.RateTerms;
                        tokenPairRateRecord.Currency2 = token2RateRecord.PriceCurrency;

                        tokenPairRateRecord.Currency1BidRate = currencyPairRateRecord.Currency1BidRate;
                        tokenPairRateRecord.Currency1AskRate = currencyPairRateRecord.Currency1AskRate;
                        tokenPairRateRecord.Currency1RateTerms = currencyPairRateRecord.Currency1RateTerms;

                        tokenPairRateRecord.Currency2BidRate = currencyPairRateRecord.Currency2BidRate;
                        tokenPairRateRecord.Currency2AskRate = currencyPairRateRecord.Currency2AskRate;
                        tokenPairRateRecord.Currency2RateTerms = currencyPairRateRecord.Currency2RateTerms;

                        tokenPairRateRecord.CurrencyPairBidRate = currencyPairRateRecord.BidRate;
                        tokenPairRateRecord.CurrencyPairAskRate = currencyPairRateRecord.AskRate;

                        tokenPairRateRecord.RateTerms = TokenPairRateTermsEnum.Token2PerToken1;

                        RecalculateRates();

                        disposeOfToken1Subscription = token1RateViewModelRecord._subject.Subscribe(UpdateTokenRate);
                        disposeOfToken2Subscription = token2RateViewModelRecord._subject.Subscribe(UpdateTokenRate);
                        disposeOfCurrencyPairSubscription = currencyPairRateViewModelRecord._subject.Subscribe(UpdateCurrencyPairRate);
                    }
                }
            }
        }

        public void RecalculateRates()
        {
            if (tokenPairRateRecord.RateTerms == TokenPairRateTermsEnum.Token1PerToken2)
            {
                tokenPairRateRecord.BidRate = (tokenPairRateRecord.Token1RateTerms == TokenRateTermsEnum.TokenPerCurrency ?
                                                    tokenPairRateRecord.Token1BidRate : 1.0 / tokenPairRateRecord.Token1AskRate) *
                                               (tokenPairRateRecord.Token2RateTerms == TokenRateTermsEnum.CurrencyPerToken ?
                                                    tokenPairRateRecord.Token2BidRate : 1.0 / tokenPairRateRecord.Token2AskRate) *
                                               (currencyPairRateRecord.RateTerms == CurrencyPairRateTermsEnum.Currency1PerCurrency2 ?
                                                currencyPairRateRecord.BidRate : 1.0 / currencyPairRateRecord.AskRate);

                tokenPairRateRecord.AskRate = (tokenPairRateRecord.Token1RateTerms == TokenRateTermsEnum.TokenPerCurrency ?
                                                    tokenPairRateRecord.Token1AskRate : 1.0 / tokenPairRateRecord.Token1BidRate) *
                                               (tokenPairRateRecord.Token2RateTerms == TokenRateTermsEnum.CurrencyPerToken ?
                                                    tokenPairRateRecord.Token2AskRate : 1.0 / tokenPairRateRecord.Token2BidRate) *
                                               (currencyPairRateRecord.RateTerms == CurrencyPairRateTermsEnum.Currency1PerCurrency2 ?
                                                currencyPairRateRecord.AskRate : 1.0 / currencyPairRateRecord.BidRate);
            }
            // Token2 Per Token1
            else
            {
                tokenPairRateRecord.BidRate = (tokenPairRateRecord.Token1RateTerms == TokenRateTermsEnum.CurrencyPerToken ?
                                                    tokenPairRateRecord.Token1BidRate : 1.0 / tokenPairRateRecord.Token1AskRate) *
                                               (tokenPairRateRecord.Token2RateTerms == TokenRateTermsEnum.TokenPerCurrency ?
                                                    tokenPairRateRecord.Token2BidRate : 1.0 / tokenPairRateRecord.Token2AskRate) *
                                               (currencyPairRateRecord.RateTerms == CurrencyPairRateTermsEnum.Currency2PerCurrency1 ?
                                                currencyPairRateRecord.BidRate : 1.0 / currencyPairRateRecord.AskRate);

                tokenPairRateRecord.AskRate = (tokenPairRateRecord.Token1RateTerms == TokenRateTermsEnum.CurrencyPerToken ?
                                                    tokenPairRateRecord.Token1AskRate : 1.0 / tokenPairRateRecord.Token1BidRate) *
                                               (tokenPairRateRecord.Token2RateTerms == TokenRateTermsEnum.TokenPerCurrency ?
                                                    tokenPairRateRecord.Token2AskRate : 1.0 / tokenPairRateRecord.Token2BidRate) *
                                               (currencyPairRateRecord.RateTerms == CurrencyPairRateTermsEnum.Currency2PerCurrency1 ?
                                                currencyPairRateRecord.AskRate : 1.0 / currencyPairRateRecord.BidRate);
            }
        }

        public void UpdateCurrencyPairRate(CurrencyPairRateData currencyPairRateData)
        {
            tokenPairRateRecord.CurrencyPairBidRate = currencyPairRateData.BidRate;
            tokenPairRateRecord.CurrencyPairAskRate = currencyPairRateData.AskRate;
            tokenPairRateRecord.Currency1BidRate = currencyPairRateData.Currency1BidRate;
            tokenPairRateRecord.Currency1AskRate = currencyPairRateData.Currency1AskRate;
            tokenPairRateRecord.Currency2BidRate = currencyPairRateData.Currency2BidRate;
            tokenPairRateRecord.Currency2AskRate = currencyPairRateData.Currency2AskRate;

            RecalculateRates();
            publishTokenPairUpdate();
        }

        public void UpdateTokenRate(TokenRateData tokenRateRecord)
        {
            //Console.WriteLine("Updating Token Rate for {0} Bid/Offer {1}/{2}", tokenRateRecord.TokenId, tokenRateRecord.BidRate, tokenRateRecord.AskRate);

            if (tokenRateRecord.TokenId == tokenPairRateRecord.Token1Id)
            {
                tokenPairRateRecord.Token1BidRate = tokenRateRecord.BidRate;
                tokenPairRateRecord.Token1AskRate = tokenRateRecord.AskRate;
            }
            else if (tokenRateRecord.TokenId == tokenPairRateRecord.Token2Id)
            {
                tokenPairRateRecord.Token2BidRate = tokenRateRecord.BidRate;
                tokenPairRateRecord.Token2AskRate = tokenRateRecord.AskRate;
            }

            RecalculateRates();
            publishTokenPairUpdate();
        }

        private void publishTokenPairUpdate()
        {
            //Console.WriteLine("Publishing Token Pair {0}/{1} Bid/Offer {2}/{3}", tokenPairRateRecord.Token1Id, tokenPairRateRecord.Token2Id, tokenPairRateRecord.BidRate, tokenPairRateRecord.AskRate);
            _subject.OnNext(tokenPairRateRecord);
        }

        public IDisposable subscribe()
        {
            return _subject.Subscribe();
        }

        public void Dispose()
        {
            // Discontinue publishing
            _subject.Dispose();

            // Discontinue subscriptions
            disposeOfToken1Subscription.Dispose();
            disposeOfToken2Subscription.Dispose();
            disposeOfCurrencyPairSubscription.Dispose();
        }
    }

    /// <summary>
    /// Currently, the Asset Rate Manager is pre-populated with a set of pre-defined assets, but this will change such that assets are
    /// loaded on demand, and linked to a rate feed based on when there is a demand.  In turn, the asset rates within this manager will
    /// be created as subscriptions are created to the discrete asset rates either by a client application, or upstream such as the 
    /// currency pair manager, or the token pair manager.
    /// </summary>
    public class AssetRateManager
    {
        public ConcurrentDictionary<string, AssetRateViewModel> assetRateList = new ConcurrentDictionary<string, AssetRateViewModel>();
        public object assetRateListLock = new object();
    }

    public class TokenRateManager
    {
        public ConcurrentDictionary<string, TokenRateViewModel> tokenRateList = new ConcurrentDictionary<string, TokenRateViewModel>();
        public object tokenRateListLock = new object();

    }

    public class CurrencyPairRateManager
    {
        public object currencyPairRateListLock = new object();
        public ConcurrentDictionary<string, CurrencyPairRateViewModel> currencyPairRateList = new ConcurrentDictionary<string, CurrencyPairRateViewModel>();
        private AssetRateManager _assetRateManager;


        public CurrencyPairRateManager(AssetRateManager assetRateManager)
        {
            _assetRateManager = assetRateManager;
        }

        public CurrencyPairRateViewModel GetCurrencyPairRateView(string currency1, string currency2)
        {
            CurrencyPairRateViewModel currencyPairRateViewModelRecord = null;
            String CurrencyPairKey = currency1.Trim() + " - " + currency2.Trim();

            // Create a currency pair view model if one does not exist

            lock (currencyPairRateListLock)
            {
                if (currencyPairRateList.TryGetValue(CurrencyPairKey, out currencyPairRateViewModelRecord) == false)
                {
                    AssetRateViewModel assetRateDataRecord1;
                    AssetRateViewModel assetRateDataRecord2;

                    lock (_assetRateManager.assetRateListLock)
                    {
                        if (_assetRateManager.assetRateList.TryGetValue(currency1, out assetRateDataRecord1) == true)
                        {
                            if (_assetRateManager.assetRateList.TryGetValue(currency2, out assetRateDataRecord2) == true)
                            {
                                currencyPairRateViewModelRecord = new CurrencyPairRateViewModel(currency1, currency2, assetRateDataRecord1.assetRateDataRecord, assetRateDataRecord2.assetRateDataRecord,
                                                                                                assetRateDataRecord1._subject, assetRateDataRecord2._subject);
                                currencyPairRateList.TryAdd(CurrencyPairKey, currencyPairRateViewModelRecord);
                            }
                        }
                    }
                }
            }


            return currencyPairRateViewModelRecord;
        }

        public void CheckCurrencyPairSubscriptions(string CurrencyPairKey)
        {
            CurrencyPairRateViewModel currencyPairRateViewModelRecord;

            if (currencyPairRateList.TryGetValue(CurrencyPairKey, out currencyPairRateViewModelRecord) == true)
            {
                if (currencyPairRateViewModelRecord._subject.HasObservers == false)
                {
                    currencyPairRateViewModelRecord.Dispose();
                    currencyPairRateList.TryRemove(CurrencyPairKey, out currencyPairRateViewModelRecord);

                    Console.WriteLine("Removing the Currency Pair Subscription");
                }
            }
        }
    }

    public class TokenPairRateManager
    {
        public ConcurrentDictionary<string, TokenPairRateViewModel> tokenPairRateList = new ConcurrentDictionary<string, TokenPairRateViewModel>();
        public object tokenPairRateListLock = new object();
        private TokenRateManager _tokenRateManager;
        private CurrencyPairRateManager _currencyPairRateManager;

        public TokenPairRateManager(TokenRateManager tokenRateManager, CurrencyPairRateManager currencyPairRateManager)
        {
            _tokenRateManager = tokenRateManager;
            _currencyPairRateManager = currencyPairRateManager;
        }

        public TokenPairRateViewModel GetTokenPairRateView(string token1Id, string token2Id)
        {
            TokenPairRateViewModel tokenPairRateViewModelRecord = null;
            String TokenPairKey = token1Id.Trim() + " - " + token2Id.Trim();

            try
            {
                lock (tokenPairRateListLock)
                {
                    if (tokenPairRateList.TryGetValue(TokenPairKey, out tokenPairRateViewModelRecord) == false)
                    {
                        TokenRateViewModel tokenRateRecord1;
                        TokenRateViewModel tokenRateRecord2;

                        if (_tokenRateManager.tokenRateList.TryGetValue(token1Id, out tokenRateRecord1) == true)
                        {
                            if (_tokenRateManager.tokenRateList.TryGetValue(token2Id, out tokenRateRecord2) == true)
                            {
                                tokenPairRateViewModelRecord = new TokenPairRateViewModel(token1Id, token2Id, _tokenRateManager, _currencyPairRateManager);

                                tokenPairRateList.TryAdd(TokenPairKey, tokenPairRateViewModelRecord);
                            }
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(String.Format("Error obtaining TokenPairRate {0}", ex.Message), ex);
            }

            return tokenPairRateViewModelRecord;
        }

        public void CheckTokenPairSubscriptions(string TokenPairKey)
        {
            TokenPairRateViewModel tokenPairRateViewModelRecord;

            if (tokenPairRateList.TryGetValue(TokenPairKey, out tokenPairRateViewModelRecord) == true)
            {
                if (tokenPairRateViewModelRecord._subject.HasObservers == false)
                {
                    tokenPairRateViewModelRecord.Dispose();
                    tokenPairRateList.TryRemove(TokenPairKey, out tokenPairRateViewModelRecord);

                    Console.WriteLine("Removing the Token Pair Subscription");
                }
            }
        }
    }

    [ServiceBehavior(UseSynchronizationContext = false, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class RateManager : IRateService
    {
        // Asset/Token Lists and Locks
        ConcurrentDictionary<string, AssetRate> baseAssetList = new ConcurrentDictionary<string, AssetRate>();

        static public ConcurrentDictionary<string, AssetRateListener> assetRateSubscriptions = new ConcurrentDictionary<string, AssetRateListener>();
        static public ConcurrentDictionary<string, TokenRateListener> tokenRateSubscriptions = new ConcurrentDictionary<string, TokenRateListener>();
        static public ConcurrentDictionary<string, CurrencyPairRateListener> currencyPairRateSubscriptions = new ConcurrentDictionary<string, CurrencyPairRateListener>();
        static public ConcurrentDictionary<string, TokenPairRateListener> tokenPairRateSubscriptions = new ConcurrentDictionary<string, TokenPairRateListener>();

        public AssetRateManager assetRateManager;
        public CurrencyPairRateManager currencyPairRateManager;
        public TokenRateManager tokenRateManager;
        public TokenPairRateManager tokenPairRateManager;

        // Listener Lists and Locks
        static public object AssetRateListenerLock = new object();
        static public object AssetListLock = new object();

        static public object TokenRateListenerLock = new object();
        static public object TokenListLock = new object();

        static public object CurrencyPairRateListenerLock = new object();
        static public object CurrencyPairListLock = new object();

        static public object TokenPairRateListenerLock = new object();
        static public object TokenPairListLock = new object();


        private readonly Random _updateOrNotRandom = new Random();
        private readonly Random random = new Random();
        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(150);
        private readonly Timer _timer;

        private Subject<AssetRate> _subject = new Subject<AssetRate>();
        private volatile bool _updatingAssetPrices = false;

        public RateManager()
        {
            assetRateManager = new AssetRateManager();
            currencyPairRateManager = new CurrencyPairRateManager(assetRateManager);
            tokenRateManager = new TokenRateManager();
            tokenPairRateManager = new TokenPairRateManager(tokenRateManager, currencyPairRateManager);


            baseAssetList.TryAdd("AAPL", new AssetRate { AssetId = "AAPL", PriceCurrency = "USD", RateTerms = AbacasX.Model.Models.RateTermsEnum.CurrencyPerAsset, BidRate = 179.85, AskRate = 180 });
            baseAssetList.TryAdd("GOOG", new AssetRate { AssetId = "GOOG", PriceCurrency = "USD", RateTerms = AbacasX.Model.Models.RateTermsEnum.CurrencyPerAsset, BidRate = 1140, AskRate = 1141 });
            baseAssetList.TryAdd("MSFT", new AssetRate { AssetId = "MSFT", PriceCurrency = "USD", RateTerms = AbacasX.Model.Models.RateTermsEnum.CurrencyPerAsset, BidRate = 127, AskRate = 127 });
            baseAssetList.TryAdd("GOLD", new AssetRate { AssetId = "GOLD", PriceCurrency = "USD", RateTerms = AbacasX.Model.Models.RateTermsEnum.CurrencyPerAsset, BidRate = 1283, AskRate = 1283 });
            baseAssetList.TryAdd("BTC", new AssetRate { AssetId = "BTC", PriceCurrency = "USD", RateTerms = AbacasX.Model.Models.RateTermsEnum.CurrencyPerAsset, BidRate = 8012, AskRate = 8012 });
            baseAssetList.TryAdd("USD", new AssetRate { AssetId = "USD", PriceCurrency = "USD", RateTerms = AbacasX.Model.Models.RateTermsEnum.CurrencyPerAsset, BidRate = 1, AskRate = 1 });
            baseAssetList.TryAdd("ETH", new AssetRate { AssetId = "ETH", PriceCurrency = "USD", RateTerms = AbacasX.Model.Models.RateTermsEnum.CurrencyPerAsset, BidRate = 251, AskRate = 251 });
            baseAssetList.TryAdd("BNP", new AssetRate { AssetId = "BNP", PriceCurrency = "EUR", RateTerms = AbacasX.Model.Models.RateTermsEnum.CurrencyPerAsset, BidRate = 43.655, AskRate = 43.655 });
            baseAssetList.TryAdd("EUR", new AssetRate { AssetId = "EUR", PriceCurrency = "USD", RateTerms = AbacasX.Model.Models.RateTermsEnum.CurrencyPerAsset, BidRate = 1.1188, AskRate = 1.1188 });

            foreach (AssetRate a in baseAssetList.Values)
            {
                string tokenId;
                tokenId = '@' + a.AssetId;

                // Create the Asset Rate List
                if (assetRateManager.assetRateList.TryGetValue(a.AssetId, out AssetRateViewModel assetRateViewModelRecord) == false)
                {
                    assetRateViewModelRecord = new AssetRateViewModel(a.AssetId, a, _subject);
                    assetRateManager.assetRateList.TryAdd(a.AssetId, assetRateViewModelRecord);
                }

                // Create the Token Rate List
                if (tokenRateManager.tokenRateList.TryGetValue(tokenId, out TokenRateViewModel tokenRateRecord) == false)
                {
                    tokenRateRecord = new TokenRateViewModel(tokenId, a, _subject);
                    tokenRateManager.tokenRateList.TryAdd(tokenId, tokenRateRecord);
                }
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

                    foreach (var assetRateRecord in baseAssetList.Values)
                    {
                        if (assetRateRecord.AssetId != "USD")
                        {
                            TryUpdateAssetPrice(assetRateRecord);
                            _subject.OnNext(assetRateRecord);
                            //Console.WriteLine("Update Asset Price for {0} to {1}/{2}", assetRateRecord.AssetId, assetRateRecord.BidRate, assetRateRecord.AskRate);
                        }
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
            return assetRateManager.assetRateList.Values.ToList().Select(s => { return s.assetRateDataRecord.AssetId; }).ToList();
        }

        public List<AssetRateData> GetAssetRateList()
        {
            List<AssetRateData> assetRateDataList = new List<AssetRateData>();

            lock (assetRateManager.assetRateListLock)
            {
                foreach (AssetRateViewModel a in assetRateManager.assetRateList.Values)
                {
                    assetRateDataList.Add(a.assetRateDataRecord);
                }
            }

            return assetRateDataList;
        }

        public List<string> GetTokenList()
        {
            return tokenRateManager.tokenRateList.Values.ToList().Select(s => { return s.tokenRateRecord.TokenId; }).ToList();
        }

        public List<TokenRateData> GetTokenRateList()
        {
            List<TokenRateData> tokenRateDataList = new List<TokenRateData>();
            TokenRateData tokenRateDataRecord = new TokenRateData();

            lock (tokenRateManager.tokenRateListLock)
            {
                foreach (TokenRateViewModel t in tokenRateManager.tokenRateList.Values)
                {
                    tokenRateDataList.Add(t.tokenRateRecord);
                }
            }

            return tokenRateDataList;
        }

        #endregion

        #region Subscription Section

        public void SubscribeToAssetRateUpdate(string AssetId)
        {
            AssetRateListener assetRateListenerRecord;
            AssetRateViewModel assetRateViewModelRecord;
            AssetSubscription assetSubscription;

            lock (AssetRateListenerLock)
            {
                lock (assetRateManager.assetRateListLock)
                {
                    if (assetRateManager.assetRateList.TryGetValue(AssetId, out assetRateViewModelRecord) == false)
                    {
                        if (baseAssetList.TryGetValue(AssetId, out AssetRate assetRateRecord) == true)
                        {
                            assetRateViewModelRecord = new AssetRateViewModel(AssetId, assetRateRecord, _subject);
                        }
                        else
                        {
                            throw new Exception("Unable to find asset");
                        }
                    }

                    if (assetRateSubscriptions.TryGetValue(OperationContext.Current.SessionId, out assetRateListenerRecord) == false)
                    {
                        IRateServiceCallBack callBack = OperationContext.Current.GetCallbackChannel<IRateServiceCallBack>();
                        assetRateListenerRecord = new AssetRateListener(OperationContext.Current.GetCallbackChannel<IRateServiceCallBack>(), OperationContext.Current.SessionId);

                        assetSubscription = new AssetSubscription();
                        assetSubscription.AssetId = AssetId;
                        assetSubscription.disposable = assetRateViewModelRecord._subject.Subscribe(assetRateListenerRecord.PublishUpdate);

                        assetRateListenerRecord.assetSubscriptions.TryAdd(AssetId, assetSubscription);
                        assetRateSubscriptions.TryAdd(OperationContext.Current.SessionId, assetRateListenerRecord);
                    }
                    else
                    {

                        // If not already subscribed, then add a subscription on this asset

                        if (assetRateListenerRecord.assetSubscriptions.TryGetValue(AssetId, out assetSubscription) == false)
                        {
                            assetSubscription = new AssetSubscription();

                            assetSubscription.AssetId = AssetId;
                            assetSubscription.disposable = assetRateViewModelRecord._subject.Subscribe(assetRateListenerRecord.PublishUpdate);
                            assetRateListenerRecord.assetSubscriptions.TryAdd(AssetId, assetSubscription);
                        }
                    }
                }
            }
        }

        public void SubscribeToTokenRateUpdate(string TokenId)
        {
            TokenRateListener tokenRateListenerRecord;
            TokenRateViewModel tokenRateViewModelRecord;
            TokenSubscription tokenSubscription;

            lock (TokenRateListenerLock)
            {
                lock (tokenRateManager.tokenRateListLock)
                {
                    // Token Rate List will have a record for any defined token.
                    // It would be a fault to not find a tokenRateViewModel record.
                    if (tokenRateManager.tokenRateList.TryGetValue(TokenId, out tokenRateViewModelRecord) == true)
                    {
                        if (tokenRateSubscriptions.TryGetValue(OperationContext.Current.SessionId, out tokenRateListenerRecord) == false)
                        {
                            IRateServiceCallBack callBack = OperationContext.Current.GetCallbackChannel<IRateServiceCallBack>();
                            tokenRateListenerRecord = new TokenRateListener(OperationContext.Current.GetCallbackChannel<IRateServiceCallBack>(), OperationContext.Current.SessionId);

                            tokenSubscription = new TokenSubscription();
                            tokenSubscription.TokenId = TokenId;
                            tokenSubscription.disposable = tokenRateViewModelRecord._subject.Subscribe(tokenRateListenerRecord.PublishUpdate);

                            tokenRateListenerRecord.tokenSubscriptions.TryAdd(TokenId, tokenSubscription);
                            tokenRateSubscriptions.TryAdd(OperationContext.Current.SessionId, tokenRateListenerRecord);
                        }
                        else
                        {
                            // Add a subscription to the Token Rate Listener

                            if (tokenRateListenerRecord.tokenSubscriptions.TryGetValue(TokenId, out tokenSubscription) == false)
                            {
                                tokenSubscription = new TokenSubscription();
                                tokenSubscription.TokenId = TokenId;
                                tokenSubscription.disposable = tokenRateViewModelRecord._subject.Subscribe(tokenRateListenerRecord.PublishUpdate);

                                tokenRateListenerRecord.tokenSubscriptions.TryAdd(TokenId, tokenSubscription);
                            }
                        }
                    }
                }
            }
        }

        public void SubscribeToCurrencyPairRateUpdate(string Currency1, string Currency2)
        {
            String CurrencyPairKey = Currency1.Trim() + " - " + Currency2.Trim();

            CurrencyPairRateListener currencyPairRateListenerRecord;
            CurrencyPairRateViewModel currencyPairRateViewModelRecord;
            CurrencyPairSubscription currencyPairSubscription;

            lock (CurrencyPairRateListenerLock)
            {
                lock (currencyPairRateManager.currencyPairRateListLock)
                {
                    // The GetCurrencyPairRateView will create a CurrencyPairRateView record for the currency pair if one does not exist
                    //
                    if ((currencyPairRateViewModelRecord = currencyPairRateManager.GetCurrencyPairRateView(Currency1, Currency2)) != null)
                    {
                        if (currencyPairRateSubscriptions.TryGetValue(OperationContext.Current.SessionId, out currencyPairRateListenerRecord) == false)
                        {
                            IRateServiceCallBack callBack = OperationContext.Current.GetCallbackChannel<IRateServiceCallBack>();
                            currencyPairRateListenerRecord = new CurrencyPairRateListener(OperationContext.Current.GetCallbackChannel<IRateServiceCallBack>(), OperationContext.Current.SessionId);

                            // Add a currency pair subscription to the listener
                            currencyPairSubscription = new CurrencyPairSubscription();
                            currencyPairSubscription.CurrencyPair = CurrencyPairKey;
                            currencyPairSubscription.disposable = currencyPairRateViewModelRecord._subject.Subscribe(currencyPairRateListenerRecord.PublishUpdate);

                            currencyPairRateListenerRecord.currencyPairSubscriptions.TryAdd(CurrencyPairKey, currencyPairSubscription);

                            // Add the new listener to the listof currency pair subscriptions
                            currencyPairRateSubscriptions.TryAdd(OperationContext.Current.SessionId, currencyPairRateListenerRecord);
                        }
                        else
                        {
                            // Add the new currency pair subscription
                            if (currencyPairRateListenerRecord.currencyPairSubscriptions.TryGetValue(CurrencyPairKey, out currencyPairSubscription) == false)
                            {
                                currencyPairSubscription = new CurrencyPairSubscription();
                                currencyPairSubscription.CurrencyPair = CurrencyPairKey;
                                currencyPairSubscription.disposable = currencyPairRateViewModelRecord._subject.Subscribe(currencyPairRateListenerRecord.PublishUpdate);

                                currencyPairRateListenerRecord.currencyPairSubscriptions.TryAdd(CurrencyPairKey, currencyPairSubscription);
                            }
                            else
                            {
                                // The currency pair is already subscribed
                            }
                        }
                        Console.WriteLine("Subscribed to currency pair {0}/{1}", Currency1, Currency2);
                    }
                    else
                    {
                        throw new Exception("Unable to create currency pair view model");
                    }
                }
            }
        }

        public void SubscribeToTokenPairRateUpdate(string Token1, string Token2)
        {
            string TokenPairKey = Token1.Trim() + " - " + Token2.Trim();

            TokenPairRateListener tokenPairRateListenerRecord;
            TokenPairRateViewModel tokenPairRateViewModelRecord;
            TokenPairSubscription tokenPairSubscription;

            lock (TokenPairRateListenerLock)
            {
                if ((tokenPairRateViewModelRecord = tokenPairRateManager.GetTokenPairRateView(Token1, Token2)) != null)
                {
                    if (tokenPairRateSubscriptions.TryGetValue(OperationContext.Current.SessionId, out tokenPairRateListenerRecord) == false)
                    {
                        IRateServiceCallBack callBack = OperationContext.Current.GetCallbackChannel<IRateServiceCallBack>();
                        tokenPairRateListenerRecord = new TokenPairRateListener(OperationContext.Current.GetCallbackChannel<IRateServiceCallBack>(), OperationContext.Current.SessionId);

                        // Add a currency pair subscription to the listener
                        tokenPairSubscription = new TokenPairSubscription();
                        tokenPairSubscription.TokenPair = TokenPairKey;
                        tokenPairSubscription.disposable = tokenPairRateViewModelRecord._subject.Subscribe(tokenPairRateListenerRecord.PublishUpdate);

                        tokenPairRateListenerRecord.tokenPairSubscriptions.TryAdd(TokenPairKey, tokenPairSubscription);

                        // Add the new listener to the listof currency pair subscriptions
                        tokenPairRateSubscriptions.TryAdd(OperationContext.Current.SessionId, tokenPairRateListenerRecord);
                    }
                    else
                    {
                        // Add the new currency pair subscription
                        if (tokenPairRateListenerRecord.tokenPairSubscriptions.TryGetValue(TokenPairKey, out tokenPairSubscription) == false)
                        {
                            tokenPairSubscription = new TokenPairSubscription();
                            tokenPairSubscription.TokenPair = TokenPairKey;
                            tokenPairSubscription.disposable = tokenPairRateViewModelRecord._subject.Subscribe(tokenPairRateListenerRecord.PublishUpdate);

                            tokenPairRateListenerRecord.tokenPairSubscriptions.TryAdd(TokenPairKey, tokenPairSubscription);
                        }
                        else
                        {
                            // The currency pair is already subscribed
                        }
                    }
                    Console.WriteLine("Subscribed to token pair {0}/{1}", Token1, Token2);
                }
                else
                {
                    throw new Exception("Unable to create token pair view model");
                }

            }
        }

        public void UnSubscribeAllRateUpdates()
        {
            throw new NotImplementedException();
        }

        public void UnSubscribeToAssetRateUpdate(string AssetId)
        {
            AssetRateListener assetRateListenerRecord;
            AssetSubscription assetSubscription;

            lock (AssetRateListenerLock)
            {
                lock (assetRateManager.assetRateListLock)
                {
                    if (assetRateSubscriptions.TryGetValue(OperationContext.Current.SessionId, out assetRateListenerRecord) == true)
                    {
                        if (assetRateListenerRecord.assetSubscriptions.TryGetValue(AssetId, out assetSubscription) == true)
                        {
                            Console.WriteLine("Unsubscribing asset {0}", AssetId);
                            assetSubscription.disposable.Dispose();
                            assetRateListenerRecord.assetSubscriptions.TryRemove(AssetId, out assetSubscription);
                        }
                    }
                }
            }
        }

        public void UnSubscribeToTokenRateUpdate(string TokenId)
        {
            TokenRateListener tokenRateListenerRecord;
            TokenSubscription tokenSubscription;

            lock (TokenRateListenerLock)
            {
                lock (tokenRateManager.tokenRateListLock)
                {
                    if (tokenRateSubscriptions.TryGetValue(OperationContext.Current.SessionId, out tokenRateListenerRecord) == true)
                    {
                        if (tokenRateListenerRecord.tokenSubscriptions.TryGetValue(TokenId, out tokenSubscription) == true)
                        {
                            Console.WriteLine("Unsubscribing token {0}", TokenId);
                            tokenSubscription.disposable.Dispose();
                            tokenRateListenerRecord.tokenSubscriptions.TryRemove(TokenId, out tokenSubscription);
                        }
                    }
                }
            }
        }

        public void UnSubscribeToCurrencyPairRateUpdate(string Currency1, string Currency2)
        {
            CurrencyPairRateListener currencyPairRateListenerRecord;
            CurrencyPairSubscription currencyPairSubscription;
            String CurrencyPairKey = Currency1.Trim() + " - " + Currency2.Trim();

            lock (CurrencyPairRateListenerLock)
            {
                lock (currencyPairRateManager.currencyPairRateListLock)
                {
                    if (currencyPairRateSubscriptions.TryGetValue(OperationContext.Current.SessionId, out currencyPairRateListenerRecord) == true)
                    {
                        if (currencyPairRateListenerRecord.currencyPairSubscriptions.TryGetValue(CurrencyPairKey, out currencyPairSubscription) == true)
                        {
                            Console.WriteLine("Unsubscribing to Currency Pair {0}", CurrencyPairKey);
                            currencyPairSubscription.disposable.Dispose();
                            currencyPairRateListenerRecord.currencyPairSubscriptions.TryRemove(CurrencyPairKey, out currencyPairSubscription);
                            currencyPairRateManager.CheckCurrencyPairSubscriptions(CurrencyPairKey);
                        }
                    }
                }
            }
        }

        public void UnSubscribeToTokenPairRateUpdate(string Token1, string Token2)
        {
            string TokenPairKey = Token1.Trim() + " - " + Token2.Trim();

            TokenPairRateListener tokenPairRateListenerRecord;
            TokenPairSubscription tokenPairSubscription;

            lock (TokenPairRateListenerLock)
            {
                lock (tokenPairRateManager.tokenPairRateListLock)
                {
                    if (tokenPairRateSubscriptions.TryGetValue(OperationContext.Current.SessionId, out tokenPairRateListenerRecord) == true)
                    {
                        if (tokenPairRateListenerRecord.tokenPairSubscriptions.TryGetValue(TokenPairKey, out tokenPairSubscription) == true)
                        {
                            Console.WriteLine("Unsubscribing from token pair {0}", TokenPairKey);
                            tokenPairSubscription.disposable.Dispose();
                            tokenPairRateListenerRecord.tokenPairSubscriptions.TryRemove(TokenPairKey, out tokenPairSubscription);

                            tokenPairRateManager.CheckTokenPairSubscriptions(TokenPairKey);
                        }
                    }
                }
            }
        }

        public TokenPairRateData GetTokenPairRate(string Token1, string Token2)
        {
            TokenPairRateViewModel tokenPairRateViewModelRecord;

            Console.WriteLine("Requesting Token Pair Rate for {0}/{1} Connection Session Id {2}", Token1, Token2, OperationContext.Current.SessionId);

            try
            {
                tokenPairRateViewModelRecord = tokenPairRateManager.GetTokenPairRateView(Token1, Token2);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Get TokenPair Rate Exception {0}", ex.Message), ex);
            }

            Console.WriteLine("Returning Token Pair Rate for {0}/{1} Connection Session Id {2} Bid Rate {3}", Token1, Token2, OperationContext.Current.SessionId, tokenPairRateViewModelRecord.tokenPairRateRecord.BidRate);

            return tokenPairRateViewModelRecord.tokenPairRateRecord;
        }

        public TokenRateData GetTokenRate(string TokenId)
        {
            TokenRateData tokenRateDataRecord = new TokenRateData();
            TokenRateViewModel tokenRateViewModelRecord;

            lock (tokenRateManager.tokenRateListLock)
            {
                if (tokenRateManager.tokenRateList.TryGetValue(TokenId, out tokenRateViewModelRecord) == true)
                {
                    tokenRateDataRecord = tokenRateViewModelRecord.tokenRateRecord;
                }
                else
                {
                    tokenRateDataRecord = new TokenRateData();
                    tokenRateDataRecord.TokenId = TokenId;
                }
            }

            return tokenRateDataRecord;
        }

        #endregion
    }
}