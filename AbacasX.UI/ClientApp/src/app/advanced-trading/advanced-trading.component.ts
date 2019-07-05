import { Component, OnInit, Input, OnChanges, SimpleChange } from '@angular/core';
import { IQuickOrder, BuySellTypeEnum, OrderPriceTermsEnum, OrderTypeEnum, TokenPairRate, OrderStatusEnum, IOrder, OrderLegFillStatusEnum } from '../../shared/interfaces';
import { DataService } from '../../core/data.service';
import { Router } from '@angular/router';
import { LoginService } from '../../core/login.service';


@Component({
  selector: 'advanced-trading',
  templateUrl: './advanced-trading.component.html',
  styleUrls: ['./advanced-trading.component.css']

})
export class AdvancedTradingComponent implements OnInit {
  @Input()
  selectedAssetPair: string = "";
  @Input()
  TokenPairRate: TokenPairRate;

  public Token1PriceCurrency: string = "USD";
  public Token2PriceCurrency: string = "USD";


  public orderExpiration: Date = new Date();
  public orderExpirationTime: Date = new Date();

  selectedOrderType: string = "GTC";

  IsCrossCurrency: boolean = false;
  IsBuyOrder: boolean = true;
  IsMarketOrder: boolean = true;
  TokenBalanceAvailable: number = 0;
  IsTokenBalanceExceeded = false;
  changeLog: string[] = [];

  OrderDescription: string = "Buy @AAPL for @GOOG";
  ExchangeDescription: string = "@AAPL for @GOOG";
  errorMessage: string = "";

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

  order: IOrder = {
    clientId: 0,
    clientAccountId: 0,
    buySellType: BuySellTypeEnum.Buy,
    token1Id: '',
    token1Amount: 0,
    token1AmountFilled:0,
    token2Id: '',
    orderPrice: 0,
    orderPriceTerms: OrderPriceTermsEnum.Token2PerToken1,
    orderType: OrderTypeEnum.Market,
    token2Amount: 0,
    orderStatus: OrderStatusEnum.Active,
    orderFillStatus: OrderLegFillStatusEnum.None,
    priceFilled: 0
  };

  constructor(private router: Router,
    private dataService: DataService, private loginService: LoginService) {
    this.orderExpirationTime.setHours(17, 0, 0);
  };

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

