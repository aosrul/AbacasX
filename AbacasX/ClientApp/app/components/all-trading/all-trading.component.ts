import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'all-trading',
    templateUrl: './all-trading.component.html',
})
export class AllTradingComponent implements OnInit
{
    selectedAssetPair: string = "@AAPL - @GOOG";
    Token1Id: string = "@AAPL";
    Token2Id: string = "@GOOG";
    TokenExchangeBid: number = 0;
    TokenExchangeAsk: number = 0;
    IsCrossCurrency: boolean = false;

    GOOGBidPrice: number = 1085.50;
    GOOGAskPrice: number = 1085.60;

    AAPLBidPrice: number = 186.9;
    AAPLAskPrice: number = 187.5;

    MSFTBidPrice: number = 98.85;
    MSFTAskPrice: number = 98.9;

    GOLDBidPrice: number = 1299.56;
    GOLDAskPrice: number = 1300.30;

    BTCBidPrice: number = 7500;
    BTCAskPrice: number = 7525;

    USDBidPrice: number = 1.0;
    USDAskPrice: number = 1.0;

    BNPBidPrice: number = 53.59;
    BNPAskPrice: number = 55;

    Token1PriceCurrency: string = "@USD";
    Token2PriceCurrency: string = "@USD";

    Token1PriceCurrencyBid: number = 1.0;
    Token1PriceCurrencyAsk: number = 1.0;
    Token2PriceCurrencyBid: number = 1.0;
    Token2PriceCurrencyAsk: number = 1.0;

    USDFXBidRate: number = 1.0;
    USDFXAskRate: number = 1.0;

    EURFXBidRate: number = 1.1800;
    EURFXAskRate: number = 1.1850;

    constructor() {
    }

    ngOnInit() {

        this.selectedAssetPair = "@AAPL - @GOOG";
        this.Token1Id = "@AAPL";
        this.Token2Id = "@GOOG";
        this.TokenExchangeBid = 0.0;
        this.TokenExchangeAsk = 0.0;
        this.assetPairChange("@AAPL - @GOOG");

        this.Token1PriceCurrencyBid = 1.0;
        this.Token1PriceCurrencyAsk = 1.0;
        this.Token2PriceCurrencyBid = 1.0;
        this.Token2PriceCurrencyAsk = 1.0;

        this.Token1PriceCurrency = "@USD";
        this.Token2PriceCurrency = "@USD";
        this.IsCrossCurrency = false;

    }

