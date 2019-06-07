import { ModuleWithProviders } from '@angular/core';

export enum TokenPairRateTermsEnum {
  Token1PerToken2,
  Token2PerToken1
}

export enum TokenRateTermsEnum {
  TokenPerCurrency,
  CurrencyPerToken
}

export enum AssetRateTermsEnum {
  AssetPerCurrency,
  CurrencyPerAsset
}

export enum CurrencyPairRateTermsEnum {
  Currency1PerCurrency2,
  Currency2PerCurrency1
}

export class TokenPairRate {
  token1Id: string;
  token2Id: string;
  rateTerms: TokenPairRateTermsEnum;
  bidRate: number;
  askRate: number;
  token1BidRate: number;
  token1AskRate: number;
  token1RateTerms: TokenRateTermsEnum;
  token2BidRate: number;
  token2AskRate: number;
  token2RateTerms: TokenRateTermsEnum;
  currency1: string;
  currency1BidRate: number;
  currency1AskRate: number;
  currency1RateTerms: AssetRateTermsEnum;
  currency2: string;
  currency2BidRate: number;
  currency2AskRate: number;
  currency2RateTerms: AssetRateTermsEnum;
  currencyPairBidRate: number;
  currencyPairAskRate: number;
  currencyPairRateTerms: CurrencyPairRateTermsEnum;
}

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
  public userId: number;
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
export interface IDeposit {
  referenceId?: string;
  assetId: string;
  amount: number;
  clientId: number;
}

export interface IWithdrawal {
  referenceId?: string;
  tokenId: string;
  amount: number;
  clientId: number;
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

export interface IAssetTransfer {
  referenceId: string;
  forAccountOf: string;
  clientId: number;
  tokenId: string;
  assetId: string;
  tokenAmount: number;
  assetAmount: number;
  transferStatus: TransferStatusEnum;
  transferType: TransferTypeEnum;
}

export enum TransferStatusEnum {
  Requested,
  InProgress,
  Completed,
  Canceled,
  Failed
}

export enum TransferTypeEnum {
  Deposit,
  Withdrawal
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
  Guest,
  Custodian
}
