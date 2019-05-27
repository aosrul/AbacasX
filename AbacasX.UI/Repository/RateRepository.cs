using RateService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AbacasX.UI.Repository
{

    public class RateRepository : IRateService
    {
        RateServiceClient _rateServiceClient;

        public RateRepository()
        {
            _rateServiceClient = new RateServiceClient();
        }

        public async Task<string[]> GetAssetListAsync()
        {
            return await _rateServiceClient.GetAssetListAsync();
        }

        public async Task<AssetRateData[]> GetAssetRateListAsync()
        {
            return await _rateServiceClient.GetAssetRateListAsync();
        }

        public async Task<string[]> GetTokenListAsync()
        {
            checkConnectionStatus();

            try
            {
                var result = await _rateServiceClient.GetTokenListAsync();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetTokenList Failed {0}", e.Message);
                throw new Exception("GetTokenList Failed", e);
            }
            //return await _rateServiceClient.GetTokenListAsync();
        }

        public async Task<TokenRateData[]> GetTokenRateListAsync()
        {
            checkConnectionStatus();

            try
            {
                return await _rateServiceClient.GetTokenRateListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetTokenRateList Failed");
                throw new Exception("GetTokenRateList Failed", e);
            }
        }

        public Boolean checkConnectionStatus()
        {
            if (_rateServiceClient.State != CommunicationState.Opened)
            {
                try
                {
                    Console.WriteLine("Re-Connecting to Rate Service");
                    _rateServiceClient = new RateServiceClient();

                }
                catch(Exception e)
                {
                    throw new Exception(String.Format("Error Re-connecting to Rate Service {0}", e.Message));
                }
            }

            return true;
        }

        public Task SubscribeToAssetRateUpdateAsync(string AssetId)
        {
            _rateServiceClient.AssetRateUpdateReceived += _rateServiceClient_AssetRateUpdateReceived;
            return _rateServiceClient.SubscribeToAssetRateUpdateAsync(AssetId);
        }

        private void _rateServiceClient_AssetRateUpdateReceived(object sender, AssetRateUpdateReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public Task SubscribeToCurrencyPairRateUpdateAsync(string Currency1, string Currency2)
        {
            _rateServiceClient.CurrencyPairRateUpdateReceived += _rateServiceClient_CurrencyPairRateUpdateReceived;
            return _rateServiceClient.SubscribeToCurrencyPairRateUpdateAsync(Currency1, Currency2);
        }

        private void _rateServiceClient_CurrencyPairRateUpdateReceived(object sender, CurrencyPairRateUpdateReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public Task SubscribeToTokenPairRateUpdateAsync(string Token1, string Token2)
        {
            _rateServiceClient.TokenPairRateUpdateReceived += _rateServiceClient_TokenPairRateUpdateReceived1;
            return _rateServiceClient.SubscribeToTokenPairRateUpdateAsync(Token1, Token2);
        }

        private void _rateServiceClient_TokenPairRateUpdateReceived1(object sender, TokenPairRateUpdateReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _rateServiceClient_TokenPairRateUpdateReceived(object sender, TokenPairRateUpdateReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public Task SubscribeToTokenRateUpdateAsync(string TokenId)
        {
            _rateServiceClient.TokenRateUpdateReceived += _rateServiceClient_TokenRateUpdateReceived;
            return _rateServiceClient.SubscribeToTokenRateUpdateAsync(TokenId);
        }

        private void _rateServiceClient_TokenRateUpdateReceived(object sender, TokenRateUpdateReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public Task UnSubscribeAllRateUpdatesAsync()
        {
            return _rateServiceClient.UnSubscribeAllRateUpdatesAsync();
        }

        public Task UnSubscribeToAssetRateUpdateAsync(string AssetId)
        {
            return _rateServiceClient.UnSubscribeToAssetRateUpdateAsync(AssetId);
        }

        public Task UnSubscribeToCurrencyPairRateUpdateAsync(string Currency1, string Currency2)
        {
            return _rateServiceClient.UnSubscribeToCurrencyPairRateUpdateAsync(Currency1, Currency2);
        }

        public Task UnSubscribeToTokenPairRateUpdateAsync(string Token, string Token2)
        {
            return _rateServiceClient.UnSubscribeToTokenPairRateUpdateAsync(Token, Token2);
        }

        public Task UnSubscribeToTokenRateUpdateAsync(string TokenId)
        {
            return _rateServiceClient.UnSubscribeToTokenRateUpdateAsync(TokenId);
        }

        public async Task<TokenPairRateData> GetTokenPairRateAsync(string Token1Id, string Token2Id)
        {
            checkConnectionStatus();
            
            try
            {
                Console.WriteLine("Calling RateRepository GetTokenPairRateAsync on {0}/{1}", Token1Id, Token2Id);

                var result = await _rateServiceClient.GetTokenPairRateAsync(Token1Id, Token2Id);
                return result;
            }
            catch(Exception e)
            {
                Console.WriteLine("GetTokenPairRate Failed {0}", e.Message);
                throw new Exception("RateRepository GetTokenPairRate Failed", e);
            }
        }

        public async Task<TokenRateData> GetTokenRateAsync(string Token1Id)
        {
            return await _rateServiceClient.GetTokenRateAsync(Token1Id);
        }
    }
}