    assetPairChange(selectedAssetPair: string) {
        switch (selectedAssetPair) {
            case "@AAPL - @GOOG":
                {
                    this.Token1Id = "@AAPL";
                    this.Token2Id = "@GOOG";

                    this.TokenExchangeAsk = this.AAPLAskPrice * (1.0 / this.GOOGBidPrice);
                    this.TokenExchangeBid = this.AAPLBidPrice * (1.0 / this.GOOGAskPrice);
                    this.selectedAssetPair = selectedAssetPair;

                    this.Token1PriceCurrencyBid = 1.0;
                    this.Token1PriceCurrencyAsk = 1.0;
                    this.Token2PriceCurrencyBid = 1.0;
                    this.Token2PriceCurrencyAsk = 1.0;

                    this.Token1PriceCurrency = "@USD";
                    this.Token2PriceCurrency = "@USD";
                    this.IsCrossCurrency = false;

                }
                break;
            case "@AAPL - @MSFT":
                {
                    this.Token1Id = "@AAPL";
                    this.Token2Id = "@MSFT";

                    this.TokenExchangeAsk = this.AAPLAskPrice * (1.0 / this.MSFTBidPrice);
                    this.TokenExchangeBid = this.AAPLBidPrice * (1.0 / this.MSFTAskPrice);
                    this.selectedAssetPair = selectedAssetPair;

                    this.Token1PriceCurrencyBid = 1.0;
                    this.Token1PriceCurrencyAsk = 1.0;
                    this.Token2PriceCurrencyBid = 1.0;
                    this.Token2PriceCurrencyAsk = 1.0;

                    this.Token1PriceCurrency = "@USD";
                    this.Token2PriceCurrency = "@USD";
                    this.IsCrossCurrency = false;
                }
                break;

            case "@AAPL - @GOLD":
                {
                    this.Token1Id = "@AAPL";
                    this.Token2Id = "@GOLD";

                    this.TokenExchangeAsk = this.AAPLAskPrice * (1.0 / this.GOLDBidPrice);
                    this.TokenExchangeBid = this.AAPLBidPrice * (1.0 / this.GOLDAskPrice);
                    this.selectedAssetPair = selectedAssetPair;

                    this.Token1PriceCurrencyBid = 1.0;
                    this.Token1PriceCurrencyAsk = 1.0;
                    this.Token2PriceCurrencyBid = 1.0;
                    this.Token2PriceCurrencyAsk = 1.0;

                    this.Token1PriceCurrency = "@USD";
                    this.Token2PriceCurrency = "@USD";
                    this.IsCrossCurrency = false;
                }
                break;

            case "@AAPL - @BTC":
                {
                    this.Token1Id = "@AAPL";
                    this.Token2Id = "@BTC";

                    this.TokenExchangeAsk = this.AAPLAskPrice * (1.0 / this.BTCBidPrice);
                    this.TokenExchangeBid = this.AAPLBidPrice * (1.0 / this.BTCAskPrice);
                    this.selectedAssetPair = selectedAssetPair;

                    this.Token1PriceCurrencyBid = 1.0;
                    this.Token1PriceCurrencyAsk = 1.0;
                    this.Token2PriceCurrencyBid = 1.0;
                    this.Token2PriceCurrencyAsk = 1.0;

                    this.Token1PriceCurrency = "@USD";
                    this.Token2PriceCurrency = "@USD";
                    this.IsCrossCurrency = false;
                }
                break;

            case "@AAPL - @USD":
                {
                    this.Token1Id = "@AAPL";
                    this.Token2Id = "@USD";

                    this.TokenExchangeAsk = this.AAPLAskPrice * (1.0 / this.USDBidPrice);
                    this.TokenExchangeBid = this.AAPLBidPrice * (1.0 / this.USDAskPrice);
                    this.selectedAssetPair = selectedAssetPair;

                    this.Token1PriceCurrencyBid = 1.0;
                    this.Token1PriceCurrencyAsk = 1.0;
                    this.Token2PriceCurrencyBid = 1.0;
                    this.Token2PriceCurrencyAsk = 1.0;

                    this.Token1PriceCurrency = "@USD";
                    this.Token2PriceCurrency = "@USD";
                    this.IsCrossCurrency = false;
                }
                break;

            case "@AAPL - @BNP":
                {
                    this.Token1Id = "@AAPL";
                    this.Token2Id = "@BNP";

                    this.Token1PriceCurrencyBid = 1.0;
                    this.Token1PriceCurrencyAsk = 1.0;
                    this.Token2PriceCurrencyBid = this.EURFXBidRate;
                    this.Token2PriceCurrencyAsk = this.EURFXAskRate;

                    this.Token1PriceCurrency = "@USD";
                    this.Token2PriceCurrency = "@EUR";

                    this.TokenExchangeBid = this.AAPLBidPrice *
                        (1.0 / this.EURFXAskRate) * (1.0 / this.BNPAskPrice);
                    this.TokenExchangeAsk = this.AAPLAskPrice *
                        (1.0 / this.EURFXBidRate) * (1.0 / this.BNPBidPrice);

                    this.selectedAssetPair = selectedAssetPair;
                    this.IsCrossCurrency = true;

                    
                }
                break;

            default:
                {
                    this.TokenExchangeAsk = 0;
                    this.TokenExchangeBid = 0;

                    this.Token1PriceCurrencyBid = 1.0;
                    this.Token1PriceCurrencyAsk = 1.0;
                    this.Token2PriceCurrencyBid = 1.0;
                    this.Token2PriceCurrencyAsk = 1.0;

                    this.Token1PriceCurrency = "@USD";
                    this.Token2PriceCurrency = "@USD";

                }
        }
    }
}
