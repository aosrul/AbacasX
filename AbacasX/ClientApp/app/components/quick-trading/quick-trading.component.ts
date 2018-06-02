import { Component, OnInit, Input, OnChanges, SimpleChange } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IQuickOrder, BuySellTypeEnum, OrderPriceTermsEnum, OrderTypeEnum } from '../../shared/interfaces';
import { validateConfig } from '@angular/router/src/config';

@Component({
    selector: 'quick-trading',
    templateUrl: './quick-trading.component.html',
    styleUrls: ['./quick-trading.component.css']

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

    OrderDescription: string = "Buy @AAPL with @GOOG";

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
        }

        if (this.selectedAssetPair == "@AAPL - @GOLD") {
            this.quickOrder.Token1Id = "@AAPL";
            this.quickOrder.Token2Id = "@GOLD";
        }

        if (this.selectedAssetPair == "@AAPL - @MSFT") {
            this.quickOrder.Token1Id = "@AAPL";
            this.quickOrder.Token2Id = "@MSFT";
        }

        if (this.selectedAssetPair == "@AAPL - @BTC") {
            this.quickOrder.Token1Id = "@AAPL";
            this.quickOrder.Token2Id = "@BTC";
        }

        if (this.IsBuyOrder)
            this.OrderDescription = "Buy " + this.quickOrder.Token1Id + " for " + this.quickOrder.Token2Id;
        else
            this.OrderDescription = "Sell " + this.quickOrder.Token1Id + " for " + this.quickOrder.Token2Id;
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
        this.OrderDescription = "Buy " + this.quickOrder.Token1Id + " for " + this.quickOrder.Token2Id;
        this.quickOrder.OrderPrice = this.TokenExchangeAsk;
        this.quickOrder.Token2Amount = this.quickOrder.Token1Amount * this.quickOrder.OrderPrice;
    }

    SellToken1Clicked() {
        this.quickOrder.BuySellType = BuySellTypeEnum.Sell;
        this.IsBuyOrder = false;
        this.OrderDescription = "Sell " + this.quickOrder.Token1Id + " for " + this.quickOrder.Token2Id;
        this.quickOrder.OrderPrice = this.TokenExchangeBid;
        this.quickOrder.Token2Amount = this.quickOrder.Token1Amount * this.quickOrder.OrderPrice;
    }
}

