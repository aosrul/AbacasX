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
using AbacasX.Exchange.ExchangeSystem;
using AbacasX.Model.Extensions;
using AbacasX.Model.DataContracts;

public class KeyPairData
{
    public string publicKey;
    public string privateKey;
}

public class ClientPosition
{
    public int ClientId;
    public ConcurrentDictionary<string, ClientPositionData> ClientPositions;

    public ClientPosition(int clientId)
    {
        ClientId = clientId;
        ClientPositions = new ConcurrentDictionary<string, ClientPositionData>();
    }
}

public class ClientFilledOrder
{
    public int ClientId;
    public ConcurrentDictionary<int, OrderFilledData> ClientTransactions;

    public ClientFilledOrder(int clientId)
    {
        ClientId = clientId;
        ClientTransactions = new ConcurrentDictionary<int, OrderFilledData>();
    }
}

namespace AbacasX.Exchange.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    class OrderManager : IOrderService
    {
        ConcurrentDictionary<int, OrderData> ClientOrders = new ConcurrentDictionary<int, OrderData>();
        ConcurrentDictionary<int, OrderData> ClientHistoricalOrders = new ConcurrentDictionary<int, OrderData>();
        ConcurrentDictionary<int, ClientFilledOrder> ClientFilledOrders = new ConcurrentDictionary<int, ClientFilledOrder>();

        // Positions for each client
        ConcurrentDictionary<int, ClientPosition> ClientPositions = new ConcurrentDictionary<int, ClientPosition>();

        ConcurrentDictionary<string, TokenRateData> TokenRates = new ConcurrentDictionary<string, TokenRateData>();
        ConcurrentDictionary<int, KeyPairData> KeyPairs = new ConcurrentDictionary<int, KeyPairData>();
        ConcurrentDictionary<int, BlockChainData> BlockChainTransactions = new ConcurrentDictionary<int, BlockChainData>();
        ConcurrentDictionary<string, AssetDepositData> DepositNotifications = new ConcurrentDictionary<string, AssetDepositData>();
        ConcurrentDictionary<string, AssetWithdrawalData> WithdrawalRequests = new ConcurrentDictionary<string, AssetWithdrawalData>();
        ConcurrentDictionary<string, AssetTransferData> AssetTransfers = new ConcurrentDictionary<string, AssetTransferData>();

        ExchangeBook _exchangeBook;

        static int orderCount = 20150;
        static int blockCount = 10015;
        static int orderFilledCount = 1;

        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(5000);

        private readonly Timer _timer;
        public object OrderListLock = new object();

        public object ClientFilledOrderLock = new object();
        public object ClientOrderLock = new object();
        public object ClientHistoricalOrderLock = new object();
        public object ClientPositionLock = new object();
        public object BlockChainTransactionsLock = new object();
        public object DepositNotificationLock = new object();
        public object WithdrawalRequestLock = new object();
        public object AssetTransferLock = new object();

        private volatile bool _fillingOpenOrders = false;

        public OrderManager()
        {

            Console.Title = "AbacasX Exchange Service";

            _exchangeBook = new ExchangeBook();

            _exchangeBook.NotifyOrderAdded = NotifyOrderLegAdded;
            _exchangeBook.NotifyOrderLegMatched = NotifyOrderLegMatched;
            _exchangeBook.NotifyOrderLegFilled = NotifyOrderLegFilled;

            var tokenRateList = _exchangeBook.rateServiceClient.GetTokenRateList();

            for (int i = 0; i < tokenRateList.Count(); i++)
            {
                TokenRates.TryAdd(tokenRateList[i].TokenId, tokenRateList[i]);
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

        public string GenerateNewClientKey(int ClientId)
        {
            KeyPairData KeyPair;

            if (KeyPairs.TryGetValue(ClientId, out KeyPair) == true)
            {
                return KeyPair.publicKey;
            }
            else
            {
                var result = CreateKeyPair();
                KeyPair = new KeyPairData();
                KeyPair.privateKey = result.Item1;
                KeyPair.publicKey = result.Item2;
                KeyPairs.TryAdd(ClientId, KeyPair);
                return (KeyPair.publicKey);
            }
        }

        public static Tuple<string, string> CreateKeyPair()
        {
            CspParameters cspParams = new CspParameters { ProviderType = 1 };

            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider(1024, cspParams);

            string publicKey = Convert.ToBase64String(rsaProvider.ExportCspBlob(false));
            string privateKey = Convert.ToBase64String(rsaProvider.ExportCspBlob(true));

            return new Tuple<string, string>(privateKey, publicKey);
        }

        private void addFilledOrderToPosition(OrderLeg orderLegRecord, OrderFilledData orderFilledDataRecord)
        {
            ClientPosition clientPositionRecord;

            ClientPositionData ClientPosition = new ClientPositionData();
            TokenRateData TokenRate = new TokenRateData();
            int clientId = orderLegRecord.Order.ClientId;


            if (ClientPositions.TryGetValue(clientId, out clientPositionRecord) == false)
            {
                clientPositionRecord = new ClientPosition(clientId);
                ClientPositions.TryAdd(clientId, clientPositionRecord);
            }


            if (clientPositionRecord == null)
            {
                throw new Exception("Unable to find/generate client position record");
            }


            if (orderLegRecord.BuySellType == OrderLegBuySellEnum.Buy)
            {
                if (clientPositionRecord.ClientPositions.TryGetValue(orderFilledDataRecord.Token1Id, out ClientPosition) == true)
                {
                    ClientPosition.TokenAmount += orderFilledDataRecord.Token1Amount;
                    ClientPosition.TokenValue = ClientPosition.TokenAmount * ClientPosition.TokenRate;
                }
                else
                {
                    ClientPosition = new ClientPositionData();

                    ClientPosition.TokenId = orderFilledDataRecord.Token1Id;
                    ClientPosition.TokenAmount = orderFilledDataRecord.Token1Amount;

                    ClientPosition.TokenRate = 0.0m;
                    ClientPosition.TokenRateIn = "USD";

                    if (TokenRates.TryGetValue(ClientPosition.TokenId, out TokenRate) == true)
                    {
                        ClientPosition.TokenRate = (decimal)TokenRate.AskRate;
                        ClientPosition.TokenRateIn = TokenRate.PriceCurrency;
                    }

                    ClientPosition.TokenValue = ClientPosition.TokenAmount * ClientPosition.TokenRate;
                    clientPositionRecord.ClientPositions.TryAdd(ClientPosition.TokenId, ClientPosition);
                }


                if (clientPositionRecord.ClientPositions.TryGetValue(orderFilledDataRecord.Token2Id, out ClientPosition) == true)
                {
                    ClientPosition.TokenAmount -= orderFilledDataRecord.Token2Amount;
                    ClientPosition.TokenValue = ClientPosition.TokenAmount * ClientPosition.TokenRate;
                }
            }
            else
            {
                if (clientPositionRecord.ClientPositions.TryGetValue(orderFilledDataRecord.Token2Id, out ClientPosition) == true)
                {
                    ClientPosition.TokenAmount += orderFilledDataRecord.Token2Amount;
                    ClientPosition.TokenValue = ClientPosition.TokenAmount * ClientPosition.TokenRate;
                }
                else
                {
                    ClientPosition = new ClientPositionData();
                    ClientPosition.TokenId = orderFilledDataRecord.Token2Id;
                    ClientPosition.TokenAmount = orderFilledDataRecord.Token2Amount;

                    ClientPosition.TokenRate = 0.0m;
                    ClientPosition.TokenRateIn = "USD";

                    if (TokenRates.TryGetValue(ClientPosition.TokenId, out TokenRate) == true)
                    {
                        ClientPosition.TokenRate = (decimal)TokenRate.AskRate;
                        ClientPosition.TokenRateIn = TokenRate.PriceCurrency;
                    }

                    ClientPosition.TokenValue = ClientPosition.TokenAmount * ClientPosition.TokenRate;
                    clientPositionRecord.ClientPositions.TryAdd(ClientPosition.TokenId, ClientPosition);
                }

                if (clientPositionRecord.ClientPositions.TryGetValue(orderFilledDataRecord.Token1Id, out ClientPosition) == true)
                {
                    ClientPosition.TokenAmount -= orderFilledDataRecord.Token1Amount;
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


                    // Add the processing of transfer requests

                    //Console.WriteLine("Processing Asset Transfer Requests");

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

            lock (BlockChainTransactionsLock)
            {
                blockChainRecord = new BlockChainData();

                blockChainRecord.Date = DateTime.Now.ToShortDateString();
                blockChainRecord.BlockNumber = blockCount++;
                blockChainRecord.OrderId = orderCount++;
                blockChainRecord.clientId = transferRequest.clientId;

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
        }

        private void addTransferToPosition(AssetTransferData transferRequest)
        {
            ClientPositionData ClientPosition = new ClientPositionData();
            TokenRateData TokenRate = new TokenRateData();
            ClientPosition clientPositionRecord;

            lock (ClientPositionLock)
            {

                if (ClientPositions.TryGetValue(transferRequest.clientId, out clientPositionRecord) == false)
                {
                    clientPositionRecord = new ClientPosition(transferRequest.clientId);
                    ClientPositions.TryAdd(transferRequest.clientId, clientPositionRecord);
                }

                if (clientPositionRecord == null)
                    throw new Exception("Unable to find/create client position record");

                if (transferRequest.transferType == TransferTypeEnum.Deposit)
                {
                    if (clientPositionRecord.ClientPositions.TryGetValue(transferRequest.tokenId, out ClientPosition) == true)
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
                            ClientPosition.TokenRate = (decimal)TokenRate.AskRate;
                            ClientPosition.TokenRateIn = TokenRate.PriceCurrency;
                        }

                        ClientPosition.TokenValue = ClientPosition.TokenAmount * ClientPosition.TokenRate;
                        clientPositionRecord.ClientPositions.TryAdd(ClientPosition.TokenId, ClientPosition);
                    }
                }
                else
                {
                    if (clientPositionRecord.ClientPositions.TryGetValue(transferRequest.tokenId, out ClientPosition) == true)
                    {
                        ClientPosition.TokenAmount -= transferRequest.tokenAmount;
                        ClientPosition.TokenValue = ClientPosition.TokenAmount * ClientPosition.TokenRate;
                    }
                }
            }
        }

        private void UpdateBlockChain(OrderLeg orderLegRecord, OrderFilledData orderFilledData)
        {
            BlockChainData blockChainRecord = new BlockChainData();
            KeyPairData KeyPair = new KeyPairData();

            //Console.WriteLine("Writing Order Flows to blockchain");

            lock (BlockChainTransactionsLock)
            {

                if (orderLegRecord.BuySellType == OrderLegBuySellEnum.Buy)
                {
                    blockChainRecord = new BlockChainData();

                    blockChainRecord.clientId = orderLegRecord.Order.ClientId;

                    blockChainRecord.Date = orderFilledData.FilledDateTime.ToShortDateString();
                    blockChainRecord.BlockNumber = blockCount++;
                    blockChainRecord.OrderId = orderLegRecord.OrderId;
                    blockChainRecord.PayReceive = "Receive";
                    blockChainRecord.TokenAmount = orderFilledData.Token1Amount;
                    blockChainRecord.TokenId = orderFilledData.Token1Id;

                    if (KeyPairs.TryGetValue(1, out KeyPair) == true)
                    {
                        blockChainRecord.Address = KeyPair.publicKey;
                    }
                    else
                    {
                        //blockChainRecord.Address = GenerateNewClientKey(orderLegRecord.Order.ClientId);
                    }

                    blockChainRecord.TransactionHash = HashOrder(blockChainRecord);
                    BlockChainTransactions.TryAdd(blockChainRecord.BlockNumber, blockChainRecord);


                    blockChainRecord = new BlockChainData();
                    blockChainRecord.clientId = orderLegRecord.Order.ClientId;
                    blockChainRecord.Date = DateTime.Now.ToShortDateString();
                    blockChainRecord.BlockNumber = blockCount++;
                    blockChainRecord.OrderId = orderFilledData.OrderLegId;
                    blockChainRecord.PayReceive = "Pay";
                    blockChainRecord.TokenAmount = orderFilledData.Token2Amount;
                    blockChainRecord.TokenId = orderFilledData.Token2Id;

                    if (KeyPairs.TryGetValue(0, out KeyPair) == true)
                    {
                        blockChainRecord.Address = KeyPair.publicKey;
                    }
                    else
                    {
                        //blockChainRecord.Address = GenerateNewClientKey(orderLegRecord.Order.ClientId);

                    }

                    blockChainRecord.TransactionHash = HashOrder(blockChainRecord);
                    BlockChainTransactions.TryAdd(blockChainRecord.BlockNumber, blockChainRecord);
                }
                else
                {
                    blockChainRecord = new BlockChainData();
                    blockChainRecord.Date = DateTime.Now.ToShortDateString();

                    blockChainRecord.clientId = orderLegRecord.Order.ClientId;
                    blockChainRecord.BlockNumber = blockCount++;
                    blockChainRecord.OrderId = orderLegRecord.OrderId;
                    blockChainRecord.PayReceive = "Pay";
                    blockChainRecord.TokenAmount = orderFilledData.Token1Amount;
                    blockChainRecord.TokenId = orderFilledData.Token1Id;

                    if (KeyPairs.TryGetValue(0, out KeyPair) == true)
                    {
                        blockChainRecord.Address = KeyPair.publicKey;
                    }
                    else
                    {

                    }

                    blockChainRecord.TransactionHash = HashOrder(blockChainRecord);
                    BlockChainTransactions.TryAdd(blockChainRecord.BlockNumber, blockChainRecord);

                    blockChainRecord = new BlockChainData();
                    blockChainRecord.Date = DateTime.Now.ToShortDateString();

                    blockChainRecord.clientId = orderLegRecord.Order.ClientId;
                    blockChainRecord.BlockNumber = blockCount++;
                    blockChainRecord.OrderId = orderLegRecord.OrderId;
                    blockChainRecord.PayReceive = "Receive";
                    blockChainRecord.TokenAmount = orderFilledData.Token2Amount;
                    blockChainRecord.TokenId = orderFilledData.Token2Id;
                    if (KeyPairs.TryGetValue(1, out KeyPair) == true)
                    {
                        blockChainRecord.Address = KeyPair.publicKey;
                    }
                    else
                    {

                    }

                    blockChainRecord.TransactionHash = HashOrder(blockChainRecord);
                    BlockChainTransactions.TryAdd(blockChainRecord.BlockNumber, blockChainRecord);
                }
            }
        }

        public int ActivateOrder(int OrderId)
        {
            return (0);
        }

        public decimal GetClientTokenBalance(int ClientId, string TokenId)
        {
            decimal tokenBalance = 0m;
            ClientPosition clientPositionsRecord;
            ClientPositionData clientPositionRecord;


            if (ClientPositions.TryGetValue(ClientId, out clientPositionsRecord) == true)
            {
                if (clientPositionsRecord.ClientPositions.TryGetValue(TokenId, out clientPositionRecord) == true)
                {
                    tokenBalance = clientPositionRecord.TokenAmount;
                }
            }

            return tokenBalance;
        }

        public List<BlockChainData> GetClientBlockChainTransactions(int clientId)
        {
            //Console.WriteLine("Client {0} Request for Blockchain Transactions", clientId);
            //Console.WriteLine("{0} Transactions Returned", BlockChainTransactions.Values.Where(o => o.clientId == clientId).Count());

            return BlockChainTransactions.Values.Where(o => o.clientId == clientId).ToList();
        }

        public List<ClientPositionData> GetClientPositions(int ClientId)
        {
            ClientPosition clientPositionRecord;

            if (ClientPositions.TryGetValue(ClientId, out clientPositionRecord) == true)
            {
                //Console.WriteLine("Client Id {0} /  {1} Positions Returned", ClientId, clientPositionRecord.ClientPositions.Count());
                return clientPositionRecord.ClientPositions.Values.ToList();
            }
            else
                return null;
        }

        public List<OrderFilledData> GetClientFilledOrders(int ClientId)
        {
            ClientFilledOrder clientFilledOrderRecord;

            if (ClientFilledOrders.TryGetValue(ClientId, out clientFilledOrderRecord) == true)
            {
                return clientFilledOrderRecord.ClientTransactions.Values.ToList();
            }
            else
                return null;
        }

        public List<OrderData> GetClientOrders(int ClientId)
        {
            //Console.WriteLine("Client Id {0} with {1} Orders Returned", ClientId, ClientOrders.Values.Where(o => o.ClientId == ClientId).Count());

            return ClientOrders.Values.Where(o => o.ClientId == ClientId).ToList();
        }

        public List<OrderData> GetClientHistoricalOrders(int ClientId)
        {

            //Console.WriteLine("Client Id {0} with {1} Historical Returned", ClientId, ClientHistoricalOrders.Values.Where(o => o.ClientId == ClientId).Count());

            return ClientHistoricalOrders.Values.Where(o => o.ClientId == ClientId).ToList();
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
            OrderLeg orderLegRecord = new OrderLeg();
            orderLegRecord.Order = new Order();



            orderCount++;
            orderData.OrderId = orderCount;

            Console.WriteLine("Add Order for Client {0} Details {1} ", orderData.ClientId, Newtonsoft.Json.JsonConvert.SerializeObject(orderData));

            orderLegRecord.Order.ClientId = orderData.ClientId;

            // The OrderId and OrderLegId will be created by the database
            orderLegRecord.OrderId = orderData.OrderId;
            orderLegRecord.OrderLegId = orderData.OrderId;


            orderLegRecord.OrderLegCreatedDateTime = DateTime.Now;
            orderLegRecord.BuySellType = orderData.BuySellType;
            orderLegRecord.OrderLegFillStatus = OrderLegFillStatusEnum.None;
            orderLegRecord.OrderLegStatus = OrderLegStatusEnum.Active;
            orderLegRecord.OrderPriceTerms = orderData.OrderPriceTerms;
            orderLegRecord.OrderLegType = orderData.OrderType == OrderTypeEnum.Market ? OrderLegTypeEnum.Market : OrderLegTypeEnum.Limit;
            orderLegRecord.Token1AccountId = 0;
            orderLegRecord.Token2AccountId = 0;
            orderLegRecord.Token1Id = orderData.Token1Id;
            orderLegRecord.Token2Id = orderData.Token2Id;
            orderLegRecord.Token1Amount = orderData.Token1Amount;
            orderLegRecord.Token2Amount = orderData.Token2Amount;
            orderLegRecord.OrderPrice = orderData.OrderPrice;
            orderLegRecord.OrderPriceTerms = orderData.OrderPriceTerms;

            orderLegRecord.Order.ClientId = orderData.ClientId;
            orderLegRecord.Order.OrderId = orderData.OrderId;
            orderLegRecord.Order.OrderStatus = OrderStatusEnum.Active;
            orderLegRecord.Order.OrderType = orderData.OrderType;
            orderLegRecord.Order.OrderPriceTerms = orderData.OrderPriceTerms;

            // Needs to support expiration types like EOD, Good Till
            orderLegRecord.Order.OrderExpirationType = OrderExpirationTypeEnum.GoodTillCancel;


            lock (OrderListLock)
            {
                ClientOrders.TryAdd(orderData.OrderId, orderData);
                _exchangeBook.AddOrderToExchange(orderLegRecord);
            }

            return (orderData);
        }

        public int CancelOrder(int OrderId)
        {
            throw new Exception("Not Implemented");
        }

        public int SuspendOrder(int OrderID)
        {
            throw new Exception("Not Implemented");
        }

        public string GetNewGuid()
        {
            return Guid.NewGuid().ToString();
        }

        public AssetDepositData AddDeposit(AssetDepositData depositNotification)
        {
            //depositNotification.referenceId = Guid.NewGuid().ToString();

            lock (DepositNotificationLock)
            {
                DepositNotifications.TryAdd(depositNotification.referenceId, depositNotification);

                Console.WriteLine("Asset Deposit Notification {0} Added", depositNotification.referenceId);

                AssetTransferData newTransfer = new AssetTransferData();

                newTransfer.clientId = depositNotification.clientId;

                newTransfer.referenceId = depositNotification.referenceId;
                newTransfer.assetId = depositNotification.assetId;
                newTransfer.assetAmount = depositNotification.amount;

                newTransfer.transferStatus = TransferStatusEnum.InProgress;
                newTransfer.transferType = TransferTypeEnum.Deposit;

                newTransfer.tokenId = '@' + depositNotification.assetId;
                newTransfer.tokenAmount = depositNotification.amount;

                switch (newTransfer.clientId)
                {
                    case 1:
                        newTransfer.forAccountOf = "TradezDigital";
                        break;
                    case 4:
                        newTransfer.forAccountOf = "VinceSmall";
                        break;
                    case 5:
                        newTransfer.forAccountOf = "MarkVanRoon";
                        break;
                    default:
                        newTransfer.forAccountOf = String.Format("Abacas Exchange Client {0}", newTransfer.clientId);
                        break;
                }

                lock (AssetTransferLock)
                {
                    AssetTransfers.TryAdd(newTransfer.referenceId, newTransfer);
                    Console.WriteLine("Asset Deposit Transfer Added {0}", newTransfer.referenceId);
                }
            }

            return depositNotification;
        }

        public AssetWithdrawalData AddWithdrawal(AssetWithdrawalData withdrawalRequest)
        {
            lock (WithdrawalRequestLock)
            {
                //withdrawalRequest.referenceId = Guid.NewGuid().ToString();
                WithdrawalRequests.TryAdd(withdrawalRequest.referenceId, withdrawalRequest);

                Console.WriteLine("Asset Withdrawal Notification {0} Added", withdrawalRequest.referenceId);

                AssetTransferData newTransfer = new AssetTransferData();

                newTransfer.clientId = withdrawalRequest.clientId;

                newTransfer.referenceId = withdrawalRequest.referenceId;
                newTransfer.assetId = withdrawalRequest.tokenId.Substring(1);
                newTransfer.assetAmount = withdrawalRequest.amount;

                newTransfer.transferStatus = TransferStatusEnum.InProgress;
                newTransfer.transferType = TransferTypeEnum.Withdrawal;

                newTransfer.tokenId = withdrawalRequest.tokenId;
                newTransfer.tokenAmount = withdrawalRequest.amount;

                switch (newTransfer.clientId)
                {
                    case 1:
                        newTransfer.forAccountOf = "TradezDigital";
                        break;
                    case 4:
                        newTransfer.forAccountOf = "VinceSmall";
                        break;
                    case 5:
                        newTransfer.forAccountOf = "MarkVanRoon";
                        break;
                    default:
                        newTransfer.forAccountOf = String.Format("Abacas Exchange Client {0}", newTransfer.clientId);
                        break;
                }

                lock (AssetTransferLock)
                {
                    AssetTransfers.TryAdd(newTransfer.referenceId, newTransfer);
                    Console.WriteLine("Asset Withdrawal Transfer Added {0}", newTransfer.referenceId);
                }
            }

            return withdrawalRequest;
        }

        public List<AssetTransferData> GetClientTransferActivity(int ClientId)
        {
            return AssetTransfers.Values.Where(o => o.clientId == ClientId).ToList();
        }

        public void NotifyOrderLegAdded(OrderLeg orderLegRecord)
        {
            OrderData orderData = new OrderData();

            orderLegRecord.CopyPropertiesTo(orderData);

            // The OrderId and OrderLegId will be created by the database
            orderData.OrderId = orderLegRecord.OrderId;
            orderData.OrderLegId = orderLegRecord.OrderLegId;
            orderData.BuySellType = orderLegRecord.BuySellType;
            orderData.OrderStatus = OrderStatusEnum.Active;
            orderData.OrderFillStatus = OrderLegFillStatusEnum.None;
            orderData.OrderPrice = orderLegRecord.OrderPrice;
            orderData.OrderPriceTerms = orderLegRecord.OrderPriceTerms;
            orderData.OrderType = (orderLegRecord.OrderLegType == OrderLegTypeEnum.Market ? OrderTypeEnum.Market : OrderTypeEnum.Limit);

            orderData.Token1Id = orderLegRecord.Token1Id;
            orderData.Token1Amount = orderLegRecord.Token1Amount;
            orderData.Token1AmountFilled = orderLegRecord.Token1AmountFilled;
            orderData.Token2Id = orderLegRecord.Token2Id;
            orderData.Token2Amount = orderLegRecord.Token2Amount;
            orderData.ClientAccountId = 0;
            orderData.ClientId = orderLegRecord.Order.ClientId;

            lock (ClientOrderLock)
            {
                ClientOrders.TryAdd(orderData.OrderLegId, orderData);
            }

            Console.WriteLine("Order Added: Order Id {0}, Client {1} Token Pair {2}-{3} type {4} at Price {5}", orderLegRecord.OrderLegId, orderLegRecord.Order.ClientId, orderLegRecord.Token1Id, orderLegRecord.Token2Id, orderLegRecord.OrderLegType.ToString(), orderLegRecord.OrderPrice);
        }

        public void NotifyOrderLegMatched(OrderLeg orderLegRecord, OrderFilledData orderFilledDataRecord)
        {
            orderFilledDataRecord.TransactionId = orderFilledCount++;
            OrderData orderData;
            ClientFilledOrder clientFilledOrderRecord;


            Console.WriteLine("Order Matched:  Order Id {0} , Client {1}  Transaction Id {2} Token Pair {3}-{4} type {5} Amount filled {6} at Price {7}",
                orderLegRecord.OrderLegId, orderLegRecord.Order.ClientId, orderFilledDataRecord.TransactionId,
                orderLegRecord.Token1Id, orderLegRecord.Token2Id, orderLegRecord.OrderLegType.ToString(),
                orderFilledDataRecord.Token1Amount, orderFilledDataRecord.PriceFilled);

            // Update Position
            addFilledOrderToPosition(orderLegRecord, orderFilledDataRecord);

            lock (ClientOrderLock)
            {
                // This should always be true
                if (ClientOrders.TryGetValue(orderLegRecord.OrderLegId, out orderData) == true)
                {
                    orderData.OrderFillStatus = OrderLegFillStatusEnum.Partial;
                    orderData.Token1AmountFilled += orderFilledDataRecord.Token1Amount;
                    UpdateBlockChain(orderLegRecord, orderFilledDataRecord);
                }

                lock (ClientFilledOrderLock)
                {
                    if (ClientFilledOrders.TryGetValue(orderLegRecord.Order.ClientId, out clientFilledOrderRecord) == false)
                    {
                        clientFilledOrderRecord = new ClientFilledOrder(orderLegRecord.Order.ClientId);
                        clientFilledOrderRecord.ClientTransactions.TryAdd(orderFilledDataRecord.TransactionId, orderFilledDataRecord);

                        ClientFilledOrders.TryAdd(orderLegRecord.Order.ClientId, clientFilledOrderRecord);
                    }
                    else
                    {
                        clientFilledOrderRecord.ClientTransactions.TryAdd(orderFilledDataRecord.TransactionId, orderFilledDataRecord);
                    }
                }
            }
        }

        public void NotifyOrderLegFilled(OrderLeg orderLegRecord)
        {
            OrderData orderData;

            lock (OrderListLock)
            {
                if (ClientOrders.TryGetValue(orderLegRecord.OrderLegId, out orderData) == true)
                {
                    orderData.OrderFillStatus = OrderLegFillStatusEnum.Full;
                    orderData.OrderStatus = OrderStatusEnum.Filled;

                    lock (ClientHistoricalOrderLock)
                    {
                        ClientHistoricalOrders.TryAdd(orderLegRecord.OrderLegId, orderData);
                    }

                    ClientOrders.TryRemove(orderLegRecord.OrderLegId, out orderData);
                }
            }

            Console.WriteLine("Order Filled: Order Id {0}, Client {1} Status Change to Filled",
                orderLegRecord.OrderLegId, orderLegRecord.Order.ClientId);
        }
    }
}
