using AbacasX.UI.Hubs;
using AbacasX.UI.Repository;
using Microsoft.AspNetCore.SignalR;
using RateService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace AbacasX.UI.Services
{
    public class RateServer
    {
        private IHubContext<RateServerHub> Hub { get; set; }
        private IRateService _rateService;
        private readonly Subject<TokenRateData> _tokenRateSubject = new Subject<TokenRateData>();
        private readonly Subject<TokenPairRateData> _tokenPairRateSubject = new Subject<TokenPairRateData>();


        public RateServer(IHubContext<RateServerHub> hub, IRateService rateService)
        {
            Hub = hub;
            _rateService = rateService;
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
            var results =  _rateService.GetTokenRateListAsync().GetAwaiter().GetResult();

            return results;
        }

        public IObservable<TokenRateData> StreamTokenRates()
        {
            return _tokenRateSubject;
        }

        public IObservable<TokenPairRateData> StreamTokenPairRates()
        {
            return _tokenPairRateSubject;
        }

        public void SubscribeToTokenRates(string TokenId)
        {
            _rateService.SubscribeToTokenRateUpdateAsync(TokenId);
        }

        public void UnSubscribeToTokenRates(string TokenId)
        {
            _rateService.UnSubscribeToTokenRateUpdateAsync(TokenId);
        }

        public void SubscribeToTokenPairRates(string Token1Id, string Token2Id)
        {
            _rateService.SubscribeToTokenPairRateUpdateAsync(Token1Id, Token2Id);
        }

        public void UnSubscribeToTokenPairRates(string Token1Id, string Token2Id)
        {
            _rateService.UnSubscribeToTokenPairRateUpdateAsync(Token1Id, Token2Id);
        }

        public TokenRateData GetTokenRate(string TokenId)
        {
            return _rateService.GetTokenRateAsync(TokenId).GetAwaiter().GetResult();
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
    }
}
