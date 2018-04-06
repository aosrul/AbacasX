namespace AbacasXModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Asset",
                c => new
                    {
                        AssetCode = c.String(nullable: false, maxLength: 35),
                        AssetName = c.String(nullable: false, maxLength: 35),
                        AssetType = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.AssetCode);
            
            CreateTable(
                "dbo.AssetAccount",
                c => new
                    {
                        AssetAccountId = c.Int(nullable: false, identity: true),
                        CustodianId = c.Int(nullable: false),
                        AccountNumber = c.String(nullable: false, maxLength: 40),
                        AssetCode = c.String(nullable: false, maxLength: 35),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 6),
                        AvailableBalance = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.AssetAccountId)
                .ForeignKey("dbo.Asset", t => t.AssetCode, cascadeDelete: true)
                .ForeignKey("dbo.Custodian", t => t.CustodianId, cascadeDelete: true)
                .Index(t => t.CustodianId)
                .Index(t => t.AssetCode);
            
            CreateTable(
                "dbo.Custodian",
                c => new
                    {
                        CustodianId = c.Int(nullable: false, identity: true),
                        CustodianName = c.String(nullable: false, maxLength: 40),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.CustodianId);
            
            CreateTable(
                "dbo.AssetRate",
                c => new
                    {
                        RateId = c.Int(nullable: false, identity: true),
                        AssetCode = c.String(nullable: false, maxLength: 35),
                        PriceCurrency = c.String(nullable: false, maxLength: 10),
                        RateProviderId = c.Int(nullable: false),
                        RateProviderCode = c.String(maxLength: 30),
                        RateTerms = c.Int(nullable: false),
                        BidRate = c.Double(nullable: false),
                        AskRate = c.Double(nullable: false),
                        HighRate = c.Double(nullable: false),
                        LowRate = c.Double(nullable: false),
                        OpenRate = c.Double(nullable: false),
                        CloseRate = c.Double(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.RateId)
                .ForeignKey("dbo.Asset", t => t.AssetCode, cascadeDelete: true)
                .ForeignKey("dbo.AssetRateProvider", t => t.RateProviderId, cascadeDelete: true)
                .Index(t => t.AssetCode)
                .Index(t => t.RateProviderId);
            
            CreateTable(
                "dbo.AssetRateProvider",
                c => new
                    {
                        RateProviderId = c.Int(nullable: false, identity: true),
                        ProviderName = c.String(nullable: false, maxLength: 40),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.RateProviderId);
            
            CreateTable(
                "dbo.AssetTransferTokenFlow",
                c => new
                    {
                        AssetTokenFlowId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        TokenId = c.String(maxLength: 35),
                        AssetTransferId = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        AssetTransfer_TransferId = c.Int(),
                        TokenAccount_TokenAccountId = c.Int(),
                    })
                .PrimaryKey(t => t.AssetTokenFlowId)
                .ForeignKey("dbo.AssetTransfer", t => t.AssetTransfer_TransferId)
                .ForeignKey("dbo.Token", t => t.TokenId)
                .ForeignKey("dbo.TokenAccount", t => t.TokenAccount_TokenAccountId)
                .Index(t => t.TokenId)
                .Index(t => t.AssetTransfer_TransferId)
                .Index(t => t.TokenAccount_TokenAccountId);
            
            CreateTable(
                "dbo.AssetTransfer",
                c => new
                    {
                        TransferId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        TransferType = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        TransferStatus = c.Int(nullable: false),
                        ForAccountOf = c.String(maxLength: 75),
                        ReferenceCode = c.String(maxLength: 50),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        AssetAccount_AssetAccountId = c.Int(),
                    })
                .PrimaryKey(t => t.TransferId)
                .ForeignKey("dbo.AssetAccount", t => t.AssetAccount_AssetAccountId)
                .Index(t => t.AssetAccount_AssetAccountId);
            
            CreateTable(
                "dbo.Token",
                c => new
                    {
                        TokenId = c.String(nullable: false, maxLength: 35),
                        Name = c.String(nullable: false, maxLength: 35),
                        Denomination = c.String(nullable: false, maxLength: 35),
                        Multiplier = c.Int(nullable: false),
                        PriceTerms = c.Int(nullable: false),
                        TokenStatus = c.Int(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 6),
                        AvailableBalance = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        BaseAsset_AssetCode = c.String(maxLength: 35),
                    })
                .PrimaryKey(t => t.TokenId)
                .ForeignKey("dbo.Asset", t => t.BaseAsset_AssetCode)
                .Index(t => t.BaseAsset_AssetCode);
            
            CreateTable(
                "dbo.TokenAccount",
                c => new
                    {
                        TokenAccountId = c.Int(nullable: false, identity: true),
                        AccountName = c.String(nullable: false, maxLength: 40),
                        AccountNumber = c.String(nullable: false, maxLength: 30),
                        AccountStatus = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.TokenAccountId);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        UserType = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        EncryptedPassword = c.String(nullable: false, maxLength: 15),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.ClientAuthentication",
                c => new
                    {
                        ClientId = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        User_ClientId = c.Int(),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("dbo.Client", t => t.User_ClientId)
                .Index(t => t.User_ClientId);
            
            CreateTable(
                "dbo.ClientKYC",
                c => new
                    {
                        ClientId = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Client_ClientId = c.Int(),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("dbo.Client", t => t.Client_ClientId)
                .Index(t => t.Client_ClientId);
            
            CreateTable(
                "dbo.ClientLogin",
                c => new
                    {
                        ClientId = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Client_ClientId = c.Int(),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("dbo.Client", t => t.Client_ClientId)
                .Index(t => t.Client_ClientId);
            
            CreateTable(
                "dbo.ClientRegistration",
                c => new
                    {
                        ClientId = c.Int(nullable: false),
                        RegistrationStatus = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Client_ClientId = c.Int(),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("dbo.Client", t => t.Client_ClientId)
                .Index(t => t.Client_ClientId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        TokenAccountId = c.Int(nullable: false),
                        ChildOrderID = c.Int(),
                        ClientId = c.Int(nullable: false),
                        OrderType = c.Int(nullable: false),
                        OrderFillType = c.Int(nullable: false),
                        OrderStatus = c.Int(nullable: false),
                        OrderAllocationType = c.Int(nullable: false),
                        OrderFillStatus = c.Int(nullable: false),
                        OrderExpirationType = c.Int(nullable: false),
                        OrderPriceTerms = c.Int(nullable: false),
                        OrderExpirationDateTime = c.DateTime(nullable: false),
                        OrderCreatedDateTime = c.DateTime(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.OrderFilled",
                c => new
                    {
                        OrderLegID = c.Int(nullable: false),
                        TransactionId = c.Int(nullable: false),
                        FilledDateTime = c.DateTime(nullable: false),
                        Token1Id = c.String(nullable: false),
                        Token1Amount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Token2Id = c.String(nullable: false),
                        Token2Amount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        PriceFilled = c.Decimal(nullable: false, precision: 18, scale: 6),
                        OrderPriceTerms = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => new { t.OrderLegID, t.TransactionId })
                .ForeignKey("dbo.OrderLeg", t => t.OrderLegID, cascadeDelete: true)
                .Index(t => t.OrderLegID);
            
            CreateTable(
                "dbo.OrderLeg",
                c => new
                    {
                        OrderLegID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        OrderLegCreatedDateTime = c.DateTime(nullable: false),
                        OrderLegType = c.Int(nullable: false),
                        OrderLegStatus = c.Int(nullable: false),
                        BuySellType = c.Int(nullable: false),
                        OrderLegFillStatus = c.Int(nullable: false),
                        Token1Id = c.String(nullable: false, maxLength: 35),
                        Token1AccountId = c.Int(nullable: false),
                        Token1Amount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Token1AmountFilled = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Token2Id = c.String(nullable: false, maxLength: 35),
                        Token2Amount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Token2AccountId = c.Int(nullable: false),
                        OrderPrice = c.Decimal(nullable: false, precision: 18, scale: 6),
                        OrderPriceTerms = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.OrderLegID)
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.OrderFilledMatch",
                c => new
                    {
                        TransactionId = c.Int(nullable: false),
                        OffsetTransactionId = c.Int(nullable: false),
                        Token1Id = c.String(),
                        Token1Amount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Token2Id = c.String(),
                        Token2Amount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        PriceFilled = c.Decimal(nullable: false, precision: 18, scale: 6),
                        OrderPriceTerms = c.Int(nullable: false),
                        MatchedDateTime = c.DateTime(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => new { t.TransactionId, t.OffsetTransactionId });
            
            CreateTable(
                "dbo.TokenAccountHolder",
                c => new
                    {
                        ClientId = c.Int(nullable: false),
                        TokenAccountId = c.Int(nullable: false),
                        AccountHolderType = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => new { t.ClientId, t.TokenAccountId })
                .ForeignKey("dbo.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.TokenAccount", t => t.TokenAccountId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.TokenAccountId);
            
            CreateTable(
                "dbo.TransactionTokenFlow",
                c => new
                    {
                        TradeTokenFlowId = c.Int(nullable: false, identity: true),
                        TransactionId = c.Int(nullable: false),
                        TokenId = c.String(),
                        TokenFlowType = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        AmountOffset = c.Decimal(nullable: false, precision: 18, scale: 6),
                        TokenAccountId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        FlowDateTime = c.DateTime(nullable: false),
                        SettlementDateTime = c.DateTime(nullable: false),
                        TokenFlowProcessingStatus = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.TradeTokenFlowId)
                .ForeignKey("dbo.Transaction", t => t.TransactionId, cascadeDelete: true)
                .Index(t => t.TransactionId);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        OrderLegId = c.Int(nullable: false),
                        TransactionSource = c.Int(nullable: false),
                        ExternalReferenceId = c.String(maxLength: 35),
                        BuySellType = c.Int(nullable: false),
                        Token1ID = c.String(maxLength: 35),
                        Token1Amount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Token2ID = c.String(maxLength: 35),
                        Token2Amount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        PriceTerms = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 6),
                        TransactionDate = c.DateTime(nullable: false),
                        TransactionFee = c.Decimal(nullable: false, precision: 18, scale: 6),
                        TransactionStatus = c.Int(nullable: false),
                        ProcessingStatus = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.TransactionId);
            
            CreateTable(
                "dbo.Trust",
                c => new
                    {
                        TrustId = c.Int(nullable: false, identity: true),
                        TrustName = c.String(nullable: false, maxLength: 40),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.TrustId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionTokenFlow", "TransactionId", "dbo.Transaction");
            DropForeignKey("dbo.TokenAccountHolder", "TokenAccountId", "dbo.TokenAccount");
            DropForeignKey("dbo.TokenAccountHolder", "ClientId", "dbo.Client");
            DropForeignKey("dbo.OrderFilled", "OrderLegID", "dbo.OrderLeg");
            DropForeignKey("dbo.OrderLeg", "OrderID", "dbo.Order");
            DropForeignKey("dbo.ClientRegistration", "Client_ClientId", "dbo.Client");
            DropForeignKey("dbo.ClientLogin", "Client_ClientId", "dbo.Client");
            DropForeignKey("dbo.ClientKYC", "Client_ClientId", "dbo.Client");
            DropForeignKey("dbo.ClientAuthentication", "User_ClientId", "dbo.Client");
            DropForeignKey("dbo.AssetTransferTokenFlow", "TokenAccount_TokenAccountId", "dbo.TokenAccount");
            DropForeignKey("dbo.AssetTransferTokenFlow", "TokenId", "dbo.Token");
            DropForeignKey("dbo.Token", "BaseAsset_AssetCode", "dbo.Asset");
            DropForeignKey("dbo.AssetTransferTokenFlow", "AssetTransfer_TransferId", "dbo.AssetTransfer");
            DropForeignKey("dbo.AssetTransfer", "AssetAccount_AssetAccountId", "dbo.AssetAccount");
            DropForeignKey("dbo.AssetRate", "RateProviderId", "dbo.AssetRateProvider");
            DropForeignKey("dbo.AssetRate", "AssetCode", "dbo.Asset");
            DropForeignKey("dbo.AssetAccount", "CustodianId", "dbo.Custodian");
            DropForeignKey("dbo.AssetAccount", "AssetCode", "dbo.Asset");
            DropIndex("dbo.TransactionTokenFlow", new[] { "TransactionId" });
            DropIndex("dbo.TokenAccountHolder", new[] { "TokenAccountId" });
            DropIndex("dbo.TokenAccountHolder", new[] { "ClientId" });
            DropIndex("dbo.OrderLeg", new[] { "OrderID" });
            DropIndex("dbo.OrderFilled", new[] { "OrderLegID" });
            DropIndex("dbo.ClientRegistration", new[] { "Client_ClientId" });
            DropIndex("dbo.ClientLogin", new[] { "Client_ClientId" });
            DropIndex("dbo.ClientKYC", new[] { "Client_ClientId" });
            DropIndex("dbo.ClientAuthentication", new[] { "User_ClientId" });
            DropIndex("dbo.Token", new[] { "BaseAsset_AssetCode" });
            DropIndex("dbo.AssetTransfer", new[] { "AssetAccount_AssetAccountId" });
            DropIndex("dbo.AssetTransferTokenFlow", new[] { "TokenAccount_TokenAccountId" });
            DropIndex("dbo.AssetTransferTokenFlow", new[] { "AssetTransfer_TransferId" });
            DropIndex("dbo.AssetTransferTokenFlow", new[] { "TokenId" });
            DropIndex("dbo.AssetRate", new[] { "RateProviderId" });
            DropIndex("dbo.AssetRate", new[] { "AssetCode" });
            DropIndex("dbo.AssetAccount", new[] { "AssetCode" });
            DropIndex("dbo.AssetAccount", new[] { "CustodianId" });
            DropTable("dbo.Trust");
            DropTable("dbo.Transaction");
            DropTable("dbo.TransactionTokenFlow");
            DropTable("dbo.TokenAccountHolder");
            DropTable("dbo.OrderFilledMatch");
            DropTable("dbo.OrderLeg");
            DropTable("dbo.OrderFilled");
            DropTable("dbo.Order");
            DropTable("dbo.ClientRegistration");
            DropTable("dbo.ClientLogin");
            DropTable("dbo.ClientKYC");
            DropTable("dbo.ClientAuthentication");
            DropTable("dbo.Client");
            DropTable("dbo.TokenAccount");
            DropTable("dbo.Token");
            DropTable("dbo.AssetTransfer");
            DropTable("dbo.AssetTransferTokenFlow");
            DropTable("dbo.AssetRateProvider");
            DropTable("dbo.AssetRate");
            DropTable("dbo.Custodian");
            DropTable("dbo.AssetAccount");
            DropTable("dbo.Asset");
        }
    }
}
