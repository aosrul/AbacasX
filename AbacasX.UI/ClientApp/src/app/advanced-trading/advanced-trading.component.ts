import { Component, OnInit, Input, OnChanges, SimpleChange } from '@angular/core';
import { IQuickOrder, BuySellTypeEnum, OrderPriceTermsEnum, OrderTypeEnum, TradingRates } from '../../shared/interfaces';


@Component({
  selector: 'advanced-trading',
  templateUrl: './advanced-trading.component.html',
  styleUrls: ['./advanced-trading.component.css']

})
export class AdvancedTradingComponent implements OnInit {
  @Input()
  selectedAssetPair: string = "";
  @Input()
  TokenExchangeBid: number = 0;
  @Input()
  TokenExchangeAsk: number = 0;
  @Input()
  IsCrossCurrency: boolean = false;
  @Input()
  Token1PriceCurrency: string = "USD";
  @Input()
  Token2PriceCurrency: string = "USD";
  @Input()
  tradingRates: TradingRates = new TradingRates();

  public orderExpiration: Date = new Date();
  public orderExpirationTime: Date = new Date();

  selectedOrderType: string = "GTC";

  IsBuyOrder: boolean = true;
  IsMarketOrder: boolean = true;
  changeLog: string[] = [];

  OrderDescription: string = "Buy @AAPL with @GOOG";
  ExchangeDescription: string = "@AAPL for @GOOG";

  public token1Rate: number = 1.0;
  public token2Rate: number = 1.0;
  public tokenFXRate: number = 1.0;

  quickOrder: IQuickOrder = {
    ClientId: 0,
    ClientAccountId: 0,
    BuySellType: BuySellTypeEnum.Buy,
    Token1Id: '',
    Token1Amount: 0,
    Token2Id: '',
    OrderPrice: 0,
    OrderPriceTerms: OrderPriceTermsEnum.Token2PerToken1,
    OrderType: OrderTypeEnum.Market,
    Token2Amount: 0
  };

  constructor() {
    this.orderExpirationTime.setHours(17, 0, 0);

    this.token1Rate = 222.415;
    this.token2Rate = 49;
    this.tokenFXRate = 1.1850;

    //this.tradingRates.Token1Id = "@AAPL";
    //this.tradingRates.Token2Id = "@GOOG";
    //this.tradingRates.Token1Bid = 1.0;
    //this.tradingRates.Token1Ask = 1.0;

    //this.tradingRates.TokenExchangeBid = 1.0;
    //this.tradingRates.TokenExchangeAsk = 1.0;

    //this.tradingRates.Token1PriceCurrencyBid = 1.0;
    //this.tradingRates.Token1PriceCurrencyAsk = 1.0;
    //this.tradingRates.Token2PriceCurrencyBid = 1.0;
    //this.tradingRates.Token2PriceCurrencyAsk = 1.0;

    //this.tradingRates.IsCrossCurrency = false;

    //this.tradingRates.Token1PriceCurrency = "@USD";
    //this.tradingRates.Token2PriceCurrency = "@USD";
    //this.tradingRates.TokenExchangeFXBid = 1.0;
    //this.tradingRates.TokenExchangeFXAsk = 1.0;
  }
 
  ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
    let log: string[] = [];

