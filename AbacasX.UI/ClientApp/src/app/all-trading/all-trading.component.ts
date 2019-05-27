import { Component, OnInit } from '@angular/core';
import { TradingRates, TokenPairRate } from '../../shared/interfaces';
import { indexDebugNode } from '@angular/core/src/debug/debug_node';
import { rateSignalRService } from '../../core/rate.service';
import { either, last } from '@progress/kendo-angular-dateinputs/dist/es2015/util';

@Component({
  selector: 'all-trading',
  templateUrl: './all-trading.component.html',
  providers: [rateSignalRService]
})
export class AllTradingComponent implements OnInit {

  selectedAssetPair: string = "@AAPL - @GOOG";
  public Token1Id: string = "";
  public Token2Id: string = "";
  TokenExchangeBid: number = 0;
  TokenExchangeAsk: number = 0;
  IsCrossCurrency: boolean = false;
  IsSubscribed: boolean = false;

  Token1PriceCurrency: string = "@USD";
  Token2PriceCurrency: string = "@USD";

  Token1PriceCurrencyBid: number = 1.0;
  Token1PriceCurrencyAsk: number = 1.0;
  Token2PriceCurrencyBid: number = 1.0;
  Token2PriceCurrencyAsk: number = 1.0;

  TokenExchangeFXBid: number = 1.0;
  TokenExchangeFXAsk: number = 1.0;

  public source: Array<string> = ["@AAPL", "@GOOG", "@MSFT", "@GOLD", "@BTC", "@USD", "@ETH", "@BNP"];
  public data: Array<string>;
  public data2: Array<string>;
  public events: string[] = [];
  public value: number = 20;
  public tokens: string[] = [];
  public tokenRates: any[] = [];
  public tokenPairRate: TokenPairRate;

  constructor(private rateService: rateSignalRService) {

    rateService.connectionEstablished.subscribe(() => {

      this.IsSubscribed = true;

      console.log("All Trading Component Subscription Connection");

      rateService.getTokenList().then((data) => {
        console.log(data);
        this.tokens = data;
        this.source = data;
        this.data = data.slice();
        this.data2 = data.slice();

      }).catch((reason: any) => { console.log(reason); });


      rateService.getTokenRateList().then((data) => {
        console.log(data);
        this.tokenRates = data;
      }).catch((reason: any) => { console.log(reason); });

      rateService.getTokenPairRate("@AAPL", "@GOOG").then((data) => {
        console.log(data);
        this.tokenPairRate = data;
        this.updateTokenRate();
      }).catch((reason: any) => { console.log(reason); });

    });
  }

  ngOnInit() {

    this.selectedAssetPair = "@AAPL - @GOOG";
    this.TokenExchangeBid = 0.0;
    this.TokenExchangeAsk = 0.0;
    this.assetPairChange("@AAPL - @GOOG");
    this.Token1Id = null;
    this.Token2Id = null;

    this.Token1PriceCurrencyBid = 1.0;
    this.Token1PriceCurrencyAsk = 1.0;
    this.Token2PriceCurrencyBid = 1.0;
    this.Token2PriceCurrencyAsk = 1.0;

    this.Token1PriceCurrency = "@USD";
    this.Token2PriceCurrency = "@USD";
    this.IsCrossCurrency = false;

    this.TokenExchangeFXBid = 1.0;
    this.TokenExchangeFXAsk = 1.0;

  }

  public refreshTokenPairRate(): void {
    if (this.IsSubscribed == true) {
      this.rateService.getTokenPairRate(this.Token1Id, this.Token2Id).then((data) => {
        console.log(data);
        this.tokenPairRate = data;
        this.updateTokenRate();
      }).catch((reason: any) => { console.log(reason); });
    }
    else {
      this.initTokenRate();
    }
  }

  public initTokenRate(): void {

    this.TokenExchangeAsk = 0;
    this.TokenExchangeBid = 0;

    this.Token1PriceCurrencyBid = 1.0;
    this.Token1PriceCurrencyAsk = 1.0;
    this.Token2PriceCurrencyBid = 1.0;
    this.Token2PriceCurrencyAsk = 1.0;

    this.Token1PriceCurrency = "@USD";
    this.Token2PriceCurrency = "@USD";

    this.IsCrossCurrency = false;

    this.TokenExchangeFXBid = 1.0;
    this.TokenExchangeFXAsk = 1.0;
  }


  public updateTokenRate(): void {

    if (this.IsSubscribed == true) {

      this.TokenExchangeBid = this.tokenPairRate.bidRate;
      this.TokenExchangeAsk = this.tokenPairRate.askRate;

      this.Token1Id = this.tokenPairRate.token1Id;
      this.Token2Id = this.tokenPairRate.token2Id;

      this.Token1PriceCurrencyBid = this.tokenPairRate.currency1BidRate;
      this.Token1PriceCurrencyAsk = this.tokenPairRate.currency1AskRate;
      this.Token2PriceCurrencyBid = this.tokenPairRate.currency2BidRate;
      this.Token2PriceCurrencyAsk = this.tokenPairRate.currency2AskRate;

      this.Token1PriceCurrency = this.tokenPairRate.currency1;
      this.Token2PriceCurrency = this.tokenPairRate.currency2;
      if (this.Token1PriceCurrency == this.Token2PriceCurrency)
        this.IsCrossCurrency = false;
      else
        this.IsCrossCurrency = true;


      this.TokenExchangeFXBid = this.tokenPairRate.currencyPairBidRate;
      this.TokenExchangeFXAsk = this.tokenPairRate.currencyPairAskRate;
    }
  }


  public valueChange(value: any): void {

    this.log('valueChange', value);
    this.data2 = this.source.filter((item) => {
      if (item != this.Token1Id) { return item; }
    });

    if (this.Token1Id == this.Token2Id)
      this.Token2Id = null;

  }

  public selectionChange(value: any): void {
    this.log('selectionChange', value);
  }


  public valueChange2(value: any): void {
    this.log('valueChange2', value);

    this.data = this.source.filter((item) => {
      if (item != this.Token2Id) { return item; }
    });

    if (this.Token1Id == this.Token2Id)
      this.Token1Id = null;
  }

  public selectionChange2(value: any): void {
    this.log('selectionChange2', value);
  }


  private log(event: string, arg: any): void {
    this.events.push(`${event} ${arg || ''}`);
  }

  assetPairChange(selectedAssetPair: string) {

    // Locate the assets which start with an @
    var results = selectedAssetPair.match(/@\S*/g);

    console.log("Asset 1 {0}", results[0]);
    console.log("Asset 2 {0}", results[1]);

    this.Token1Id = results[0];
    this.Token2Id = results[1];

    this.refreshTokenPairRate();
  }
}