      if (propName === "TokenPairRate") {
        this.updateTradingPrices();
      }

    }
    this.changeLog.push(log.join(', '));
  }

  submit() {

    this.quickOrder.ClientId = this.loginService.userId;

    this.order.token1Id = this.quickOrder.Token1Id;
    this.order.token2Id = this.quickOrder.Token2Id;
    this.order.token1Amount = this.quickOrder.Token1Amount;
    this.order.token2Amount = this.quickOrder.Token2Amount;
    this.order.buySellType = this.quickOrder.BuySellType;
    this.order.clientId = this.quickOrder.ClientId;
    this.order.clientAccountId = this.quickOrder.ClientAccountId;
    this.order.orderPrice = this.quickOrder.OrderPrice;
    this.order.orderPriceTerms = this.quickOrder.OrderPriceTerms;

    console.log("Advanced Order Submitted");

    this.dataService.addOrder(this.order)
      .subscribe((order: IOrder) => {
        if (order) {
        }
        else {
          this.errorMessage = 'Unable to add Order';
        }
      },
        (err: any) => console.log(err));

    this.quickOrder.Token1Amount = 0;
  }

  
  updateTradingPrices() {
    this.Token1PriceCurrency = this.TokenPairRate.currency1;
    this.Token2PriceCurrency = this.TokenPairRate.currency2;

    if (this.IsBuyOrder) {
      this.token1Rate = this.TokenPairRate.token1AskRate;
      this.token2Rate = this.TokenPairRate.token2BidRate;
      this.tokenFXRate = this.TokenPairRate.currencyPairAskRate;
    }
    else {
      this.token1Rate = this.TokenPairRate.token1BidRate;
      this.token2Rate = this.TokenPairRate.token2AskRate;
      this.tokenFXRate = this.TokenPairRate.currencyPairBidRate;
    }

    if (this.TokenPairRate.currency1 == this.TokenPairRate.currency2)
      this.IsCrossCurrency = false;
    else
      this.IsCrossCurrency = true;

    if (this.quickOrder.BuySellType == BuySellTypeEnum.Buy)
      this.quickOrder.OrderPrice = this.TokenPairRate.askRate;
    else
      this.quickOrder.OrderPrice = this.TokenPairRate.bidRate;

    if (this.IsBuyOrder)
      this.OrderDescription = "Buy " + this.quickOrder.Token1Id + " for " + this.quickOrder.Token2Id;
    else
      this.OrderDescription = "Sell " + this.quickOrder.Token1Id + " for " + this.quickOrder.Token2Id;

    this.ExchangeDescription = this.quickOrder.Token1Id + " - " + this.quickOrder.Token2Id;
    this.quickOrder.Token2Amount = this.quickOrder.Token1Amount * this.quickOrder.OrderPrice;

    this.CheckTokenBalance();
  }


  orderTypeChange(selectedOrderType: string) {
  }

  updateOrderPrice(orderPrice: string) {
    this.quickOrder.OrderPrice = Number(orderPrice);
    this.quickOrder.Token2Amount = this.quickOrder.Token1Amount * this.quickOrder.OrderPrice;
    this.CheckTokenBalance();
  }

  updateToken1Amount(token1Amount: string) {
    this.quickOrder.Token1Amount = Number(token1Amount);
    this.quickOrder.Token2Amount = this.quickOrder.Token1Amount * this.quickOrder.OrderPrice;
    this.CheckTokenBalance();
  }

  selectedAssetPairChanged(newAssetPair: string) {

    // Locate the assets which start with an @
    var results = newAssetPair.match(/@\S*/g);

    console.log("Asset 1 {0}", results[0]);
    console.log("Asset 2 {0}", results[1]);

    this.quickOrder.Token1Id = results[0];
    this.quickOrder.Token2Id = results[1];
    this.quickOrder.Token1Amount = 0;

    this.Token1PriceCurrency = this.TokenPairRate.currency1;
    this.Token2PriceCurrency = this.TokenPairRate.currency2;

    if (this.IsBuyOrder) {
      this.token1Rate = this.TokenPairRate.token1AskRate;
      this.token2Rate = this.TokenPairRate.token2BidRate;
      this.tokenFXRate = this.TokenPairRate.currencyPairAskRate;
    }
    else {
      this.token1Rate = this.TokenPairRate.token1BidRate;
      this.token2Rate = this.TokenPairRate.token2AskRate;
      this.tokenFXRate = this.TokenPairRate.currencyPairBidRate;
    }

    if (this.TokenPairRate.currency1 == this.TokenPairRate.currency2)
      this.IsCrossCurrency = false;
    else
      this.IsCrossCurrency = true;

    if (this.quickOrder.BuySellType == BuySellTypeEnum.Buy)
      this.quickOrder.OrderPrice = this.TokenPairRate.askRate;
    else
      this.quickOrder.OrderPrice = this.TokenPairRate.bidRate;

    if (this.IsBuyOrder)
      this.OrderDescription = "Buy " + this.quickOrder.Token1Id + " for " + this.quickOrder.Token2Id;
    else
      this.OrderDescription = "Sell " + this.quickOrder.Token1Id + " for " + this.quickOrder.Token2Id;

    this.ExchangeDescription = this.quickOrder.Token1Id + " - " + this.quickOrder.Token2Id;

    this.quickOrder.Token2Amount = this.quickOrder.Token1Amount * this.quickOrder.OrderPrice;
  }

  ngOnInit() {

    // Initial setup is to Buy Token 1, thus the offer on Token1, Bid on token 2
    // Buying token1 means selling currency1 and buying currency 2
    // thus the offer on currency2 - currency1.

    this.quickOrder.ClientId = this.loginService.userId;

    this.token1Rate = this.TokenPairRate.token1AskRate;
    this.token2Rate = this.TokenPairRate.token2BidRate;
    this.tokenFXRate = this.TokenPairRate.currencyPairAskRate;
    this.Token1PriceCurrency = this.TokenPairRate.currency1;
    this.Token2PriceCurrency = this.TokenPairRate.currency2;

    if (this.TokenPairRate.currency1 == this.TokenPairRate.currency2)
      this.IsCrossCurrency = false;
    else
      this.IsCrossCurrency = true;

    if (this.quickOrder.BuySellType == BuySellTypeEnum.Buy)
      this.quickOrder.OrderPrice = this.TokenPairRate.askRate;
    else
      this.quickOrder.OrderPrice = this.TokenPairRate.bidRate;

    this.quickOrder.Token2Amount = this.quickOrder.Token1Amount * this.quickOrder.OrderPrice;
  }

  MarketOrderClicked() {
    this.quickOrder.OrderType = OrderTypeEnum.Market;
    this.IsMarketOrder = true;
  }

  CheckTokenBalance() {

    // No need to check of the amounts are zero.
    if ((this.IsBuyOrder == true ? this.quickOrder.Token2Amount : this.quickOrder.Token1Amount) == 0)
      return;

    console.log("Checking token balance for {0}", (this.IsBuyOrder == true ? this.quickOrder.Token2Id : this.quickOrder.Token1Id));


    this.dataService.getClientTokenBalance(this.quickOrder.ClientId, (this.IsBuyOrder == true ? this.quickOrder.Token2Id : this.quickOrder.Token1Id))
      .subscribe((tokenBalance: number) => {
        if (tokenBalance) {

          this.TokenBalanceAvailable = tokenBalance;

          if (this.IsBuyOrder == true) {
            if (this.quickOrder.Token2Amount > this.TokenBalanceAvailable)
              this.IsTokenBalanceExceeded = true;
            else
              this.IsTokenBalanceExceeded = false;
          }
          else {
            if (this.quickOrder.Token1Amount > this.TokenBalanceAvailable)
              this.IsTokenBalanceExceeded = true;
            else
              this.IsTokenBalanceExceeded = false;
          }

          console.log("Token Balance is {0}", this.TokenBalanceAvailable);

        }
        else {
          this.errorMessage = 'Unable to get client token balance';
          this.IsTokenBalanceExceeded = true;
        }
      },
        (err: any) => console.log(err));
  }

  LimitOrderClicked() {
    this.quickOrder.OrderType = OrderTypeEnum.Limit;
    this.IsMarketOrder = false;
  }

  BuyToken1Clicked() {

    this.token1Rate = this.TokenPairRate.token1AskRate;
    this.token2Rate = this.TokenPairRate.token2BidRate;
    this.tokenFXRate = this.TokenPairRate.currencyPairAskRate;


    this.quickOrder.BuySellType = BuySellTypeEnum.Buy;
    this.IsBuyOrder = true;
    
    this.quickOrder.OrderPrice = this.TokenPairRate.askRate;
    this.quickOrder.Token2Amount = this.quickOrder.Token1Amount * this.quickOrder.OrderPrice;

    if (this.IsBuyOrder)
      this.OrderDescription = "Buy " + this.quickOrder.Token1Id + " for " + this.quickOrder.Token2Id;
    else
      this.OrderDescription = "Sell " + this.quickOrder.Token1Id + " for " + this.quickOrder.Token2Id;

    this.CheckTokenBalance();
  }

  SellToken1Clicked() {


    this.token1Rate = this.TokenPairRate.token1BidRate;
    this.token2Rate = this.TokenPairRate.token2AskRate;
    this.tokenFXRate = this.TokenPairRate.currencyPairBidRate;



    this.quickOrder.BuySellType = BuySellTypeEnum.Sell;
    this.IsBuyOrder = false;
    this.quickOrder.OrderPrice = this.TokenPairRate.bidRate;
    this.quickOrder.Token2Amount = this.quickOrder.Token1Amount * this.quickOrder.OrderPrice;

    if (this.IsBuyOrder)
      this.OrderDescription = "Buy " + this.quickOrder.Token1Id + " for " + this.quickOrder.Token2Id;
    else
      this.OrderDescription = "Sell " + this.quickOrder.Token1Id + " for " + this.quickOrder.Token2Id;

    this.CheckTokenBalance();
  }
}
