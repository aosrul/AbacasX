import { ModuleWithProviders } from '@angular/core';

export interface IQuickOrder {
    orderId?: number;

    ClientId: number;
    ClientAccountId: number;

    BuySellType: BuySellTypeEnum;
    Token1Id: string;
    Token1Amount: number;

    Token2Id: string;
    OrderPrice: number;
    OrderPriceTerms: OrderPriceTermsEnum;

    OrderType: OrderTypeEnum;
}

export enum OrderTypeEnum {
    Market,
    Limit
}

export enum OrderPriceTermsEnum {
    Token1PerToken2,
    Token2PerToken1
}

export enum BuySellTypeEnum {
    Buy,
    Sell
}