    for (let propName in changes) {
      let changedProp = changes[propName];
      let to = JSON.stringify(changedProp.currentValue);

      if (changedProp.isFirstChange()) {
        log.push(`Initial value of ${propName} set to ${to}`);
      } else {
        let from = JSON.stringify(changedProp.previousValue);
        log.push(`${propName} changed from ${from} to ${to}`);
      }

      if (propName === "selectedAssetPair")
        this.selectedAssetPairChanged(changedProp.currentValue);

      if (propName === "TokenExchangeAsk") {
        if (this.IsBuyOrder)
          this.updateOrderPrice(changedProp.currentValue);
      }

      if (propName === "TokenExchangeBid") {
        if (this.IsBuyOrder == false) {
          this.updateOrderPrice(changedProp.currentValue);
        }
      }

      if (propName === "tradingRates") {
        this.updateTradingPrices();
      }

    }
    this.changeLog.push(log.join(', '));
  }


  updateTradingPrices() {
    //this.token1Rate = this.tradingRates.Token1Bid;
    //this.token2Rate = this.tradingRates.Token2Bid;
    //this.tokenFXRate = this.tradingRates.TokenExchangeFXAsk;
  }


  orderTypeChange(selectedOrderType: string) {
  }

  updateOrderPrice(orderPrice: string) {
    this.quickOrder.OrderPrice = Number(orderPrice);
    this.quickOrder.Token2Amount = this.quickOrder.Token1Amount * this.quickOrder.OrderPrice;
  }

  updateToken1Amount(token1Amount: string) {
    this.quickOrder.Token1Amount = Number(token1Amount);
    this.quickOrder.Token2Amount = this.quickOrder.Token1Amount * this.quickOrder.OrderPrice;
  }

  selectedAssetPairChanged(newAssetPair: string) {
    if (this.selectedAssetPair == "@AAPL - @GOOG") {
      this.quickOrder.Token1Id = "@AAPL";
      this.quickOrder.Token2Id = "@GOOG";
      this.IsCrossCurrency = false;
    }

    if (this.selectedAssetPair == "@AAPL - @GOLD") {
      this.quickOrder.Token1Id = "@AAPL";
      this.quickOrder.Token2Id = "@GOLD";
      this.IsCrossCurrency = false;
    }

    if (this.selectedAssetPair == "@AAPL - @MSFT") {
      this.quickOrder.Token1Id = "@AAPL";
      this.quickOrder.Token2Id = "@MSFT";
      this.IsCrossCurrency = false;
    }

    if (this.selectedAssetPair == "@AAPL - @BTC") {
      this.quickOrder.Token1Id = "@AAPL";
      this.quickOrder.Token2Id = "@BTC";
      this.IsCrossCurrency = false;
    }

    if (this.selectedAssetPair == "@AAPL - @USD") {
      this.quickOrder.Token1Id = "@AAPL";
      this.quickOrder.Token2Id = "@USD";
      this.IsCrossCurrency = false;
    }

    if (this.selectedAssetPair == "@AAPL - @BNP") {
      this.quickOrder.Token1Id = "@AAPL";
      this.quickOrder.Token2Id = "@BNP";
      this.IsCrossCurrency = true;
    }

    if (this.IsBuyOrder)
      this.OrderDescription = "Buy " + this.quickOrder.Token1Id + " with " + this.quickOrder.Token2Id;
    else
      this.OrderDescription = "Sell " + this.quickOrder.Token1Id + " with " + this.quickOrder.Token2Id;

    this.ExchangeDescription = this.quickOrder.Token1Id + " for " + this.quickOrder.Token2Id;
  }

  ngOnInit() {

    if (this.quickOrder.BuySellType == BuySellTypeEnum.Buy)
      this.quickOrder.OrderPrice = this.TokenExchangeAsk;
    else
      this.quickOrder.OrderPrice = this.TokenExchangeBid;

    this.quickOrder.Token2Amount = this.quickOrder.Token1Amount * this.quickOrder.OrderPrice;
  }

  MarketOrderClicked() {
    this.quickOrder.OrderType = OrderTypeEnum.Market;
    this.IsMarketOrder = true;
  }

  LimitOrderClicked() {
    this.quickOrder.OrderType = OrderTypeEnum.Limit;
    this.IsMarketOrder = false;
  }

  BuyToken1Clicked() {
    this.quickOrder.BuySellType = BuySellTypeEnum.Buy;
    this.IsBuyOrder = true;
    this.OrderDescription = "Buy " + this.quickOrder.Token1Id + " with " + this.quickOrder.Token2Id;
    this.quickOrder.OrderPrice = this.TokenExchangeAsk;
    this.quickOrder.Token2Amount = this.quickOrder.Token1Amount * this.quickOrder.OrderPrice;
  }

  SellToken1Clicked() {
    this.quickOrder.BuySellType = BuySellTypeEnum.Sell;
    this.IsBuyOrder = false;
    this.OrderDescription = "Sell " + this.quickOrder.Token1Id + " with " + this.quickOrder.Token2Id;
    this.quickOrder.OrderPrice = this.TokenExchangeBid;
    this.quickOrder.Token2Amount = this.quickOrder.Token1Amount * this.quickOrder.OrderPrice;
  }
}
