using AbacasWebX.Exchange.RateService;
using AbacasX.Model.DataContracts;
using AbacasX.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace AbacasWebX.Exchange.ExchangeSystem
{
    public class ExchangeBook
    {
        public delegate void NotifyOrderAddedDel(OrderLeg orderLegRecord);
        public delegate void NotifyOrderLegMatchDel(OrderLeg orderLegRecord, OrderFilledData orderFilledDataRecord);
        public delegate void NotifyOrderLegFilledDel(OrderLeg orderLegRecord);

        // Instance pointers must be initialized by method creating the Exchange Book
        public NotifyOrderAddedDel NotifyOrderAdded;
        public NotifyOrderLegMatchDel NotifyOrderLegMatched;
        public NotifyOrderLegFilledDel NotifyOrderLegFilled;

        public Dictionary<string, OrderBook> ExchangeTokenPairBook = new Dictionary<string, OrderBook>();
        public RateServiceClient rateServiceClient;
        RateServiceCallBack rateServiceCallBack = new RateServiceCallBack();

        public ExchangeBook()
        {
            rateServiceClient = new RateServiceClient(new InstanceContext(rateServiceCallBack));
        }

        public void AddTokenPairToExchange(string Token1Id, string Token2Id)
        {
            string TokenPairKey = Token1Id + "-" + Token2Id;
            OrderBook orderBook;

            if (ExchangeTokenPairBook.TryGetValue(TokenPairKey, out orderBook) == false)
            {
                orderBook = new OrderBook(this, TokenPairKey, Token1Id, Token2Id, rateServiceClient);

                // Order book should subscribe to token pair rate updates here !

                ExchangeTokenPairBook.Add(TokenPairKey, orderBook);
            }
        }

        public void AddOrderToExchange(OrderLeg orderLegRecord)
        {
            string TokenPairKey;
            string Token1Id;
            string Token2Id;
            OrderBook orderBook;

            TokenPairKey = orderLegRecord.Token1Id + "-" + orderLegRecord.Token2Id;
            Token1Id = orderLegRecord.Token1Id;
            Token2Id = orderLegRecord.Token2Id;

            // If the order book exists for the asset pair, then just add the order into the order book
            if (ExchangeTokenPairBook.TryGetValue(TokenPairKey, out orderBook) == true)
            {
                // Order book exists, so add the order to the order book
                orderBook.AddToOrderBook(orderLegRecord);
            }
            // If no order book exists for the asset pair, then create a new order book, add the order, and add the order book to the exchange asset pair list
            else
            {
                Console.WriteLine("Order Book created for Token Pair {0}-{1} with Key {2}", Token1Id, Token2Id, TokenPairKey);
                orderBook = new OrderBook(this, TokenPairKey, Token1Id, Token2Id, rateServiceClient);

                //// Link the order to the rate feed updates.
                //TokenPairRecord.bidRateChanged += orderBook.TokenPairBidRateChanged;
                //TokenPairRecord.askRateChanged += orderBook.TokenPairAskRateChanged;

                orderBook.AddToOrderBook(orderLegRecord);
                ExchangeTokenPairBook.Add(TokenPairKey, orderBook);
            }
        }
    }

    public class RateServiceCallBack : IRateServiceCallback
    {
        public void TokenRateUpdate(TokenRateData TokenRateRecord)
        {
            throw new NotImplementedException();
        }

        public void CurrencyPairRateUpdate(CurrencyPairRateData CurrencyPairRateRecord)
        {
            throw new NotImplementedException();
        }

        public void TokenPairRateUpdate(TokenPairRateData TokenPairRateRecord)
        {
            throw new NotImplementedException();
        }

        public void AssetRateUpdate(AssetRateData AssetRateRecord)
        {
            throw new NotImplementedException();
        }
    }

}