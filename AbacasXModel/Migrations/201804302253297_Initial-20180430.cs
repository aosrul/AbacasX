namespace AbacasXModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial20180430 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Asset",
                c => new
                    {
                        AssetId = c.String(nullable: false, maxLength: 35),
                        AssetName = c.String(nullable: false, maxLength: 50),
                        AssetType = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.AssetId);
            
            CreateTable(
                "dbo.AssetAccount",
                c => new
                    {
                        AssetAccountId = c.Int(nullable: false, identity: true),
                        AssetId = c.String(maxLength: 35),
                        CustodianId = c.Int(nullable: false),
                        AccountNumber = c.String(nullable: false, maxLength: 40),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 6),
                        AvailableBalance = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.AssetAccountId)
                .ForeignKey("dbo.Asset", t => t.AssetId)
                .ForeignKey("dbo.Custodian", t => t.CustodianId, cascadeDelete: true)
                .Index(t => t.AssetId)
                .Index(t => t.CustodianId);
            
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
                        AssetId = c.String(nullable: false, maxLength: 35),
                        RateProviderId = c.Int(nullable: false),
                        PriceCurrency = c.String(nullable: false, maxLength: 10),
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
                .ForeignKey("dbo.Asset", t => t.AssetId, cascadeDelete: true)
                .ForeignKey("dbo.AssetRateProvider", t => t.RateProviderId, cascadeDelete: true)
                .Index(t => t.AssetId)
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
                "dbo.AssetTransfer",
                c => new
                    {
                        AssetTransferId = c.Int(nullable: false, identity: true),
                        AssetAccountId = c.Int(nullable: false),
                        CustodianId = c.Int(nullable: false),
                        TokenConversionId = c.Int(),
                        AssetId = c.String(nullable: false, maxLength: 35),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        TransferStatus = c.Int(nullable: false),
                        TransferType = c.Int(nullable: false),
                        ForAccountOf = c.String(nullable: false, maxLength: 75),
                        ReferenceCode = c.String(nullable: false, maxLength: 50),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.AssetTransferId)
                .ForeignKey("dbo.AssetAccount", t => t.AssetAccountId, cascadeDelete: true)
                .Index(t => t.AssetAccountId);
            
            CreateTable(
                "dbo.BlockChainTokenFlow",
                c => new
                    {
                        TokenFlowId = c.Int(nullable: false, identity: true),
                        BlockChainId = c.String(),
                        BlockNumber = c.Int(nullable: false),
                        BlockChainStatus = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        TokenFlow_TokenFlowId = c.Int(),
                    })
                .PrimaryKey(t => t.TokenFlowId)
                .ForeignKey("dbo.TokenFlow", t => t.TokenFlow_TokenFlowId)
                .Index(t => t.TokenFlow_TokenFlowId);
            
            CreateTable(
                "dbo.TokenFlow",
                c => new
                    {
                        TokenFlowId = c.Int(nullable: false, identity: true),
                        TradeId = c.Int(nullable: false),
                        TokenId = c.String(),
                        TokenFlowType = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        TokenAccountId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        FlowDateTime = c.DateTime(nullable: false),
                        SettlementDateTime = c.DateTime(nullable: false),
                        TokenFlowProcessingStatus = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.TokenFlowId)
                .ForeignKey("dbo.TokenAccount", t => t.TokenAccountId, cascadeDelete: true)
                .ForeignKey("dbo.TokenTrade", t => t.TradeId, cascadeDelete: true)
                .Index(t => t.TradeId)
                .Index(t => t.TokenAccountId);
            
            CreateTable(
                "dbo.TokenAccount",
                c => new
                    {
                        TokenAccountId = c.Int(nullable: false, identity: true),
                        ClientAccountId = c.Int(nullable: false),
                        TokenId = c.String(nullable: false, maxLength: 35),
                        AccountName = c.String(nullable: false, maxLength: 40),
                        AccountNumber = c.String(nullable: false, maxLength: 30),
                        AccountStatus = c.Int(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 6),
                        AvailableBalance = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.TokenAccountId)
                .ForeignKey("dbo.ClientAccount", t => t.ClientAccountId, cascadeDelete: true)
                .ForeignKey("dbo.Token", t => t.TokenId)
                .Index(t => t.ClientAccountId)
                .Index(t => t.TokenId);
            
            CreateTable(
                "dbo.ClientAccount",
                c => new
                    {
                        ClientAccountId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        AccountName = c.String(nullable: false, maxLength: 40),
                        AccountNumber = c.String(nullable: false, maxLength: 30),
                        AccountStatus = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ClientAccountId)
                .ForeignKey("dbo.Client", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        ClientType = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        EncryptedPassword = c.String(nullable: false, maxLength: 15),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.Token",
                c => new
                    {
                        TokenId = c.String(nullable: false, maxLength: 35),
                        AssetId = c.Int(nullable: false),
                        AssetAccountId = c.Int(nullable: false),
                        CustodianId = c.Int(nullable: false),
                        TrustId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 35),
                        Denomination = c.String(nullable: false, maxLength: 35),
                        Multiplier = c.Int(nullable: false),
                        PriceTerms = c.Int(nullable: false),
                        TokenStatus = c.Int(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 6),
                        AvailableBalance = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Asset_AssetId = c.String(maxLength: 35),
                    })
                .PrimaryKey(t => t.TokenId)
                .ForeignKey("dbo.Asset", t => t.Asset_AssetId)
                .ForeignKey("dbo.AssetAccount", t => t.AssetAccountId, cascadeDelete: true)
                .ForeignKey("dbo.Custodian", t => t.CustodianId)
                .ForeignKey("dbo.Trust", t => t.TrustId, cascadeDelete: true)
                .Index(t => t.AssetAccountId)
                .Index(t => t.CustodianId)
                .Index(t => t.TrustId)
                .Index(t => t.Asset_AssetId);
            
            CreateTable(
                "dbo.Trust",
                c => new
                    {
                        TrustId = c.Int(nullable: false, identity: true),
                        TrustName = c.String(nullable: false, maxLength: 40),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.TrustId);
            
            CreateTable(
                "dbo.TokenTrade",
                c => new
                    {
                        TradeId = c.Int(nullable: false, identity: true),
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
                .PrimaryKey(t => t.TradeId);
            
            CreateTable(
                "dbo.ClientAccountHolder",
                c => new
                    {
                        ClientId = c.Int(nullable: false),
                        ClientAccountId = c.Int(nullable: false),
                        AccountHolderType = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => new { t.ClientId, t.ClientAccountId })
                .ForeignKey("dbo.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.ClientAccount", t => t.ClientAccountId)
                .Index(t => t.ClientId)
                .Index(t => t.ClientAccountId);
            
            CreateTable(
                "dbo.ClientAuthentication",
                c => new
                    {
                        ClientAuthenticationId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        AuthenticationType = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ClientAuthenticationId)
                .ForeignKey("dbo.Client", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.ClientKYC",
                c => new
                    {
                        ClientRegistrationId = c.Int(nullable: false, identity: true),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ClientRegistration_ClientRegistrationId = c.Int(),
                    })
                .PrimaryKey(t => t.ClientRegistrationId)
                .ForeignKey("dbo.ClientRegistration", t => t.ClientRegistration_ClientRegistrationId)
                .Index(t => t.ClientRegistration_ClientRegistrationId);
            
            CreateTable(
                "dbo.ClientRegistration",
                c => new
                    {
                        ClientRegistrationId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        RegistrationStatus = c.Int(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ClientRegistrationId)
                .ForeignKey("dbo.Client", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
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
                "dbo.TokenConversion",
                c => new
                    {
                        TokenConversionId = c.Int(nullable: false, identity: true),
                        AssetTransferId = c.Int(nullable: false),
                        TokenId = c.String(maxLength: 35),
                        TokenAccountId = c.Int(nullable: false),
                        TokenAmount = c.Decimal(nullable: false, precision: 18, scale: 6),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.TokenConversionId)
                .ForeignKey("dbo.AssetTransfer", t => t.AssetTransferId, cascadeDelete: true)
                .ForeignKey("dbo.TokenAccount", t => t.TokenAccountId, cascadeDelete: true)
                .Index(t => t.AssetTransferId)
                .Index(t => t.TokenAccountId);
            
            CreateTable(
                "dbo.TrustAccount",
                c => new
                    {
                        TokenId = c.String(nullable: false, maxLength: 35),
                        AssetAccountId = c.Int(nullable: false),
                        TrustId = c.Int(nullable: false),
                        ReconciliationStatus = c.Int(nullable: false),
                        LastReconciliationDateTime = c.DateTime(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => new { t.TokenId, t.AssetAccountId, t.TrustId })
                .ForeignKey("dbo.AssetAccount", t => t.AssetAccountId)
                .ForeignKey("dbo.Token", t => t.TokenId)
                .ForeignKey("dbo.Trust", t => t.TrustId)
                .Index(t => t.TokenId)
                .Index(t => t.AssetAccountId)
                .Index(t => t.TrustId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrustAccount", "TrustId", "dbo.Trust");
            DropForeignKey("dbo.TrustAccount", "TokenId", "dbo.Token");
            DropForeignKey("dbo.TrustAccount", "AssetAccountId", "dbo.AssetAccount");
            DropForeignKey("dbo.TokenConversion", "TokenAccountId", "dbo.TokenAccount");
            DropForeignKey("dbo.TokenConversion", "AssetTransferId", "dbo.AssetTransfer");
            DropForeignKey("dbo.OrderFilled", "OrderLegID", "dbo.OrderLeg");
            DropForeignKey("dbo.OrderLeg", "OrderID", "dbo.Order");
            DropForeignKey("dbo.ClientLogin", "Client_ClientId", "dbo.Client");
            DropForeignKey("dbo.ClientKYC", "ClientRegistration_ClientRegistrationId", "dbo.ClientRegistration");
            DropForeignKey("dbo.ClientRegistration", "ClientId", "dbo.Client");
            DropForeignKey("dbo.ClientAuthentication", "ClientId", "dbo.Client");
            DropForeignKey("dbo.ClientAccountHolder", "ClientAccountId", "dbo.ClientAccount");
            DropForeignKey("dbo.ClientAccountHolder", "ClientId", "dbo.Client");
            DropForeignKey("dbo.BlockChainTokenFlow", "TokenFlow_TokenFlowId", "dbo.TokenFlow");
            DropForeignKey("dbo.TokenFlow", "TradeId", "dbo.TokenTrade");
            DropForeignKey("dbo.TokenFlow", "TokenAccountId", "dbo.TokenAccount");
            DropForeignKey("dbo.TokenAccount", "TokenId", "dbo.Token");
            DropForeignKey("dbo.Token", "TrustId", "dbo.Trust");
            DropForeignKey("dbo.Token", "CustodianId", "dbo.Custodian");
            DropForeignKey("dbo.Token", "AssetAccountId", "dbo.AssetAccount");
            DropForeignKey("dbo.Token", "Asset_AssetId", "dbo.Asset");
            DropForeignKey("dbo.TokenAccount", "ClientAccountId", "dbo.ClientAccount");
            DropForeignKey("dbo.ClientAccount", "ClientId", "dbo.Client");
            DropForeignKey("dbo.AssetTransfer", "AssetAccountId", "dbo.AssetAccount");
            DropForeignKey("dbo.AssetRate", "RateProviderId", "dbo.AssetRateProvider");
            DropForeignKey("dbo.AssetRate", "AssetId", "dbo.Asset");
            DropForeignKey("dbo.AssetAccount", "CustodianId", "dbo.Custodian");
            DropForeignKey("dbo.AssetAccount", "AssetId", "dbo.Asset");
            DropIndex("dbo.TrustAccount", new[] { "TrustId" });
            DropIndex("dbo.TrustAccount", new[] { "AssetAccountId" });
            DropIndex("dbo.TrustAccount", new[] { "TokenId" });
            DropIndex("dbo.TokenConversion", new[] { "TokenAccountId" });
            DropIndex("dbo.TokenConversion", new[] { "AssetTransferId" });
            DropIndex("dbo.OrderLeg", new[] { "OrderID" });
            DropIndex("dbo.OrderFilled", new[] { "OrderLegID" });
            DropIndex("dbo.ClientLogin", new[] { "Client_ClientId" });
            DropIndex("dbo.ClientRegistration", new[] { "ClientId" });
            DropIndex("dbo.ClientKYC", new[] { "ClientRegistration_ClientRegistrationId" });
            DropIndex("dbo.ClientAuthentication", new[] { "ClientId" });
            DropIndex("dbo.ClientAccountHolder", new[] { "ClientAccountId" });
            DropIndex("dbo.ClientAccountHolder", new[] { "ClientId" });
            DropIndex("dbo.Token", new[] { "Asset_AssetId" });
            DropIndex("dbo.Token", new[] { "TrustId" });
            DropIndex("dbo.Token", new[] { "CustodianId" });
            DropIndex("dbo.Token", new[] { "AssetAccountId" });
            DropIndex("dbo.ClientAccount", new[] { "ClientId" });
            DropIndex("dbo.TokenAccount", new[] { "TokenId" });
            DropIndex("dbo.TokenAccount", new[] { "ClientAccountId" });
            DropIndex("dbo.TokenFlow", new[] { "TokenAccountId" });
            DropIndex("dbo.TokenFlow", new[] { "TradeId" });
            DropIndex("dbo.BlockChainTokenFlow", new[] { "TokenFlow_TokenFlowId" });
            DropIndex("dbo.AssetTransfer", new[] { "AssetAccountId" });
            DropIndex("dbo.AssetRate", new[] { "RateProviderId" });
            DropIndex("dbo.AssetRate", new[] { "AssetId" });
            DropIndex("dbo.AssetAccount", new[] { "CustodianId" });
            DropIndex("dbo.AssetAccount", new[] { "AssetId" });
            DropTable("dbo.TrustAccount");
            DropTable("dbo.TokenConversion");
            DropTable("dbo.OrderFilledMatch");
            DropTable("dbo.OrderLeg");
            DropTable("dbo.OrderFilled");
            DropTable("dbo.Order");
            DropTable("dbo.ClientLogin");
            DropTable("dbo.ClientRegistration");
            DropTable("dbo.ClientKYC");
            DropTable("dbo.ClientAuthentication");
            DropTable("dbo.ClientAccountHolder");
            DropTable("dbo.TokenTrade");
            DropTable("dbo.Trust");
            DropTable("dbo.Token");
            DropTable("dbo.Client");
            DropTable("dbo.ClientAccount");
            DropTable("dbo.TokenAccount");
            DropTable("dbo.TokenFlow");
            DropTable("dbo.BlockChainTokenFlow");
            DropTable("dbo.AssetTransfer");
            DropTable("dbo.AssetRateProvider");
            DropTable("dbo.AssetRate");
            DropTable("dbo.Custodian");
            DropTable("dbo.AssetAccount");
            DropTable("dbo.Asset");
        }
    }
}
