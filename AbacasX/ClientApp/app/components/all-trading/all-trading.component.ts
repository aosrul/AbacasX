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


    constructor() {
    }

    ngOnInit() {

        this.selectedAssetPair = "@AAPL - @GOOG";
        this.Token1Id = "@AAPL";
        this.Token2Id = "@GOOG";
        this.TokenExchangeBid = 0.0;
        this.TokenExchangeAsk = 0.0;
        this.assetPairChange("@AAPL - @GOOG");
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

                }
                break;
            case "@AAPL - @MSFT":
                {
                    this.Token1Id = "@AAPL";
                    this.Token2Id = "@MSFT";

                    this.TokenExchangeAsk = this.AAPLAskPrice * (1.0 / this.MSFTBidPrice);
                    this.TokenExchangeBid = this.AAPLBidPrice * (1.0 / this.MSFTAskPrice);
                    this.selectedAssetPair = selectedAssetPair;
                }
                break;

            case "@AAPL - @GOLD":
                {
                    this.Token1Id = "@AAPL";
                    this.Token2Id = "@GOLD";

                    this.TokenExchangeAsk = this.AAPLAskPrice * (1.0 / this.GOLDBidPrice);
                    this.TokenExchangeBid = this.AAPLBidPrice * (1.0 / this.GOLDAskPrice);
                    this.selectedAssetPair = selectedAssetPair;
                }
                break;

            case "@AAPL - @BTC":
                {
                    this.Token1Id = "@AAPL";
                    this.Token2Id = "@BTC";

                    this.TokenExchangeAsk = this.AAPLAskPrice * (1.0 / this.BTCBidPrice);
                    this.TokenExchangeBid = this.AAPLBidPrice * (1.0 / this.BTCAskPrice);
                    this.selectedAssetPair = selectedAssetPair;
                }
                break;

            default:
                {
                    this.TokenExchangeAsk = 0;
                    this.TokenExchangeBid = 0;
                }
        }
    }
}
