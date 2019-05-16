using RateService;
using System;

namespace WCFRateTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running Test");
            TestRateManager();

            Console.WriteLine("Testing Completed ... Press any key to continue");
            Console.ReadKey();
        }

        

        static void TestRateManager()
        {
            RateService.RateServiceClient client = new RateServiceClient();

            //Console.WriteLine("Test the Get List Methods");

            //var assetList = client.GetAssetListAsync().GetAwaiter().GetResult();

            //foreach (string s in assetList)
            //    Console.WriteLine("Asset {0} returned", s);

            //var tokenList = client.GetTokenListAsync().GetAwaiter().GetResult();
            //foreach (string s in tokenList)
            //    Console.WriteLine("Token {0} returned", s);

            //Console.WriteLine("Press any key to continue");
            //Console.ReadKey();

            //Console.WriteLine("Asset Subscription Test");

            #region Test Asset Rate Subscription
            //client.AssetRateUpdateReceived += Client_AssetRateUpdateReceived;
            //client.SubscribeToAssetRateUpdateAsync("GOOG");
            //client.SubscribeToAssetRateUpdateAsync("AAPL");

            //Console.WriteLine("Press any key to continue");
            //Console.ReadKey();

            //client.UnSubscribeToAssetRateUpdateAsync("GOOG");
            //client.UnSubscribeToAssetRateUpdateAsync("AAPL");
            #endregion

            //Console.WriteLine("Token Subscription Test");
            //Console.ReadKey();

            #region Test Token Rate Subscription
            //client.TokenRateUpdateReceived += Client_TokenRateUpdateReceived;

            //client.SubscribeToTokenRateUpdateAsync("@GOOG");
            //client.SubscribeToTokenRateUpdateAsync("@AAPL");

            //Console.WriteLine("Press any key to continue");
            //Console.ReadKey();

            //client.UnSubscribeToTokenRateUpdateAsync("@GOOG");
            //client.UnSubscribeToTokenRateUpdateAsync("@AAPL");
            #endregion

            //Console.WriteLine("Test Completed");


            Console.WriteLine("Currency Pair Subscription Test");
            Console.ReadKey();

            client.CurrencyPairRateUpdateReceived += Client_CurrencyPairRateUpdateReceived;
            client.SubscribeToCurrencyPairRateUpdateAsync("EUR", "USD");

            Console.WriteLine("Subscribing to currency pair EUR/USD");
            Console.ReadKey();

            client.UnSubscribeToCurrencyPairRateUpdateAsync("EUR", "USD");
            Console.WriteLine("Unsubscribed from EUR/USD");
            Console.ReadKey();


            Console.WriteLine("Token Pair Subscription Test");
            Console.ReadKey();

            client.TokenPairRateUpdateReceived += Client_TokenPairRateUpdateReceived;
            client.SubscribeToTokenPairRateUpdateAsync("@AAPL", "@BNP");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();

            client.UnSubscribeToTokenPairRateUpdateAsync("@AAPL", "@BNP");

            Console.WriteLine("Unsubscribed from @AAPL/@BNP");
            Console.WriteLine("Test completed ...");


            Console.ReadKey();

        }

        private static void Client_TokenPairRateUpdateReceived(object sender, TokenPairRateUpdateReceivedEventArgs e)
        {
            Console.WriteLine("Token Pair Update for {0}/{1} Bid/Ask {2}/{3}", e.TokenPairRateRecord.Token1Id, e.TokenPairRateRecord.Token2Id,
                e.TokenPairRateRecord.BidRate, e.TokenPairRateRecord.AskRate);
        }

        private static void Client_CurrencyPairRateUpdateReceived(object sender, CurrencyPairRateUpdateReceivedEventArgs e)
        {
            Console.WriteLine("Currency Pair Rate Update for Asset {0}/{1} Bid/Ask {2}/{3}", e.CurrencyPairRateRecord.Currency1, e.CurrencyPairRateRecord.Currency2, e.CurrencyPairRateRecord.BidRate, e.CurrencyPairRateRecord.AskRate);
        }

        private static void Client_AssetRateUpdateReceived(object sender, RateService.AssetRateUpdateReceivedEventArgs e)
        {
            Console.WriteLine("Asset Rate Update for Asset {0} Bid/Ask {1}/{2}", e.AssetRateRecord.AssetId, e.AssetRateRecord.BidRate, e.AssetRateRecord.AskRate);
        }


        private static void Client_TokenRateUpdateReceived(object sender, RateService.TokenRateUpdateReceivedEventArgs e)
        {
            Console.WriteLine("Token Rate Update for Token {0} Bid/Ask {1}/{2}", e.TokenRateRecord.TokenId, e.TokenRateRecord.BidRate, e.TokenRateRecord.AskRate);
        }
    }
}
