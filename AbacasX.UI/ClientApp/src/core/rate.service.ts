import { EventEmitter, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, IStreamResult } from '@aspnet/signalr'
import { Observable } from 'rxjs/Observable';
import { TokenPairRate } from '../shared/interfaces';

@Injectable()
export class rateSignalRService {

  connectionEstablished = new EventEmitter<Boolean>();
  private connectionIsEstablished = false;
  private _rateHubConnection: HubConnection;
  public  tokenPairRateUpdated = new EventEmitter<TokenPairRate>();

  constructor() {
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  private createConnection() {
    this._rateHubConnection = new HubConnectionBuilder()
      .withUrl('/rate')
      //.configureLogging(signalR.LogLevel.Debug)
      .build();
  }

  private startConnection(): void {
    this._rateHubConnection
      .start()
      .then(() => {
        this.connectionIsEstablished = true;
        console.log('rate connection started');
        this.connectionEstablished.emit(true);
      }).catch(err => {
        console.log("Timeout Error {0}", err);
        setTimeout(() => { this.startConnection() }, 5000);
      });
  }

  public unsubscribeAllRates(): void {
    this._rateHubConnection.invoke("unsubscribeToAllRateUpdates").then(
      () => {
        console.log("All Rates have been unsubscribed");
      });
  }

  public clientUnsubscribeAllRates(): void {
    this._rateHubConnection.invoke("clientUnsubscribeAllRateUpdates").then(
      () => {
        console.log("All Client Rates have been unsubscribed");
      });
  }
  
  private registerOnServerEvents(): void
  {

    this._rateHubConnection.on("broadcastTokenPairRateUpdate", (rateUpdate) => {
      this.tokenPairRateUpdated.emit(rateUpdate);
    });


    this._rateHubConnection.onclose(async () => {

      console.log("Connection closed... restarting");
      await this.startConnection();
    });

  }

  public getTokenList(): Promise<any> {
    return this._rateHubConnection.invoke("getTokenList");
  }

  public subscribeToOneTokenPairRateUpdate(token1Id : string, token2Id : string): void {
    this._rateHubConnection.invoke("SubscribeToOneTokenPairRate", token1Id, token2Id)
      .catch(err => {
        console.log("Error subscribing to token pair rates {0}", err.toString);
      });
  }

  public clientSubscribeTokenPairRate(token1Id: string, token2Id: string): void {
    this._rateHubConnection.invoke("ClientSubscribeTokenPairRate", token1Id, token2Id)
      .catch(err => {
        console.log("Error subscribing to token pair rates {0}", err.toString);
      });
  }

  public startStreamingTokenPairRates(): IStreamResult<any> {
    return this._rateHubConnection.stream("StreamTokenPairRates");
  }

  public getTokenDetail(tokenId: string): Promise<any> {
    return this._rateHubConnection.invoke("getTokenDetail", tokenId).catch(err => {
      console.log("Error on getTokenDetail {0}", err.toString);
    });
  }

  public getAssetList(): Promise<any> {
    return this._rateHubConnection.invoke("getAssetList");
  }

  public getTokenRateList(): Promise<any> {
    return this._rateHubConnection.invoke("getTokenRateList").catch(err => {
      console.log("Error on Token Rate List {0}", err.toString);
    });
  }

  public getTokenRate(tokenId: string): Promise<any> {
    return this._rateHubConnection.invoke("getTokenRate", tokenId).catch(err => {
      console.log("Error on getTokenRate {0}", err.toString);
    });
  }

  public getTokenPairRate(token1Id: string, token2Id: string): Promise<any> {

    try {
      return this._rateHubConnection.invoke("getTokenPairRate", token1Id, token2Id);
    }
    catch (err) {
      console.log("Error on getTokenPairRate {0}", err);
    }
  }

  public isRateFeedOn(): Promise<any> {
    try {
      return this._rateHubConnection.invoke("isRateFeedOn");
    }
    catch (err) {
      console.log("Error determining rate feed On/Off status {0}", err);
    }
  }

  public toggleRateFeed(): Promise<any> {
    try {
      return this._rateHubConnection.invoke("toggleRateFeed");
    }
    catch (err) {
      console.log("Error toggling rate feed On/Off {0}", err);
    }
  }


}
