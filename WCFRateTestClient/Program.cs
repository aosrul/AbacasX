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

            Console.WriteLine("Get the Token Rate List");

            foreach(TokenRateData t in client.GetTokenRateListAsync().GetAwaiter().GetResult())
            {
                Console.WriteLine("Token {0} Bid/Ask {1}/{2}", t.TokenId, t.BidRate, t.AskRate);
            }

                        
            Console.WriteLine("Testing Token Pair Rate Data for @AAPL-@BT.A");

            TokenPairRateData tokenPairRateData = client.GetTokenPairRateAsync("@AAPL", "@BT.A").GetAwaiter().GetResult();


            Console.WriteLine("Token Pair Rates");
            Console.WriteLine("Token Pair {0}/{1}  Bid/Offer {2}/{3}", tokenPairRateData.Token1Id, tokenPairRateData.Token2Id,
                tokenPairRateData.BidRate, tokenPairRateData.AskRate);
            Console.WriteLine("Token1 {0}  Bid/Offer {1}/{2}", tokenPairRateData.Token1Id, tokenPairRateData.Token1BidRate, tokenPairRateData.Token1AskRate);
            Console.WriteLine("Token2 {0}  Bid/Offer {1}/{2}", tokenPairRateData.Token2Id, tokenPairRateData.Token2BidRate, tokenPairRateData.Token2AskRate);

            Console.WriteLine();
            Console.WriteLine("Cross Currency Rates");
            Console.WriteLine("Currency Pair {0}-{1} Bid/Offer {2}/{3}", tokenPairRateData.Currency1, tokenPairRateData.Currency2, tokenPairRateData.CurrencyPairBidRate, tokenPairRateData.CurrencyPairAskRate);
            Console.WriteLine("Token1 Price Currency {0} Bid/Offer {1}/{2}", tokenPairRateData.Currency1, tokenPairRateData.Currency1BidRate, tokenPairRateData.Currency1AskRate);
            Console.WriteLine("Token2 Price Currency {0} Bid/Offer {1}/{2}", tokenPairRateData.Currency2, tokenPairRateData.Currency2BidRate, tokenPairRateData.Currency2AskRate);

            Console.WriteLine("Press Any Key");
            Console.ReadKey();


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


            //Console.WriteLine("Currency Pair Subscription Test");
            //Console.ReadKey();

            //client.CurrencyPairRateUpdateReceived += Client_CurrencyPairRateUpdateReceived;
            //client.SubscribeToCurrencyPairRateUpdateAsync("EUR", "USD");

            //Console.WriteLine("Subscribing to currency pair EUR/USD");
            //Console.ReadKey();

            //client.UnSubscribeToCurrencyPairRateUpdateAsync("EUR", "USD");
            //Console.WriteLine("Unsubscribed from EUR/USD");
            //Console.ReadKey();


            Console.WriteLine("Token Pair Subscription Test");
            Console.ReadKey();

            client.TokenPairRateUpdateReceived += Client_TokenPairRateUpdateReceived;
            client.SubscribeToTokenPairRateUpdateAsync("@AAPL", "@BT.A");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();

            client.UnSubscribeToTokenPairRateUpdateAsync("@AAPL", "@BT.A");

            Console.WriteLine("Unsubscribed from @AAPL/@BT.A");


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
