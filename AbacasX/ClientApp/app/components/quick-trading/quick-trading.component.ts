import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IQuickOrder, BuySellTypeEnum, OrderPriceTermsEnum, OrderTypeEnum } from '../../shared/interfaces';
import { validateConfig } from '@angular/router/src/config';

@Component({
    selector: 'quick-trading',
    templateUrl: './quick-trading.component.html',
    styleUrls: ['./quick-trading.component.css']

})
export class QuickTradingComponent implements OnInit {

    //quickOrderForm: FormGroup;
    //quickOrder: IQuickOrder = {
    //    ClientId: 0,
    //    ClientAccountId: 0,
    //    BuySellType: BuySellTypeEnum.Buy,
    //    Token1Id: '',
    //    Token1Amount: 0,
    //    Token2Id: '',
    //    OrderPrice: 0,
    //    OrderPriceTerms: OrderPriceTermsEnum.Token2PerToken1,
    //    OrderType: OrderTypeEnum.Market,
    //};

    errorMessage: string;
    deleteMessageEnabled: boolean;
    operationText: string = 'Insert';

    //constructor(private formBuilder: FormBuilder) { }

    ngOnInit() {
        //this.buildForm();
    }

    //buildForm() {
    //    this.quickOrderForm = this.formBuilder.group({
    //        ClientId: [this.quickOrder.ClientId, Validators.required],
    //        ClientAccountId: [this.quickOrder.ClientAccountId, Validators.required],
    //        BuySellType: [this.quickOrder.BuySellType, Validators.required],
    //        Token1Id: [this.quickOrder.Token1Id, Validators.required],
    //        Token1Amount: [this.quickOrder.Token1Amount, Validators.required],
    //        Token2Id: [this.quickOrder.Token2Id, Validators.required],
    //        OrderType: [this.quickOrder.OrderType, Validators.required],
    //        OrderPrice: [this.quickOrder.OrderPrice, Validators.required],
    //        OrderPriceTerms: [this.quickOrder.OrderPriceTerms, Validators.required],
    //    });
    //}
}

