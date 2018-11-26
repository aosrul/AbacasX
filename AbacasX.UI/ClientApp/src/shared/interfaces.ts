import { ModuleWithProviders } from '@angular/core';

export class TradingRates {
  Token1Id: string;
  Token2Id: string;

  Token1Bid: number;
  Token1Ask: number;

  Token2Bid: number;
  Token2Ask: number;

  Token1PriceCurrency: string;
  Token2PriceCurrency: string;

  Token1PriceCurrencyBid: number;
  Token1PriceCurrencyAsk: number;
  Token2PriceCurrencyBid: number;
  Token2PriceCurrencyAsk: number;


  TokenExchangeBid: number;
  TokenExchangeAsk: number;

  TokenExchangeFXBid: number;
  TokenExchangeFXAsk: number;

  IsCrossCurrency: boolean;
}

export class LoginResults {
  public successfulLogin: boolean;
  public userName: string;
  public userRole: RoleTypeEnum;
}

export interface ILineData {
  data: number[];
  label: string;
}

export interface IClientBlockChainTransaction {
  blockNumber: number;
  date: string;
  orderId: number;
  payReceive: string;
  tokenId: string;
  tokenAmount: number;
  address: string;
  transactionHash: string;
}

export interface IClientPosition {
  tokenId: string;
  tokenAmount: number;
  tokenRateIn: string;
  tokenRate: number;
  tokenValue: number;
}

export interface IOrderRates {

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
  OrderId?: number;

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

export interface IOrder {
  orderId?: number;

  clientId: number;
  clientAccountId: number;

  buySellType: BuySellTypeEnum;
  token1Id: string;
  token1Amount: number;

  token2Id: string;
  token2Amount: number;
  orderPrice: number;
  orderPriceTerms: OrderPriceTermsEnum;

  orderType: OrderTypeEnum;
  orderStatus: OrderStatusEnum;
  priceFilled: number;
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
  Limit,
  IfDone,
  OCO,
  IfDoneOCO
}

export enum OrderPriceTermsEnum {
  Token1PerToken2,
  Token2PerToken1
}

export enum BuySellTypeEnum {
  Buy,
  Sell
}

export enum OrderStatusEnum {
  Contingent,  // Child Order awaiting outcome of Parent Order
  Pending,     // Pending Acceptance by Trading Desk
  Active,      // Active Order
  Suspended,
  Canceled,
  Filled,
  Expired
}

export enum RoleTypeEnum {
  Investor,
  Broker,
  Ops,
  Admin,
  Guest
}
