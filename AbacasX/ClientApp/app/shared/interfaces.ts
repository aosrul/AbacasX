import { ModuleWithProviders } from '@angular/core';

export interface ILineData {
    data: number[];
    label: string;
}

export interface IOrderRates
{

    Token1BidPrice: number;
    Token1AskPrice: number;

    Token2BidPrice: number;
    Token2AskPrice: number;

    Token1PriceCurrency: string;
    Token2PriceCurrency: string;

    Token1PriceTerms: OrderPriceTerms;
    Token2PriceTerms: OrderPriceTerms;

    Token1PriceCurrencyBid: number;
    Token1PriceCurrencyAsk: number;

    Token2PriceCurrencyBid: number;
    Token2PriceCurrencyAsk: number;
}

export interface IQuickOrder {
    orderId?: number;

    ClientId: number;
    ClientAccountId: number;

    BuySellType: BuySellTypeEnum;
    Token1Id: string;
    Token1Amount: number;

    Token2Id: string;
    Token2Amount: number;
    OrderPrice: number;
    OrderPriceTerms: OrderPriceTermsEnum;

    OrderType: OrderTypeEnum;
}

export enum OrderPriceCurrencyTerms {
    CurrencyPerToken,
    TokenPerCurrency
}

export enum OrderPriceTerms {
    Token1PerToken2,
    Token2PerToken1
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