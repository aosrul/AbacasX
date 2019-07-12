using AbacasX.UI.Hubs;
using AbacasX.UI.Repository;
using Microsoft.AspNetCore.SignalR;
using RateService;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace AbacasX.UI.Services
{

    #region Token Pair Subscription Management

    // This is a subscription to a particular token pair.  There may be more than one listener to this token pair
    public class TokenPairSubscription : IDisposable
    {
        public string Token1Id { get; set; }
        public string Token2Id { get; set; }
        public string TokenPairKey { get; set; }
        public Subject<TokenPairRateData> tokenPairSubject;
        public IDisposable dispose;

        public TokenPairSubscription(string token1Id, string token2Id, string tokenPairKey)
        {
            Token1Id = token1Id;
            Token2Id = token2Id;
            TokenPairKey = tokenPairKey;
            tokenPairSubject = new Subject<TokenPairRateData>();
        }

        // This is receiving all rate updates, so it must filter for the appropriate token pair updates
        public void UpdateTokenPairRate(TokenPairRateData tokenPairRateData)
        {
            if (tokenPairRateData.Token1Id.Equals(Token1Id) && tokenPairRateData.Token2Id.Equals(Token2Id))
                tokenPairSubject.OnNext(tokenPairRateData);
        }

        public void Dispose()
        {
            if (dispose != null)
                dispose.Dispose();

            tokenPairSubject.OnCompleted();
        }
    }

    public class TokenPairRateListener : IDisposable
    {
        public string SessionId;
        public IDisposable disposable;
        public Subject<TokenPairRateData> subject;
        public string tokenPairKey;
        private IHubContext<RateServerHub> _hub { get; set; }

        public TokenPairRateListener(string sessionId, IHubContext<RateServerHub> Hub)
        {
            SessionId = sessionId;
            disposable = null;
            _hub = Hub;
        }

        public void SubscribeToTokenPair(string Token1Id, string Token2Id, Subject<TokenPairRateData> tokenPairSubject)
        {
            if (disposable != null)
                disposable.Dispose();

            tokenPairKey = Token1Id.Trim() + " - " + Token2Id.Trim();

            disposable = tokenPairSubject.Subscribe(UpdateTokenPairRate);
        }

        public void UnSubscribe()
        {
            if (disposable != null)
                disposable.Dispose();
        }

        public void UpdateTokenPairRate(TokenPairRateData tokenPairRateData)
        {
            Console.WriteLine("Sending Rate Update to {0} for {1} Bid/Offer {2}/{3}",
                SessionId, tokenPairKey, tokenPairRateData.BidRate, tokenPairRateData.AskRate);

            _hub.Clients.Client(SessionId).SendAsync("broadcastTokenPairRateUpdate", tokenPairRateData);
        }

        public void Dispose()
        {
            if (disposable != null)
                disposable.Dispose();
        }
    }
    #endregion


    public class RateServer
    {
        private IHubContext<RateServerHub> Hub { get; set; }
        private IRateService _rateService;
        private readonly Subject<TokenRateData> _tokenRateSubject = new Subject<TokenRateData>();
        private readonly Subject<TokenPairRateData> _tokenPairRateSubject = new Subject<TokenPairRateData>();
        private ConcurrentDictionary<string, TokenPairSubscription> tokenPairSubscriptions = new ConcurrentDictionary<string, TokenPairSubscription>();
        private ConcurrentDictionary<string, TokenPairRateListener> tokenPairListeners = new ConcurrentDictionary<string, TokenPairRateListener>();


        public RateServer(IHubContext<RateServerHub> hub, IRateService rateService)
        {
            Hub = hub;
            _rateService = rateService;

            ((RateRepository)_rateService).setTokenPairSubject(_tokenPairRateSubject);
        }

        public IEnumerable<String> GetAssetList()
        {
            return _rateService.GetAssetListAsync().GetAwaiter().GetResult();
        }

        public IEnumerable<String> GetTokenList()
        {
            return _rateService.GetTokenListAsync().GetAwaiter().GetResult();
        }

        public IEnumerable<TokenRateData> GetTokenRateList()
        {
            var results = _rateService.GetTokenRateListAsync().GetAwaiter().GetResult();

            return results;
        }

        public IObservable<TokenRateData> StreamTokenRates()
        {
            return _tokenRateSubject;
        }

        public IObservable<TokenPairRateData> StreamTokenPairRates(string ConnectionId)
        {
            return _tokenPairRateSubject;

            //TokenPairRateListener tokenPairListenerRecord;

            //if (tokenPairListeners.TryGetValue(ConnectionId, out tokenPairListenerRecord) == true)
            //    return tokenPairListenerRecord.subject;
            //else
            //{
            //    Console.WriteLine("Error streaming rates to connection {0}", ConnectionId);
            //    return null;
            //}
        }

        public void SubscribeToTokenRates(string TokenId)
        {
            _rateService.SubscribeToTokenRateUpdateAsync(TokenId);
        }

        public void UnSubscribeToTokenRates(string TokenId)
        {
            _rateService.UnSubscribeToTokenRateUpdateAsync(TokenId);
        }

        // This requires an override to the RateRepository so that we can access the overloaded
        // function that can receive the Observable Subject which is then streamed to the clients.
        public void SubscribeToTokenPairRates(string Token1Id, string Token2Id)
        {
            ((RateRepository)_rateService).SubscribeToTokenPairRateUpdateAsync(Token1Id, Token2Id);
        }

       
        public void ClientSubscribeTokenPairRate(string ConnectionId, string Token1Id, string Token2Id)
        {
            string tokenPairKey = Token1Id.Trim() + " - " + Token2Id.Trim();
            TokenPairSubscription tokenPairSubscriptionRecord;
            TokenPairRateListener tokenPairListenerRecord;

            try
            {
                if (tokenPairSubscriptions.TryGetValue(tokenPairKey, out tokenPairSubscriptionRecord) == false)
                {
                    // New Token Pair Subscription
                    tokenPairSubscriptionRecord = new TokenPairSubscription(Token1Id, Token2Id, tokenPairKey);
                    tokenPairSubscriptions.TryAdd(tokenPairKey, tokenPairSubscriptionRecord);

                    // Subscribe to the rate updates received
                    tokenPairSubscriptionRecord.dispose = _tokenPairRateSubject.Subscribe(tokenPairSubscriptionRecord.UpdateTokenPairRate);

                    // Subscribe to the token pair on the rate server
                    ((RateRepository)_rateService).SubscribeToTokenPairRateUpdateAsync(Token1Id, Token2Id);
                }
                else
                {
                    // Re-subscribe to the server as the last subscriber for the token key pair will have unsubscribed.
                    if (tokenPairSubscriptionRecord.tokenPairSubject.HasObservers == false)
                    {
                        ((RateRepository)_rateService).SubscribeToTokenPairRateUpdateAsync(Token1Id, Token2Id);
                        tokenPairSubscriptionRecord.dispose = _tokenPairRateSubject.Subscribe(tokenPairSubscriptionRecord.UpdateTokenPairRate);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Unable to locate/create a token pair subscription for {0}/{1} with error {2}", Token1Id, Token2Id, ex.Message);
                return;
            }


            // Find the connection listener
            try
            {
                if (tokenPairListeners.TryGetValue(ConnectionId, out tokenPairListenerRecord) == false)
                {
                    tokenPairListenerRecord = new TokenPairRateListener(ConnectionId, Hub);
                    tokenPairListeners.TryAdd(ConnectionId, tokenPairListenerRecord);
                }
                else
                {
                    // Unsubscribe from current subscription
                    if (tokenPairListenerRecord.disposable != null)
                        tokenPairListenerRecord.disposable.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Unable to locate/create a token pair listener for connection id{0} with error {1}", ConnectionId, ex.Message);
                return;
            }

            tokenPairListenerRecord.SubscribeToTokenPair(Token1Id, Token2Id, tokenPairSubscriptionRecord.tokenPairSubject);
        }

        public void ClientUnSubscribeAllRateUpdates(string ConnectionId)
        {
            TokenPairSubscription tokenPairSubscriptionRecord;
            TokenPairRateListener tokenPairListenerRecord;
            string tokenPairKey;

            if (tokenPairListeners.TryGetValue(ConnectionId, out tokenPairListenerRecord) == true)
            {
                tokenPairKey = tokenPairListenerRecord.tokenPairKey;

                // Unsubscribe for this connection
                tokenPairListenerRecord.Dispose();

                // Unsubscribe at the source if there are no additional listeners.
                if (tokenPairSubscriptions.TryGetValue(tokenPairKey, out tokenPairSubscriptionRecord) == true)
                {
                    // If there are no more observers, then unsubscribe to the token pair at the rate server level.
                    if (tokenPairSubscriptionRecord.tokenPairSubject.HasObservers == false)
                    {
                        tokenPairSubscriptionRecord.dispose.Dispose();
                        //_rateService.UnSubscribeToTokenPairRateUpdateAsync(tokenPairSubscriptionRecord.Token1Id, tokenPairSubscriptionRecord.Token2Id);
                    }
                }
            }
        }

        public void UnSubscribeToTokenPairRates(string Token1Id, string Token2Id)
        {
            _rateService.UnSubscribeToTokenPairRateUpdateAsync(Token1Id, Token2Id);
        }

        public void UnSubscribeToAllRateUpdates(string ConnectionId)
        {
            //_rateService.UnSubscribeAllRateUpdatesAsync();
        }

        public TokenRateData GetTokenRate(string TokenId)
        {
            return _rateService.GetTokenRateAsync(TokenId).GetAwaiter().GetResult();
        }

        public TokenDetail GetTokenDetail(string TokenId)
        {
            return _rateService.GetTokenDetailAsync(TokenId).GetAwaiter().GetResult();
        }

        public async Task<TokenPairRateData> GetTokenPairRate(string Token1Id, string Token2Id)
        {
            try
            {
                Console.WriteLine("Calling Rate Server GetTokenPairRateAsync on {0}/{1}", Token1Id, Token2Id);
                return await _rateService.GetTokenPairRateAsync(Token1Id, Token2Id);
            }
            catch (Exception e)
            {
                throw new Exception("RateServer GetTokenPairRate Failed", e);
            }
        }

        public bool IsRateFeedOn()
        {
            return _rateService.IsRateFeedOnAsync().GetAwaiter().GetResult();
        }

        public bool ToggleRateFeed()
        {
            return _rateService.ToggleRateFeedAsync().GetAwaiter().GetResult();
        }
    }
}
