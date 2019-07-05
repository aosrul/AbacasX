using AbacasX.UI.Repository;
using AbacasX.UI.Services;
using Microsoft.AspNetCore.SignalR;
using RateService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AbacasX.UI.Hubs
{
    public class RateServerHub : Hub
    {
        private RateServer _rateServer;

        public RateServerHub(RateServer rateServer)
        {
            _rateServer = rateServer;
        }

        // Token List
        public IEnumerable<String> getTokenList()
        {
            return _rateServer.GetTokenList();
        }

        public IEnumerable<String> getAssetList()
        {
            return _rateServer.GetAssetList();
        }

        // Token Rate List
        public IEnumerable<TokenRateData> getTokenRateList()
        {
            return _rateServer.GetTokenRateList();
        }

        // Streaming Token Rates
        public ChannelReader<TokenRateData> StreamTokenRates()
        {
            return _rateServer.StreamTokenRates().AsChannelReader(10);
        }

        public ChannelReader<TokenPairRateData> StreamTokenPairRates()
        {
            return _rateServer.StreamTokenPairRates().AsChannelReader(10);
        }

        public void SubscribeToTokenRates(string tokenId)
        {
            _rateServer.SubscribeToTokenRates(tokenId);
        }

        public void UnSubscribeToTokenRates(string tokenId)
        {
            _rateServer.UnSubscribeToTokenRates(tokenId);
        }

        public void SubscribeToTokenPairRates(string token1Id, string token2Id)
        {
            _rateServer.SubscribeToTokenPairRates(token1Id, token2Id);
        }

        public void SubscribeToOneTokenPairRate(string token1Id, string token2Id)
        {
            _rateServer.SubscribeToOneTokenPairRate(token1Id, token2Id);
        }

        public void UnSubscribeToTokenPairRates(string token1Id, string token2Id)
        {
            _rateServer.UnSubscribeToTokenPairRates(token1Id, token2Id);
        }

        public void UnSubscribeToAllRateUpdates()
        {
            _rateServer.UnSubscribeToAllRateUpdates();
        }

        public async Task<TokenPairRateData> GetTokenPairRate(string Token1Id, string Token2Id)
        {
            try
            {
                Console.WriteLine("Calling RateServerHub GetTokenPairRate on {0}/{1}", Token1Id, Token2Id);
                var result = await _rateServer.GetTokenPairRate(Token1Id, Token2Id);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error calling Rate Server GetTokenPairRate {0}", e.Message);
                throw new Exception("Error calling Rate Server GetTokenPairRate", e);
            }
        }

        public TokenRateData getTokenRate(string TokenId)
        {
            return _rateServer.GetTokenRate(TokenId);
        }


        public TokenDetail GetTokenDetail(string TokenId)
        {
            try
            {
                return _rateServer.GetTokenDetail(TokenId);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error calling Rate Server GetTokenDetail {0}", e.Message);
                throw new Exception(String.Format("Error calling GetTokenDetail for {0}", TokenId));
            }
        }

        public bool IsRateFeedOn()
        {
            try
            {
                return _rateServer.IsRateFeedOn();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error determining the Rate Feed Status {0}", e.Message);
                throw new Exception("Error determining the rate feed status", e);

            }
        }

        public bool ToggleRateFeed()
        {
            try
            {
                return _rateServer.ToggleRateFeed();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error toggling the Rate Feed Status {0}", e.Message);
                throw new Exception("Error toggling the rate feed status", e);

            }
        }

    }
}
