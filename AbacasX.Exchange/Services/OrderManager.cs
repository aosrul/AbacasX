using AbacasX.Exchange.Contracts;
using AbacasX.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.Threading;
using System.Collections.Concurrent;
using System.Security.Cryptography;

public class KeyPairData
{
    public string publicKey;
    public string privateKey;
}

namespace AbacasX.Exchange.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    class OrderManager : IOrderService
    {
        ConcurrentDictionary<int, OrderData> ClientOrders = new ConcurrentDictionary<int, OrderData>();
        ConcurrentDictionary<int, OrderData> ClientHistoricalOrders = new ConcurrentDictionary<int, OrderData>();
        ConcurrentDictionary<string, ClientPositionData> ClientPositions = new ConcurrentDictionary<string, ClientPositionData>();
        ConcurrentDictionary<string, TokenRateData> TokenRates = new ConcurrentDictionary<string, TokenRateData>();
        ConcurrentDictionary<int, KeyPairData> KeyPairs = new ConcurrentDictionary<int, KeyPairData>();
        ConcurrentDictionary<int, BlockChainData> BlockChainTransactions = new ConcurrentDictionary<int, BlockChainData>();
        ConcurrentDictionary<string, AssetDepositData> DepositNotifications = new ConcurrentDictionary<string, AssetDepositData>();
        ConcurrentDictionary<string, AssetWithdrawalData> WithdrawalRequests = new ConcurrentDictionary<string, AssetWithdrawalData>();
        ConcurrentDictionary<string, AssetTransferData> AssetTransfers = new ConcurrentDictionary<string, AssetTransferData>();



        static int orderCount = 20150;
        static int blockCount = 10015;

        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(5000);
        private readonly Timer _timer;
        public object OrderListLock = new object();
        private volatile bool _fillingOpenOrders = false;
        private int orderFillCount = 0;

        public OrderManager()
        {
            Console.Title = "AbacasX Exchange Service";


            ClientPositionData basePosition = new ClientPositionData { TokenId = "@USD", TokenAmount = 1000000m, TokenRate = 1.0m, TokenRateIn = "USD", TokenValue = 1000000 };

            ClientPositionData baseEURPosition = new ClientPositionData { TokenId = "@GBP", TokenAmount = 200000m, TokenRate = 1.26m, TokenRateIn = "USD", TokenValue = 252000.0m };

            OrderData[] HistoricalOrderList = { new OrderData { OrderId = orderCount++, BuySellType = OrderLegBuySellEnum.Buy, ClientAccountId = 0, ClientId = 0, OrderPrice = 101.10M, OrderPriceTerms = OrderPriceTermsEnum.Token2PerToken1, OrderType = OrderTypeEnum.Limit, Token1Id = "@MSFT", Token1Amount = 500, Token2Id = "@USD", OrderStatus = OrderStatusEnum.Filled, PriceFilled = 99.5M},
                                                new OrderData { OrderId = orderCount++, BuySellType = OrderLegBuySellEnum.Buy, ClientAccountId = 0, ClientId = 0, OrderPrice = 1282.00M, OrderPriceTerms = OrderPriceTermsEnum.Token2PerToken1, OrderType = OrderTypeEnum.Market, Token1Id = "@GOOG", Token1Amount = 300,  Token2Id = "@USD", OrderStatus = OrderStatusEnum.Filled, PriceFilled = 1250.00M } };

            // Initialize USD, EUR Position
            ClientPositions.TryAdd(basePosition.TokenId, basePosition);
            ClientPositions.TryAdd(baseEURPosition.TokenId, baseEURPosition);

            TokenRateData[] TokenRatesList =
            {
                new TokenRateData { TokenId = "@GOOG", TokenRate = 1036.06m, TokenRateIn = "USD"},
                new TokenRateData { TokenId = "@MSFT", TokenRate = 103.9m, TokenRateIn = "USD"},
                new TokenRateData { TokenId = "@CAT", TokenRate = 127.02M, TokenRateIn = "USD"},
                new TokenRateData { TokenId = "@ETH", TokenRate = 159m, TokenRateIn = "USD"},
                new TokenRateData { TokenId = "@BTC", TokenRate = 3860.0m, TokenRateIn = "USD"},
                new TokenRateData { TokenId = "@USD", TokenRate = 1.0m, TokenRateIn = "USD"},
                new TokenRateData { TokenId = "@AAPL", TokenRate = 159.9m, TokenRateIn = "USD"},
                new TokenRateData { TokenId = "@GOLD", TokenRate = 1292.0m, TokenRateIn = "USD"},
                new TokenRateData { TokenId = "@TOYOTA", TokenRate = 58.77m, TokenRateIn = "USD"},
                new TokenRateData { TokenId = "@BT", TokenRate = 15.20m, TokenRateIn = "USD"},
                new TokenRateData { TokenId = "@BNP", TokenRate = 39.475m, TokenRateIn = "EUR"},
                new TokenRateData { TokenId = "@EUR", TokenRate = 1.15m, TokenRateIn = "USD"},
            };

            for (int i = 0; i < TokenRatesList.Count(); i++)
            {
                TokenRates.TryAdd(TokenRatesList[i].TokenId, TokenRatesList[i]);
            }

            for (int i = 0; i < HistoricalOrderList.Count(); i++)
            {
                HistoricalOrderList[i].Token2Amount = HistoricalOrderList[i].Token1Amount * HistoricalOrderList[i].PriceFilled;
                ClientHistoricalOrders.TryAdd(HistoricalOrderList[i].OrderId, HistoricalOrderList[i]);
                addOrderToPosition(HistoricalOrderList[i]);
            }

            var result = CreateKeyPair();

            KeyPairData KeyPair = new KeyPairData();
            KeyPair.privateKey = result.Item1;
            KeyPair.publicKey = result.Item2;

            KeyPairs.TryAdd(0, KeyPair);

            result = CreateKeyPair();

            KeyPair.privateKey = result.Item1;
            KeyPair.publicKey = result.Item2;

            KeyPairs.TryAdd(1, KeyPair);

            _timer = new Timer(FillOpenOrders, null, _updateInterval, _updateInterval);
        }

        public static Tuple<string, string> CreateKeyPair()
        {
            CspParameters cspParams = new CspParameters { ProviderType = 1 };

            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(1024, cspParams);

            string publicKey = Convert.ToBase64String(rsaProvider.ExportCspBlob(false));
            string privateKey = Convert.ToBase64String(rsaProvider.ExportCspBlob(true));

            return new Tuple<string, string>(privateKey, publicKey);
        }

        private void addOrderToPosition(OrderData order)
        {
            ClientPositionData ClientPosition = new ClientPositionData();
            TokenRateData TokenRate = new TokenRateData();


            if (order.BuySellType == OrderLegBuySellEnum.Buy)
            {
                if (ClientPositions.TryGetValue(order.Token1Id, out ClientPosition) == true)
                {
                    ClientPosition.TokenAmount += order.Token1Amount;
                    ClientPosition.TokenValue = ClientPosition.TokenAmount * ClientPosition.TokenRate;
                }
                else
                {
                    ClientPosition = new ClientPositionData();

                    ClientPosition.TokenId = order.Token1Id;
                    ClientPosition.TokenAmount = order.Token1Amount;

                    ClientPosition.TokenRate = 0.0m;
                    ClientPosition.TokenRateIn = "USD";

                    if (TokenRates.TryGetValue(ClientPosition.TokenId, out TokenRate) == true)
                    {
                        ClientPosition.TokenRate = TokenRate.TokenRate;
                        ClientPosition.TokenRateIn = TokenRate.TokenRateIn;
                    }

                    ClientPosition.TokenValue = ClientPosition.TokenAmount * ClientPosition.TokenRate;
                    ClientPositions.TryAdd(ClientPosition.TokenId, ClientPosition);
                }


                if (ClientPositions.TryGetValue(order.Token2Id, out ClientPosition) == true)
                {
                    ClientPosition.TokenAmount -= order.Token2Amount;
                    ClientPosition.TokenValue = ClientPosition.TokenAmount * ClientPosition.TokenRate;
                }
            }
            else
            {
                if (ClientPositions.TryGetValue(order.Token2Id, out ClientPosition) == true)
                {
                    ClientPosition.TokenAmount += order.Token2Amount;
                    ClientPosition.TokenValue = ClientPosition.TokenAmount * ClientPosition.TokenRate;
                }
                else
                {
                    ClientPosition = new ClientPositionData();
                    ClientPosition.TokenId = order.Token2Id;
                    ClientPosition.TokenAmount = order.Token2Amount;

                    ClientPosition.TokenRate = 0.0m;
                    ClientPosition.TokenRateIn = "USD";

                    if (TokenRates.TryGetValue(ClientPosition.TokenId, out TokenRate) == true)
                    {
                        ClientPosition.TokenRate = TokenRate.TokenRate;
                        ClientPosition.TokenRateIn = TokenRate.TokenRateIn;
                    }

                    ClientPosition.TokenValue = ClientPosition.TokenAmount * ClientPosition.TokenRate;
                    ClientPositions.TryAdd(ClientPosition.TokenId, ClientPosition);
                }

                if (ClientPositions.TryGetValue(order.Token1Id, out ClientPosition) == true)
                {
                    ClientPosition.TokenAmount -= order.Token1Amount;
                    ClientPosition.TokenValue = ClientPosition.TokenAmount * ClientPosition.TokenRate;
                }
            }
        }

        private string HashOrder(BlockChainData blockRecord)
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(blockRecord);

            var resultHash = ComputeHashCode(Encoding.UTF8.GetBytes(jsonString));

            return Convert.ToBase64String(resultHash);
        }

        public static byte[] ComputeHashCode(byte[] toBeHashed)
        {
            using (var md5 = SHA512.Create())
            {
                return md5.ComputeHash(toBeHashed);
            }
        }

        private void FillOpenOrders(object state)
        {
            lock (OrderListLock)
            {
                if (!_fillingOpenOrders)
                {
                    _fillingOpenOrders = true;

                    Console.WriteLine("Filling Open Orders .. Count {0}", orderFillCount++);

                    foreach (var order in ClientOrders)
                    {
                        OrderData orderFilled;

                        order.Value.OrderStatus = OrderStatusEnum.Filled;
                        order.Value.PriceFilled = order.Value.OrderPrice;

                        ClientHistoricalOrders.TryAdd(order.Value.OrderId, order.Value);
                        ClientOrders.TryRemove(order.Value.OrderId, out orderFilled);
                        addOrderToPosition(orderFilled);

                        Console.WriteLine("Order {0} Filled", orderFilled.OrderId);

                        UpdateBlockChain(orderFilled);
                    }

                    // Add the processing of transfer requests

                    Console.WriteLine("Processing Asset Transfer Requests");

                    foreach (var transfer in AssetTransfers)
                    {
                        AssetTransferData transferRequest = transfer.Value;

                        if (transferRequest.transferStatus == TransferStatusEnum.InProgress)
                        {
                            if (transferRequest.transferType == TransferTypeEnum.Deposit)
                            {
                                AssetDepositData depositRecord;

                                addTransferToPosition(transferRequest);
                                addTransferToBlockChain(transferRequest);

                                // Update the Transfer Activity to Completed, and remove the deposit notification
                                transferRequest.transferStatus = TransferStatusEnum.Completed;
                                DepositNotifications.TryRemove(transferRequest.referenceId, out depositRecord);
                            }
                            else
                            {

                                AssetWithdrawalData withdrawalRecord;

                                addTransferToPosition(transferRequest);
                                addTransferToBlockChain(transferRequest);

                                // Update the Transfer Activity to Completed, and remove the deposit notification
                                transferRequest.transferStatus = TransferStatusEnum.Completed;
                                WithdrawalRequests.TryRemove(transferRequest.referenceId, out withdrawalRecord);
                            }
                        }
                    }

                    _fillingOpenOrders = false;
                }
            }
        }


        private void addTransferToBlockChain(AssetTransferData transferRequest)
        {
            BlockChainData blockChainRecord = new BlockChainData();
            KeyPairData KeyPair = new KeyPairData();

            blockChainRecord = new BlockChainData();

            blockChainRecord.Date = DateTime.Now.ToShortDateString();
            blockChainRecord.BlockNumber = blockCount++;
            blockChainRecord.OrderId = orderCount++;

            if (transferRequest.transferType == TransferTypeEnum.Deposit)
            {
                blockChainRecord.PayReceive = "Receive";
                blockChainRecord.TokenAmount = transferRequest.tokenAmount;
                blockChainRecord.TokenId = transferRequest.tokenId;
            }
            else
            {
                blockChainRecord.PayReceive = "Pay";
                blockChainRecord.TokenAmount = transferRequest.tokenAmount;
                blockChainRecord.TokenId = transferRequest.tokenId;
            }

            if (KeyPairs.TryGetValue(1, out KeyPair) == true)
            {
                blockChainRecord.Address = KeyPair.publicKey;
            }

            blockChainRecord.TransactionHash = HashOrder(blockChainRecord);
            BlockChainTransactions.TryAdd(blockChainRecord.BlockNumber, blockChainRecord);
        }

        private void addTransferToPosition(AssetTransferData transferRequest)
        {
            ClientPositionData ClientPosition = new ClientPositionData();
            TokenRateData TokenRate = new TokenRateData();

            if (transferRequest.transferType == TransferTypeEnum.Deposit)
            {
                if (ClientPositions.TryGetValue(transferRequest.tokenId, out ClientPosition) == true)
                {
                    ClientPosition.TokenAmount += transferRequest.tokenAmount;
                    ClientPosition.TokenValue = ClientPosition.TokenAmount * ClientPosition.TokenRate;
                }
                else
                {
                    ClientPosition = new ClientPositionData();

                    ClientPosition.TokenId = transferRequest.tokenId;
                    ClientPosition.TokenAmount = transferRequest.tokenAmount;

                    ClientPosition.TokenRate = 0.0m;
                    ClientPosition.TokenRateIn = "USD";

                    if (TokenRates.TryGetValue(ClientPosition.TokenId, out TokenRate) == true)
                    {
                        ClientPosition.TokenRate = TokenRate.TokenRate;
                        ClientPosition.TokenRateIn = TokenRate.TokenRateIn;
                    }

                    ClientPosition.TokenValue = ClientPosition.TokenAmount * ClientPosition.TokenRate;
                    ClientPositions.TryAdd(ClientPosition.TokenId, ClientPosition);
                }
            }
            else
            {
                if (ClientPositions.TryGetValue(transferRequest.tokenId, out ClientPosition) == true)
                {
                    ClientPosition.TokenAmount -= transferRequest.tokenAmount;
                    ClientPosition.TokenValue = ClientPosition.TokenAmount * ClientPosition.TokenRate;
                }
            }
        }

        private void UpdateBlockChain(OrderData order)
        {
            BlockChainData blockChainRecord = new BlockChainData();
            KeyPairData KeyPair = new KeyPairData();

            Console.WriteLine("Writing Order Flows to blockchain");

            if (order.BuySellType == OrderLegBuySellEnum.Buy)
            {
                blockChainRecord = new BlockChainData();

                blockChainRecord.Date = DateTime.Now.ToShortDateString();
                blockChainRecord.BlockNumber = blockCount++;
                blockChainRecord.OrderId = order.OrderId;
                blockChainRecord.PayReceive = "Receive";
                blockChainRecord.TokenAmount = order.Token1Amount;
                blockChainRecord.TokenId = order.Token1Id;
                if (KeyPairs.TryGetValue(1, out KeyPair) == true)
                {
                    blockChainRecord.Address = KeyPair.publicKey;
                }

                blockChainRecord.TransactionHash = HashOrder(blockChainRecord);
                BlockChainTransactions.TryAdd(blockChainRecord.BlockNumber, blockChainRecord);


                blockChainRecord = new BlockChainData();
                blockChainRecord.Date = DateTime.Now.ToShortDateString();
                blockChainRecord.BlockNumber = blockCount++;
                blockChainRecord.OrderId = order.OrderId;
                blockChainRecord.PayReceive = "Pay";
                blockChainRecord.TokenAmount = order.Token2Amount;
                blockChainRecord.TokenId = order.Token2Id;

                if (KeyPairs.TryGetValue(0, out KeyPair) == true)
                {
                    blockChainRecord.Address = KeyPair.publicKey;
                }

                blockChainRecord.TransactionHash = HashOrder(blockChainRecord);
                BlockChainTransactions.TryAdd(blockChainRecord.BlockNumber, blockChainRecord);
            }
            else
            {
                blockChainRecord = new BlockChainData();
                blockChainRecord.Date = DateTime.Now.ToShortDateString();


                blockChainRecord.BlockNumber = blockCount++;
                blockChainRecord.OrderId = order.OrderId;
                blockChainRecord.PayReceive = "Pay";
                blockChainRecord.TokenAmount = order.Token1Amount;
                blockChainRecord.TokenId = order.Token1Id;

                if (KeyPairs.TryGetValue(0, out KeyPair) == true)
                {
                    blockChainRecord.Address = KeyPair.publicKey;
                }

                blockChainRecord.TransactionHash = HashOrder(blockChainRecord);
                BlockChainTransactions.TryAdd(blockChainRecord.BlockNumber, blockChainRecord);

                blockChainRecord = new BlockChainData();
                blockChainRecord.Date = DateTime.Now.ToShortDateString();


                blockChainRecord.BlockNumber = blockCount++;
                blockChainRecord.OrderId = order.OrderId;
                blockChainRecord.PayReceive = "Receive";
                blockChainRecord.TokenAmount = order.Token2Amount;
                blockChainRecord.TokenId = order.Token2Id;
                if (KeyPairs.TryGetValue(1, out KeyPair) == true)
                {
                    blockChainRecord.Address = KeyPair.publicKey;
                }

                blockChainRecord.TransactionHash = HashOrder(blockChainRecord);
                BlockChainTransactions.TryAdd(blockChainRecord.BlockNumber, blockChainRecord);
            }
        }

        public int ActivateOrder(int OrderId)
        {
            return (0);
        }

        public List<BlockChainData> GetClientBlockChainTransactions(int clientId)
        {
            Console.WriteLine("TradezDigital Request for Blockchain Transactions");
            Console.WriteLine("{0} Transactions Returned", BlockChainTransactions.Count());
            return BlockChainTransactions.Values.ToList();
        }

        public List<ClientPositionData> GetClientPositions(int ClientId)
        {
            Console.WriteLine("TradezDigital Request for Positions");
            Console.WriteLine("{0} Positions Returned", ClientPositions.Count());
            return ClientPositions.Values.ToList();
        }

        public List<OrderData> GetClientOrders(int ClientId)
        {
            Console.WriteLine("TradezDigital Request for Open Orders");
            Console.WriteLine("{0} Orders Returned", ClientOrders.Values.Count());

            return ClientOrders.Values.ToList();
        }

        public List<OrderData> GetClientHistoricalOrders(int ClientId)
        {
            Console.WriteLine("TradezDigital Request for Historical Orders");
            Console.WriteLine("{0} Historical Returned", ClientHistoricalOrders.Count());
            return ClientHistoricalOrders.Values.ToList();
        }


        public OrderData GetOrder(int OrderId)
        {
            OrderData OrderRecord = null;

            OrderRecord = new OrderData { OrderId = 1, BuySellType = OrderLegBuySellEnum.Buy, ClientAccountId = 0, ClientId = 0, OrderPrice = 1, OrderPriceTerms = OrderPriceTermsEnum.Token1PerToken2, OrderType = OrderTypeEnum.Market, Token1Id = "AAPL", Token1Amount = 1000, Token2Id = "GOOG", Token2Amount = 100 };
            return OrderRecord;

            //ClientOrders.TryGetValue(OrderId, out OrderRecord);

            //return OrderRecord;
        }

        public OrderData AddOrder(OrderData orderData)
        {

            Console.WriteLine("TradezDigital -- Add Order");
            Console.WriteLine("Order Details {0}", Newtonsoft.Json.JsonConvert.SerializeObject(orderData));


            orderCount++;
            orderData.OrderId = orderCount;
            ClientOrders.TryAdd(orderData.OrderId, orderData);

            Console.WriteLine("Trade {0} Added", orderData.OrderId);

            return (orderData);
        }

        public int CancelOrder(int OrderId)
        {
            OrderData orderData;

            if (ClientOrders.TryGetValue(OrderId, out orderData))
            {
                ClientOrders.TryRemove(OrderId, out orderData);
            }

            return (0);
        }

        public int SuspendOrder(int OrderID)
        {
            return (0);
        }

        public string GetNewGuid()
        {
            return Guid.NewGuid().ToString();
        }

        public AssetDepositData AddDeposit(AssetDepositData depositNotification)
        {
            //depositNotification.referenceId = Guid.NewGuid().ToString();

            DepositNotifications.TryAdd(depositNotification.referenceId, depositNotification);

            Console.WriteLine("Asset Deposit Notification {0} Added", depositNotification.referenceId);

            AssetTransferData newTransfer = new AssetTransferData();

            newTransfer.referenceId = depositNotification.referenceId;
            newTransfer.assetId = depositNotification.assetId;
            newTransfer.assetAmount = depositNotification.amount;

            newTransfer.transferStatus = TransferStatusEnum.InProgress;
            newTransfer.transferType = TransferTypeEnum.Deposit;

            newTransfer.tokenId = '@' + depositNotification.assetId;
            newTransfer.tokenAmount = depositNotification.amount;
            newTransfer.forAccountOf = "TradezDigital";

            AssetTransfers.TryAdd(newTransfer.referenceId, newTransfer);
            Console.WriteLine("Asset Deposit Transfer Added {0}", newTransfer.referenceId);


            return depositNotification;
        }

        public AssetWithdrawalData AddWithdrawal(AssetWithdrawalData withdrawalRequest)
        {
            //withdrawalRequest.referenceId = Guid.NewGuid().ToString();
            WithdrawalRequests.TryAdd(withdrawalRequest.referenceId, withdrawalRequest);

            Console.WriteLine("Asset Withdrawal Notification {0} Added", withdrawalRequest.referenceId);

            AssetTransferData newTransfer = new AssetTransferData();

            newTransfer.referenceId = withdrawalRequest.referenceId;
            newTransfer.assetId = withdrawalRequest.tokenId.Substring(1);
            newTransfer.assetAmount = withdrawalRequest.amount;

            newTransfer.transferStatus = TransferStatusEnum.InProgress;
            newTransfer.transferType = TransferTypeEnum.Withdrawal;

            newTransfer.tokenId = withdrawalRequest.tokenId;
            newTransfer.tokenAmount = withdrawalRequest.amount;
            newTransfer.forAccountOf = "TradezDigital";

            AssetTransfers.TryAdd(newTransfer.referenceId, newTransfer);
            Console.WriteLine("Asset Withdrawal Transfer Added {0}", newTransfer.referenceId);

            return withdrawalRequest;
        }

        public List<AssetTransferData> GetClientTransferActivity(int ClientId)
        {
            return AssetTransfers.Values.ToList();
        }
    }
}
