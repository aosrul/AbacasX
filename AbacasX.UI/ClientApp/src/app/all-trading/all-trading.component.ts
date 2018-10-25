import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'all-trading',
  templateUrl: './all-trading.component.html',
})
export class AllTradingComponent implements OnInit {
  selectedAssetPair: string = "@AAPL - @GOOG";
  public Token1Id: string = "@AAPL";
  public Token2Id: string = "@GOOG";
  TokenExchangeBid: number = 0;
  TokenExchangeAsk: number = 0;
  IsCrossCurrency: boolean = false;

  GOOGBidPrice: number = 1103.06;
  GOOGAskPrice: number = 1106.36;

  AAPLBidPrice: number = 222.415;
  AAPLAskPrice: number = 223.0;

  MSFTBidPrice: number = 109.77;
  MSFTAskPrice: number = 110;

  GOLDBidPrice: number = 1223.63;
  GOLDAskPrice: number = 1230.50;

  BTCBidPrice: number = 6431.00;
  BTCAskPrice: number = 6440.00;

  USDBidPrice: number = 1.0;
  USDAskPrice: number = 1.0;

  BNPBidPrice: number = 47.59;
  BNPAskPrice: number = 49;

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

  public source: Array<string> = ["@AAPL", "@GOOG", "@MSFT", "@GOLD", "@BTC", "@USD", "@BNP"];
  public data: Array<string>;
  public data2: Array<string>;
  public events: string[] = [];
  public value: number = 20;

  constructor() {
    this.data = this.source.slice();
    this.data2 = this.source.slice();
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


  public valueChange(value: any): void {
    this.log('valueChange', value);
  }

  public selectionChange(value: any): void {
    this.log('selectionChange', value);
  }


  public valueChange2(value: any): void {
    this.log('valueChange', value);
  }

  public selectionChange2(value: any): void {
    this.log('selectionChange', value);
  }


  private log(event: string, arg: any): void {
    this.events.push(`${event} ${arg || ''}`);
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

      case "@MSFT - @BTC":
        {
          this.Token1Id = "@MSFT";
          this.Token2Id = "@BTC";

          this.TokenExchangeAsk = this.MSFTAskPrice * (1.0 / this.BTCBidPrice);
          this.TokenExchangeBid = this.MSFTBidPrice * (1.0 / this.BTCAskPrice);
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
