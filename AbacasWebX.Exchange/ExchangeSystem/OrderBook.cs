using AbacasX.Model.DataContracts;
using AbacasWebX.Exchange.RateService;
using AbacasX.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AbacasWebX.Exchange.ExchangeSystem
{

    public class OrderBook
    {
        private ExchangeBook _exchangeBook;
        RateServiceClient _rateServiceClient;

        LinkedList<WatchOrder> LimitBuyOrders = new LinkedList<WatchOrder>();
        LinkedList<WatchOrder> LimitSellOrders = new LinkedList<WatchOrder>();
        LinkedList<WatchOrder> MarketBuyOrders = new LinkedList<WatchOrder>();
        LinkedList<WatchOrder> MarketSellOrders = new LinkedList<WatchOrder>();
        LinkedList<WatchOrder> StopBuyOrders = new LinkedList<WatchOrder>();
        LinkedList<WatchOrder> StopSellOrders = new LinkedList<WatchOrder>();

        private string TokenPairKey { get; set; }
        public string Token1Id { get; set; }
        public string Token2Id { get; set; }

        public decimal BidRate { get; set; }
        public decimal AskRate { get; set; }

        public decimal BidAmount { get; set; }
        public decimal AskAmount { get; set; }

        public decimal BuyMarketOrdersOutstanding { get; set; }
        public decimal SellMarketOrdersOutstanding { get; set; }

        private object MarketBuyLock = new object();
        private object MarketSellLock = new object();
        private object LimitBuyLock = new object();
        private object LimitSellLock = new object();

        public OrderBook(ExchangeBook exchangeBook, string tokenPairKey, string token1Id, string token2Id, RateServiceClient rateServiceClient)
        {
            TokenPairKey = tokenPairKey;
            Token1Id = token1Id;
            Token2Id = token2Id;
            _rateServiceClient = rateServiceClient;
            _exchangeBook = exchangeBook;
        }

        public void AddToOrderBook(OrderLeg orderLeg)
        {
            _exchangeBook.NotifyOrderAdded(orderLeg);
            WatchOrder watchOrder = new WatchOrder(orderLeg);
            AddOrderToWatchList(watchOrder);
        }

        public void AddOrderToWatchList(WatchOrder watchRecord)
        {
            // Buy Limit Orders Descending Order
            if (watchRecord.orderLegRecord.OrderLegType == OrderLegTypeEnum.Limit)
            {
                if (watchRecord.orderLegRecord.BuySellType == OrderLegBuySellEnum.Buy)
                {
                    lock (LimitBuyLock)
                    {
                        LinkedListNode<WatchOrder> Node = LimitBuyOrders.First;

                        if (Node == null)
                        {
                            LimitBuyOrders.AddFirst(watchRecord);
                            UpdateOrderBookBid(watchRecord);
                        }
                        else
                        {
                            // Locate the Node with a price less than the current order
                            for (; (Node != null) && (Node.Value.orderLegRecord.OrderPrice >= watchRecord.orderLegRecord.OrderPrice); Node = Node.Next) ;

                            // End of the road, so add to the end of the list
                            if (Node == null)
                                LimitBuyOrders.AddLast(watchRecord);
                            // Insertion Point
                            else
                            {
                                if (Node.Previous == null)
                                    UpdateOrderBookBid(watchRecord);

                                LimitBuyOrders.AddBefore(Node, watchRecord);
                            }
                        }
                    }

                    MatchLimitBuyWithSells();
                }
                else // Sell Limit Order Ascending Order
                {
                    lock (LimitSellLock)
                    {
                        LinkedListNode<WatchOrder> Node = LimitSellOrders.First;

                        if (Node == null)
                        {
                            LimitSellOrders.AddFirst(watchRecord);
                            UpdateOrderBookAsk(watchRecord);
                        }
                        else
                        {
                            // Locate the Node with a price less than the current order
                            for (; (Node != null) && (Node.Value.orderLegRecord.OrderPrice <= watchRecord.orderLegRecord.OrderPrice); Node = Node.Next) ;

                            // End of the road, so add to the end of the list
                            if (Node == null)
                                LimitSellOrders.AddLast(watchRecord);
                            // Insertion Point
                            else
                            {
                                if (Node.Previous == null)
                                    UpdateOrderBookAsk(watchRecord);

                                LimitSellOrders.AddBefore(Node, watchRecord);
                            }
                        }
                    }

                    MatchLimitSellWithBuys();
                }
            }
            else if (watchRecord.orderLegRecord.OrderLegType == OrderLegTypeEnum.Stop)
            {
                if (watchRecord.BuySellType == OrderLegBuySellEnum.Buy)
                {
                    LinkedListNode<WatchOrder> Node = StopBuyOrders.First;


                    if (Node == null)
                        StopBuyOrders.AddFirst(watchRecord);
                    else
                    {
                        for (; (Node != null) && (Node.Value.OrderPrice <= watchRecord.OrderPrice); Node = Node.Next) ;

                        // End of the road, so add to the end of the list
                        if (Node == null)
                            StopBuyOrders.AddLast(watchRecord);
                        // Insertion Point
                        else
                            StopBuyOrders.AddBefore(Node, watchRecord);
                    }
                }
                else
                {
                    LinkedListNode<WatchOrder> Node = StopSellOrders.First;

                    if (Node == null)
                        StopSellOrders.AddFirst(watchRecord);
                    else
                    {
                        // Locate the Node with a price less than the current order

                        for (; (Node != null) && (Node.Value.OrderPrice >= watchRecord.OrderPrice); Node = Node.Next) ;

                        // End of the road, so add to the end of the list
                        if (Node == null)
                            StopSellOrders.AddLast(watchRecord);
                        // Insertion Point
                        else
                            StopSellOrders.AddBefore(Node, watchRecord);
                    }
                }
            }
            else if (watchRecord.orderLegRecord.OrderLegType == OrderLegTypeEnum.Market)
            {
                if (watchRecord.BuySellType == OrderLegBuySellEnum.Buy)
                {
                    lock (MarketBuyLock)
                    {
                        MarketBuyOrders.AddLast(watchRecord);

                        BuyMarketOrdersOutstanding += watchRecord.AmountOutstanding;
                    }


                    MatchMarketBuyWithSells();
                }
                else
                {
                    lock (MarketSellLock)
                    {
                        MarketSellOrders.AddLast(watchRecord);

                        SellMarketOrdersOutstanding += watchRecord.AmountOutstanding;
                    }

                    MatchMarketSellWithBuys();
                }
            }
            else
            {
                // Don't add it to the list
            }
        }

        public void MatchMarketBuyWithSells()
        {
            WatchOrder marketSellOrder;
            WatchOrder limitSellOrder;
            TokenPairRateData tokenPairRateRecord;

            // Nothing is available to offset the Buy Order
            if ((MarketSellOrders.Count == 0) && (LimitSellOrders.Count == 0))
                return;

            // Spin a thread to match the orders
            Task.Run(() =>
            {
                //Lock the Buy Orders, then Lock the Sell Orders
                lock (MarketBuyLock)
                {
                    foreach (WatchOrder buyOrder in MarketBuyOrders)
                    {
                        while ((buyOrder.AmountOutstanding > 0) && ((MarketSellOrders.Count > 0) || (LimitSellOrders.Count > 0)))
                        {
                            bool marketSellOrderUtilized = false;

                            lock (MarketSellLock)
                            {
                                if (MarketSellOrders.Count > 0)
                                {
                                    // Sell Orders are removed from the list if they are filled
                                    marketSellOrder = MarketSellOrders.First();

                                    tokenPairRateRecord = GetTokenPairRate(buyOrder.orderLegRecord.Token1Id, buyOrder.orderLegRecord.Token2Id);

                                    // Two market orders offsetting will be matched at the mid-price of the market
                                    buyOrder.OrderPrice = (Decimal)(tokenPairRateRecord.BidRate + tokenPairRateRecord.AskRate) / 2M;
                                    marketSellOrder.OrderPrice = buyOrder.OrderPrice;

                                    marketSellOrderUtilized = true;
                                    OffsetBuySellOrders(buyOrder, marketSellOrder);

                                    if (buyOrder.AmountOutstanding == 0)
                                    {
                                        MarkOrderFilled(buyOrder);
                                    }

                                    if (marketSellOrder.AmountOutstanding == 0)
                                    {
                                        MarkOrderFilled(marketSellOrder);
                                        MarketSellOrders.RemoveFirst();
                                    }

                                }
                            }


                            if (marketSellOrderUtilized == false)
                            {
                                lock (LimitSellLock)
                                {
                                    if (LimitSellOrders.Count > 0)
                                    {
                                        limitSellOrder = LimitSellOrders.First();

                                        OffsetBuySellOrders(buyOrder, limitSellOrder);

                                        if (buyOrder.AmountOutstanding == 0)
                                        {
                                            MarkOrderFilled(buyOrder);
                                        }

                                        if (limitSellOrder.AmountOutstanding == 0)
                                        {
                                            MarkOrderFilled(limitSellOrder);
                                            LimitSellOrders.RemoveFirst();
                                        }
                                    }
                                } // Release Lock on Limit Sell Orders
                            }
                        }
                    }

                    // Remove any Market Buy Orders that were filled
                    while ((MarketBuyOrders.Count > 0) && (MarketBuyOrders.First().AmountOutstanding == 0))
                        MarketBuyOrders.RemoveFirst();
                }
            });

            return;
        }

        public void MatchLimitBuyWithSells()
        {
            WatchOrder marketSellOrder;
            WatchOrder limitSellOrder;

            // Nothing is available to offset the Buy Order
            if ((MarketSellOrders.Count == 0) && (LimitSellOrders.Count == 0))
                return;

            // Spin a thread to match the orders
            Task.Run(() =>
            {
                // Lock Order is Market Buy -> Market Sell -> Limit Buy -> Limit Sell
                lock (MarketSellLock)
                {
                    lock (LimitBuyLock)
                    {
                        foreach (WatchOrder buyOrder in LimitBuyOrders)
                        {
                            while ((buyOrder.AmountOutstanding > 0) && ((MarketSellOrders.Count > 0) || (LimitSellOrders.Count > 0)))
                            {
                                bool marketSellOrderUtilized = false;

                                if (MarketSellOrders.Count > 0)
                                {
                                    // Sell Orders are removed from the list if they are filled
                                    marketSellOrder = MarketSellOrders.First();

                                    // Try to offset with Market Sell Orders

                                    marketSellOrderUtilized = true;
                                    OffsetBuySellOrders(buyOrder, marketSellOrder);

                                    if (buyOrder.AmountOutstanding == 0)
                                    {
                                        MarkOrderFilled(buyOrder);
                                    }

                                    if (marketSellOrder.AmountOutstanding == 0)
                                    {
                                        MarkOrderFilled(marketSellOrder);
                                        MarketSellOrders.RemoveFirst();
                                    }
                                }



                                if (marketSellOrderUtilized == false)
                                {
                                    lock (LimitSellLock)
                                    {
                                        if (LimitSellOrders.Count > 0)
                                        {
                                            limitSellOrder = LimitSellOrders.First();

                                            if (buyOrder.OrderPrice < limitSellOrder.OrderPrice)
                                            {
                                                return;
                                            }


                                            OffsetBuySellOrders(buyOrder, limitSellOrder);

                                            if (buyOrder.AmountOutstanding == 0)
                                            {
                                                MarkOrderFilled(buyOrder);
                                            }

                                            if (limitSellOrder.AmountOutstanding == 0)
                                            {
                                                MarkOrderFilled(limitSellOrder);
                                                LimitSellOrders.RemoveFirst();
                                            }
                                        }
                                    } // Release Lock on Limit Sell Orders
                                }
                            }
                        }
                    }

                    // Remove any Market Buy Orders that were filled
                    while ((LimitBuyOrders.Count > 0) && (LimitBuyOrders.First().AmountOutstanding == 0))
                        LimitBuyOrders.RemoveFirst();
                }
            });

            return;
        }

        public void MarkOrderFilled(WatchOrder watchOrder)
        {
            // This will ultimately be replaced with logic calling into the repository 
            //
            //Console.WriteLine("Order ID {0} Leg ID {1} is filled", watchOrder.orderLegRecord.OrderId, watchOrder.orderLegRecord.OrderLegId);
            watchOrder.orderLegRecord.OrderLegFillStatus = OrderLegFillStatusEnum.Full;
            _exchangeBook.NotifyOrderLegFilled(watchOrder.orderLegRecord);
        }

        public TokenPairRateData GetTokenPairRate(string token1Id, string token2Id)
        {
            TokenPairRateData tokenPairRateData = null;

            // This will need to be changed to a subscription, otherwise there will be a lag on each invocation
            // which is not acceptable.
            // Todo: Replace this with a subscription to a service reference that is listening to the rates rather than
            // asking for the rate and waiting.

            tokenPairRateData = _rateServiceClient.GetTokenPairRate(token1Id, token2Id);

            if (tokenPairRateData == null)
            {
                throw new Exception(string.Format("Error: Token Pair Rate Unavailable {0}/{1}", token1Id, token2Id));
            }

            return tokenPairRateData;
        }

        public void MatchMarketSellWithBuys()
        {
            WatchOrder marketBuyOrder;
            WatchOrder limitBuyOrder;
            TokenPairRateData tokenPairRateRecord;

            // Check to see that there are offsetting records
            if ((MarketBuyOrders.Count == 0) && (LimitBuyOrders.Count == 0))
                return;

            // Spin a thread to match the orders
            Task.Run(() =>
            {

                lock (MarketBuyLock)  // Start with the Buy Lock to avoid deadlock with MatchMarketBuyWithSells()
                {
                    lock (MarketSellLock)
                    {
                        foreach (WatchOrder sellOrder in MarketSellOrders)
                        {
                            while ((sellOrder.AmountOutstanding > 0) && ((MarketBuyOrders.Count > 0) || (LimitBuyOrders.Count > 0)))
                            {
                                bool marketbuyOrderUtilized = false;


                                if (MarketBuyOrders.Count > 0)
                                {
                                    // Sell Orders are removed from the list if they are filled
                                    marketBuyOrder = MarketBuyOrders.First();

                                    tokenPairRateRecord = GetTokenPairRate(sellOrder.orderLegRecord.Token1Id, sellOrder.orderLegRecord.Token2Id);
                                    sellOrder.OrderPrice = (Decimal)(tokenPairRateRecord.BidRate + tokenPairRateRecord.AskRate) / 2M;
                                    marketBuyOrder.OrderPrice = sellOrder.OrderPrice;

                                    // Try to offset with Market Sell Orders

                                    marketbuyOrderUtilized = true;
                                    OffsetBuySellOrders(marketBuyOrder, sellOrder);

                                    if (sellOrder.AmountOutstanding == 0)
                                    {
                                        MarkOrderFilled(sellOrder);
                                    }

                                    if (marketBuyOrder.AmountOutstanding == 0)
                                    {
                                        MarkOrderFilled(marketBuyOrder);
                                        MarketBuyOrders.RemoveFirst();
                                    }
                                }

                                if (marketbuyOrderUtilized == false)
                                {
                                    lock (LimitBuyLock)
                                    {
                                        if (LimitBuyOrders.Count > 0)
                                        {
                                            limitBuyOrder = LimitBuyOrders.First();

                                            OffsetBuySellOrders(limitBuyOrder, sellOrder);

                                            if (sellOrder.AmountOutstanding == 0)
                                            {
                                                MarkOrderFilled(sellOrder);
                                            }

                                            if (limitBuyOrder.AmountOutstanding == 0)
                                            {
                                                MarkOrderFilled(limitBuyOrder);
                                                LimitBuyOrders.RemoveFirst();
                                            }
                                        }
                                    } // Release Lock on Limit Sell Orders
                                }
                            }
                        }
                    }

                    // Remove any Market Buy Orders that were filled
                    while ((MarketSellOrders.Count > 0) && (MarketSellOrders.First().AmountOutstanding == 0))
                        MarketSellOrders.RemoveFirst();
                }
            });

            return;
        }

        public void MatchLimitSellWithBuys()
        {
            WatchOrder marketBuyOrder;
            WatchOrder limitBuyOrder;

            // Check to see that there are offsetting records
            if ((MarketBuyOrders.Count == 0) && (LimitBuyOrders.Count == 0))
                return;

            // Spin a thread to match the orders
            Task.Run(() =>
            {

                lock (MarketBuyLock)  // Start with the Buy Lock to avoid deadlock with MatchMarketBuyWithSells()
                {
                    lock (LimitBuyLock)
                    {
                        lock (LimitSellLock)
                        {

                            foreach (WatchOrder sellOrder in LimitSellOrders)
                            {
                                while ((sellOrder.AmountOutstanding > 0) && ((MarketBuyOrders.Count > 0) || (LimitBuyOrders.Count > 0)))
                                {
                                    bool marketbuyOrderUtilized = false;


                                    if (MarketBuyOrders.Count > 0)
                                    {
                                        // Sell Orders are removed from the list if they are filled
                                        marketBuyOrder = MarketBuyOrders.First();

                                        // Try to offset with Market Sell Orders

                                        marketbuyOrderUtilized = true;
                                        OffsetBuySellOrders(marketBuyOrder, sellOrder);

                                        if (sellOrder.AmountOutstanding == 0)
                                        {
                                            MarkOrderFilled(sellOrder);
                                        }

                                        if (marketBuyOrder.AmountOutstanding == 0)
                                        {
                                            MarkOrderFilled(marketBuyOrder);
                                            MarketBuyOrders.RemoveFirst();
                                        }
                                    }

                                    if (marketbuyOrderUtilized == false)
                                    {
                                        if (LimitBuyOrders.Count > 0)
                                        {
                                            limitBuyOrder = LimitBuyOrders.First();

                                            // No further orders to offset
                                            if (limitBuyOrder.OrderPrice < sellOrder.OrderPrice)
                                            {
                                                return;
                                            }


                                            OffsetBuySellOrders(limitBuyOrder, sellOrder);

                                            if (sellOrder.AmountOutstanding == 0)
                                            {
                                                MarkOrderFilled(sellOrder);
                                            }

                                            if (limitBuyOrder.AmountOutstanding == 0)
                                            {
                                                MarkOrderFilled(limitBuyOrder);
                                                LimitBuyOrders.RemoveFirst();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Remove any Market Buy Orders that were filled
                    while ((LimitSellOrders.Count > 0) && (LimitSellOrders.First().AmountOutstanding == 0))
                        LimitSellOrders.RemoveFirst();
                }
            });

            return;
        }

        public void OffsetBuySellOrders(WatchOrder buyOrder, WatchOrder sellOrder)
        {
            decimal AmountOffset = 0;
            decimal OrderPrice = 0;

            if (buyOrder.AmountOutstanding > sellOrder.AmountOutstanding)
            {
                AmountOffset = sellOrder.AmountOutstanding;
            }
            else
            {
                AmountOffset = buyOrder.AmountOutstanding;
            }

            buyOrder.orderLegRecord.Token1AmountFilled += AmountOffset;
            sellOrder.orderLegRecord.Token1AmountFilled += AmountOffset;

            if (buyOrder.orderLegRecord.OrderLegType == OrderLegTypeEnum.Market)
                BuyMarketOrdersOutstanding -= AmountOffset;

            if (sellOrder.orderLegRecord.OrderLegType == OrderLegTypeEnum.Market)
                SellMarketOrdersOutstanding -= AmountOffset;

            if (buyOrder.orderLegRecord.OrderLegType == OrderLegTypeEnum.Market)
            {
                if (sellOrder.orderLegRecord.OrderLegType == OrderLegTypeEnum.Market)
                {
                    // Market Price was established when orders were paired
                    OrderPrice = sellOrder.OrderPrice;
                }
                else
                {
                    // Order Price is the Limit Price on the Sell Order;
                    OrderPrice = sellOrder.OrderPrice;
                }
            }
            else if (buyOrder.orderLegRecord.OrderLegType == OrderLegTypeEnum.Limit)
            {
                if (sellOrder.orderLegRecord.OrderLegType == OrderLegTypeEnum.Market)
                {
                    OrderPrice = buyOrder.OrderPrice;
                }
                else
                {
                    OrderPrice = buyOrder.OrderPrice;
                }
            }
            else
            {
                throw new NotImplementedException("Order Type not Implemented");
            }

            // Update the Database to reflect
            // the changes to the Order Legs and the amounts filled for each order leg
            SaveFilledOrders(buyOrder, sellOrder, AmountOffset, OrderPrice);

            //Console.WriteLine("Matched Asset Pair {0}-{1} Order {2}/{3} - {4}/{5} for {6}", buyOrder.AssetID1, buyOrder.AssetID2, buyOrder.OrderLegID, buyOrder.OrderType.ToString(), sellOrder.OrderLegID, sellOrder.OrderType.ToString(), AmountOffset);
            //Console.WriteLine("Asset Pair {0}-{1} Outstanding Mkt Buy {2} / Mkt Sell {3}", buyOrder.AssetID1, buyOrder.AssetID2, BuyMarketOrdersOutstanding, SellMarketOrdersOutstanding);
            //Console.WriteLine("Buy  Order {0} Type {1} Outstanding {2}", buyOrder.orderLegRecord.OrderLegID, buyOrder.OrderType.ToString(), buyOrder.AmountOutstanding);
            //Console.WriteLine("Sell Order {0} Type {1} Outstanding {2}", sellOrder.orderLegRecord.OrderLegID, sellOrder.OrderType.ToString(), sellOrder.AmountOutstanding);
        }

        public void SaveFilledOrders(WatchOrder buyOrder, WatchOrder sellOrder, decimal AmountOffset, decimal OrderPrice)
        {
            OrderFilledData orderFilledDataRecord = new OrderFilledData();

            orderFilledDataRecord.OrderLegId = buyOrder.orderLegRecord.OrderLegId;
            orderFilledDataRecord.Token1Amount = AmountOffset;
            orderFilledDataRecord.Token1Id = buyOrder.orderLegRecord.Token1Id;
            orderFilledDataRecord.Token2Id = buyOrder.orderLegRecord.Token2Id;
            orderFilledDataRecord.Token2Amount = AmountOffset * (buyOrder.orderLegRecord.OrderPriceTerms == OrderPriceTermsEnum.Token2PerToken1 ?
                OrderPrice : 1.0M / OrderPrice);
            orderFilledDataRecord.OrderPriceTerms = buyOrder.orderLegRecord.OrderPriceTerms;
            orderFilledDataRecord.FilledDateTime = System.DateTime.Now;

            _exchangeBook.NotifyOrderLegMatched(buyOrder.orderLegRecord, orderFilledDataRecord);
            _exchangeBook.NotifyOrderLegMatched(sellOrder.orderLegRecord, orderFilledDataRecord);

            //Console.WriteLine("Updating Order Book for Order {0}/{1} offset amount {2} at price {3}", buyOrder.orderLegRecord.OrderId, sellOrder.orderLegRecord.OrderId, AmountOffset, OrderPrice);
        }

        public void UpdateOrderBookBid(WatchOrder OrderWatchRecord)
        {
            BidRate = OrderWatchRecord.OrderPrice;
            BidAmount = OrderWatchRecord.AmountOutstanding;
        }

        public void UpdateOrderBookAsk(WatchOrder OrderWatchRecord)
        {
            AskRate = OrderWatchRecord.OrderPrice;
            AskAmount = OrderWatchRecord.AmountOutstanding;
        }
    }

}