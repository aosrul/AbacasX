import { Component, OnInit, Input, OnChanges, SimpleChange } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IOrder, BuySellTypeEnum, OrderPriceTermsEnum, OrderTypeEnum, OrderStatusEnum } from '../../shared/interfaces';
import { Router } from '@angular/router';
import { DataService } from '../../core/data.service';


@Component({
    selector: 'quick-trading-reactive',
    templateUrl: './quick-trading-reactive.component.html',
    styleUrls: ['./quick-trading-reactive.component.css']

})
export class QuickTradingReactiveComponent implements OnInit, OnChanges {

    orderForm?: FormGroup;

    errorMessage: string = "";

    @Input()
    selectedAssetPair: string = "";
    @Input()
    TokenExchangeBid: number = 0;
    @Input()
    TokenExchangeAsk: number = 0;

    IsBuyOrder: boolean = true;
    IsMarketOrder: boolean = true;
    changeLog: string[] = [];

    OrderDescription: string = "Buy @AAPL with @GOOG";

    quickOrder: IOrder = {
        clientId: 0,
        clientAccountId: 0,
        buySellType: BuySellTypeEnum.Buy,
        token1Id: '',
        token1Amount: 0,
        token2Id: '',
        orderPrice: 0,
        orderPriceTerms: OrderPriceTermsEnum.Token2PerToken1,
        orderType: OrderTypeEnum.Market,
        token2Amount: 0,
        orderStatus: OrderStatusEnum.Active,
        priceFilled: 0
    };


    constructor(private router: Router,
        private dataService: DataService,
        private formBuilder: FormBuilder) { }

    ngOnInit() {

        if (this.quickOrder.buySellType == BuySellTypeEnum.Buy)
            this.quickOrder.orderPrice = this.TokenExchangeAsk;
        else
            this.quickOrder.orderPrice = this.TokenExchangeBid;

        this.quickOrder.token1Amount = 0;
        this.quickOrder.token2Amount = this.quickOrder.token1Amount * this.quickOrder.orderPrice;

        this.buildForm();
    }

    buildForm() {
        this.orderForm = this.formBuilder.group({
            Token1Amount: [this.quickOrder.token1Amount, Validators.required],
            OrderPrice: [this.quickOrder.orderPrice, Validators.required],
        })
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
        }
        this.changeLog.push(log.join(', '));
    }

    submit({ value }:{ value: IOrder }) {
        this.dataService.addOrder(this.quickOrder)
            .subscribe((order: IOrder) => {
                if (order) {
                    // Successful Add
                }
                else {
                    this.errorMessage = 'Unable to add Order';
                }
            },
                (err: any) => console.log(err));

    }

    updateOrderPrice(orderPrice: string) {
        this.quickOrder.orderPrice = Number(orderPrice);
        this.quickOrder.token2Amount = this.quickOrder.token1Amount * this.quickOrder.orderPrice;
    }

    updateToken1Amount(token1Amount: string) {
        this.quickOrder.token1Amount = Number(token1Amount);
        this.quickOrder.token2Amount = this.quickOrder.token1Amount * this.quickOrder.orderPrice;
    }

    selectedAssetPairChanged(newAssetPair: string) {
        if (this.selectedAssetPair == "@AAPL - @GOOG") {
            this.quickOrder.token1Id = "@AAPL";
            this.quickOrder.token2Id = "@GOOG";
        }

        if (this.selectedAssetPair == "@AAPL - @GOLD") {
            this.quickOrder.token1Id = "@AAPL";
            this.quickOrder.token2Id = "@GOLD";
        }

        if (this.selectedAssetPair == "@AAPL - @MSFT") {
            this.quickOrder.token1Id = "@AAPL";
            this.quickOrder.token2Id = "@MSFT";
        }

        if (this.selectedAssetPair == "@AAPL - @BTC") {
            this.quickOrder.token1Id = "@AAPL";
            this.quickOrder.token2Id = "@BTC";
        }

        if (this.selectedAssetPair == "@AAPL - @USD") {
            this.quickOrder.token1Id = "@AAPL";
            this.quickOrder.token2Id = "@USD";
        }

        if (this.selectedAssetPair == "@AAPL - @BNP") {
            this.quickOrder.token1Id = "@AAPL";
            this.quickOrder.token2Id = "@BNP";
        }

        if (this.IsBuyOrder)
            this.OrderDescription = "Buy " + this.quickOrder.token1Id + " with " + this.quickOrder.token2Id;
        else
            this.OrderDescription = "Sell " + this.quickOrder.token1Id + " with " + this.quickOrder.token2Id;
    }



    MarketOrderClicked() {
        this.quickOrder.orderType = OrderTypeEnum.Market;
        this.IsMarketOrder = true;
    }

    LimitOrderClicked() {
        this.quickOrder.orderType = OrderTypeEnum.Limit;
        this.IsMarketOrder = false;
    }

    BuyToken1Clicked() {
        this.quickOrder.buySellType = BuySellTypeEnum.Buy;
        this.IsBuyOrder = true;
        this.OrderDescription = "Buy " + this.quickOrder.token1Id + " with " + this.quickOrder.token2Id;
        this.quickOrder.orderPrice = this.TokenExchangeAsk;
        this.quickOrder.token2Amount = this.quickOrder.token1Amount * this.quickOrder.orderPrice;
    }

    SellToken1Clicked() {
        this.quickOrder.buySellType = BuySellTypeEnum.Sell;
        this.IsBuyOrder = false;
        this.OrderDescription = "Sell " + this.quickOrder.token1Id + " with " + this.quickOrder.token2Id;
        this.quickOrder.orderPrice = this.TokenExchangeBid;
        this.quickOrder.token2Amount = this.quickOrder.token1Amount * this.quickOrder.orderPrice;
    }
}

