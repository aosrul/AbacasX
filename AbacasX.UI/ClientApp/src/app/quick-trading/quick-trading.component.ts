import { Component, OnInit, Input, OnChanges, SimpleChange } from '@angular/core';
import { Router } from '@angular/router';
import { IOrder, BuySellTypeEnum, OrderPriceTermsEnum, OrderTypeEnum, OrderStatusEnum, OrderLegFillStatusEnum } from '../../shared/interfaces';
import { DataService } from '../../core/data.service';
import { LoginService } from '../../core/login.service';


@Component({
  selector: 'quick-trading',
  templateUrl: './quick-trading.component.html',
  styleUrls: ['./quick-trading.component.css'],
})
export class QuickTradingComponent implements OnInit, OnChanges {

  @Input()
  selectedAssetPair: string = "";
  @Input()
  TokenExchangeBid: number = 0;
  @Input()
  TokenExchangeAsk: number = 0;

  IsBuyOrder: boolean = true;
  IsMarketOrder: boolean = true;
  changeLog: string[] = [];
  TokenBalanceAvailable: number = 0;
  IsTokenBalanceExceeded: boolean = false;


  errorMessage: string = "";

  OrderDescription: string = "Buy @AAPL for @GOOG";

  constructor(private router: Router,
    private dataService: DataService, private loginService: LoginService) { }

  quickOrder: IOrder = {
    clientId: 0,
    clientAccountId: 0,
    buySellType: BuySellTypeEnum.Buy,
    token1Id: '',
    token1Amount: 0,
    token1AmountFilled: 0,
    token2Id: '',
    orderPrice: 0,
    orderPriceTerms: OrderPriceTermsEnum.Token2PerToken1,
    orderType: OrderTypeEnum.Market,
    token2Amount: 0,
    orderStatus: OrderStatusEnum.Active,
    orderFillStatus: OrderLegFillStatusEnum.None,
    priceFilled: 0
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

      // Don't change the price if the user is entering a limit price
      if (this.IsMarketOrder == true) {

        if (propName === "TokenExchangeAsk") {
          if (this.IsBuyOrder)
            this.updateOrderPrice(changedProp.currentValue);
        }

        if (propName === "TokenExchangeBid") {
          if (this.IsBuyOrder == false) {
            this.updateOrderPrice(changedProp.currentValue);
          }
        }
      }

    }
    this.changeLog.push(log.join(', '));
  }

  submit() {
    this.quickOrder.clientId = this.loginService.userId;

    this.dataService.addOrder(this.quickOrder)
      .subscribe((order: IOrder) => {
        if (order) {
        }
        else {
          this.errorMessage = 'Unable to add Order';
        }
      },
        (err: any) => console.log(err));

    this.quickOrder.token1Amount = 0;
  }


  updateOrderPrice(orderPrice: string) {
    this.quickOrder.orderPrice = Number(orderPrice);
    this.quickOrder.token2Amount = this.quickOrder.token1Amount * this.quickOrder.orderPrice;
    this.CheckTokenBalance();
  }

  updateToken1Amount(token1Amount: string) {
    this.quickOrder.token1Amount = Number(token1Amount);
    this.quickOrder.token2Amount = this.quickOrder.token1Amount * this.quickOrder.orderPrice;
    this.CheckTokenBalance();
  }

  selectedAssetPairChanged(newAssetPair: string) {

    // Locate the assets which start with an @
    var results = newAssetPair.match(/@\S*/g);

    console.log("Asset 1 {0}", results[0]);
    console.log("Asset 2 {0}", results[1]);

    this.quickOrder.token1Id = results[0];
    this.quickOrder.token2Id = results[1];

    if (this.IsBuyOrder)
      this.OrderDescription = "Buy " + this.quickOrder.token1Id + " for " + this.quickOrder.token2Id;
    else
      this.OrderDescription = "Sell " + this.quickOrder.token1Id + " for " + this.quickOrder.token2Id;
  }


  ngOnInit() {

    this.quickOrder.clientId = this.loginService.userId;

    if (this.quickOrder.buySellType == BuySellTypeEnum.Buy)
      this.quickOrder.orderPrice = this.TokenExchangeAsk;
    else
      this.quickOrder.orderPrice = this.TokenExchangeBid;

    this.quickOrder.token2Amount = this.quickOrder.token1Amount * this.quickOrder.orderPrice;
  }

  MarketOrderClicked() {
    this.quickOrder.orderType = OrderTypeEnum.Market;
    this.IsMarketOrder = true;
  }

  LimitOrderClicked() {
    this.quickOrder.orderType = OrderTypeEnum.Limit;
    this.IsMarketOrder = false;
  }

  CheckTokenBalance() {

    // No need to check of the amounts are zero.
    if ((this.IsBuyOrder == true ? this.quickOrder.token2Amount : this.quickOrder.token1Amount) == 0)
      return;

    console.log("Checking token balance for {0}", (this.IsBuyOrder == true ? this.quickOrder.token2Id : this.quickOrder.token1Id));


    this.dataService.getClientTokenBalance(this.quickOrder.clientId, (this.IsBuyOrder == true ? this.quickOrder.token2Id : this.quickOrder.token1Id))
      .subscribe((tokenBalance: number) => {
        if (tokenBalance) {

          this.TokenBalanceAvailable = tokenBalance;

          if (this.IsBuyOrder == true) {
            if (this.quickOrder.token2Amount > this.TokenBalanceAvailable)
              this.IsTokenBalanceExceeded = true;
            else
              this.IsTokenBalanceExceeded = false;
          }
          else {
            if (this.quickOrder.token1Amount > this.TokenBalanceAvailable)
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

  BuyToken1Clicked() {
    this.quickOrder.buySellType = BuySellTypeEnum.Buy;
    this.IsBuyOrder = true;
    this.OrderDescription = "Buy " + this.quickOrder.token1Id + " for " + this.quickOrder.token2Id;
    this.quickOrder.orderPrice = this.TokenExchangeAsk;
    this.quickOrder.token2Amount = this.quickOrder.token1Amount * this.quickOrder.orderPrice;
    this.CheckTokenBalance();
  }

  SellToken1Clicked() {
    this.quickOrder.buySellType = BuySellTypeEnum.Sell;
    this.IsBuyOrder = false;
    this.OrderDescription = "Sell " + this.quickOrder.token1Id + " for " + this.quickOrder.token2Id;
    this.quickOrder.orderPrice = this.TokenExchangeBid;
    this.quickOrder.token2Amount = this.quickOrder.token1Amount * this.quickOrder.orderPrice;
    this.CheckTokenBalance();
  }
}

