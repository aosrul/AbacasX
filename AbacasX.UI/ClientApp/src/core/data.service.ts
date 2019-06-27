import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';

import { IOrder, BuySellTypeEnum, OrderPriceTermsEnum, OrderTypeEnum, IClientPosition, IClientBlockChainTransaction, IDeposit, IWithdrawal, IAssetTransfer } from '../shared/interfaces';

//Grab everything with import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';


@Injectable()
export class DataService {

  //If you're on Angular 4.3 or higher you can use HttpClientModule. See data.service.ts.httpclient

  baseOrdersUrl: string = '/api/orders';

  constructor(private http: Http) {
  }
  getClientBlockChainTransactions(clientId: number): Observable<IClientBlockChainTransaction[]> {
    return this.http.get(this.baseOrdersUrl + '/' + 'clientBlockChainTransactions' + '?clientId=' + clientId.toString())
      .map((res: Response) => {
        let clientBlockChainTransactions = res.json();
        return clientBlockChainTransactions;
      })
      .catch(this.handleError);
  }


  getClientPositions(clientId: number): Observable<IClientPosition[]> {
    return this.http.get(this.baseOrdersUrl + '/' + 'clientPosition' + '?clientId=' + clientId.toString())
      .map((res: Response) => {
        let clientPositions = res.json();
        return clientPositions;
      })
      .catch(this.handleError);
  }

  getClientTokenBalance(clientId: number, tokenId: string) : Observable<number> {
    return this.http.get(this.baseOrdersUrl + '/' + 'clientTokenBalance' + '?clientId=' + clientId.toString() + '&tokenId=' + tokenId.toString())
      .map((res: Response) => {
        const data = res.json();
        return data.tokenBalance;
      })
      .catch(this.handleError);
  }


  getOrders(): Observable<IOrder[]> {
    return this.http.get(this.baseOrdersUrl)
      .map((res: Response) => {
        let orders = res.json();
        return orders;
      })
      .catch(this.handleError);
  }

  getClientOrders(clientId: number): Observable<IOrder[]> {
    return this.http.get(this.baseOrdersUrl + '/' + 'clientOrders' + '?clientId=' + clientId.toString())
      .map((res: Response) => {
        let orders = res.json();
        return orders;
      })
      .catch(this.handleError);
  }

  getHistoricalOrders(clientId: number): Observable<IOrder[]> {
    return this.http.get(this.baseOrdersUrl + '/' + 'history' + '?clientId=' + clientId.toString())
      .map((res: Response) => {
        let orders = res.json();
        return orders;
      })
      .catch(this.handleError);
  }


  getOrder(id: number): Observable<IOrder> {
    return this.http.get(this.baseOrdersUrl + '/' + id.toString())
      .map((res: Response) => res.json())
      .catch(this.handleError);
  }

  addOrder(order: IOrder): Observable<IOrder> {
    return this.http.post(this.baseOrdersUrl, order)
      .map((res: Response) => {
        const data = res.json();
        console.log('addOrder status: ' + data.status);
        return data.order;
      })
      .catch(this.handleError);
  }

  addDeposit(deposit: IDeposit): Observable<IDeposit> {
    return this.http.post(this.baseOrdersUrl + '/createDeposit', deposit)
      .map((res: Response) => {
        const data = res.json();
        console.log('createDeposit status: ' + data.status);
        return data.deposit;
      })
      .catch(this.handleError);
  }

  addWithdrawal(withdrawal: IWithdrawal): Observable<IWithdrawal> {
    return this.http.post(this.baseOrdersUrl + '/createWithdrawal' , withdrawal)
      .map((res: Response) => {
        const data = res.json();
        console.log('createWithdrawal status: ' + data.status);
        return data.withdrawal;
      })
      .catch(this.handleError);
  }

  getClientTransferActivity(clientId: number): Observable<IAssetTransfer[]> {
    return this.http.get(this.baseOrdersUrl + '/' + 'clientTransferActivity' + '?clientId=' + clientId.toString())
      .map((res: Response) => {
        let transferActivity = res.json();
        return transferActivity;
      })
      .catch(this.handleError);
  }

  getNewGuid(): Observable<string> {
    return this.http.get(this.baseOrdersUrl + '/getNewGuid')
      .map((res: Response) => {
        const data = res.json();
        return data.guid;
      })
      .catch(this.handleError);
  }
  
  private handleError(error: any) {
    console.error('server error:', error);

    if (error instanceof Response) {
      let errMessage: string | null = '';

      try {
        errMessage = error.json().error;
      } catch (err) {
        errMessage = error.statusText;
      }
      return Observable.throw(errMessage);
      // Use the following instead if using lite-server
      //return Observable.throw(err.text() || 'backend server error');
    }
    return Observable.throw(error || 'ASP.NET Core server error');
  }

  //getCustomersPage(page: number, pageSize: number): Observable<IPagedResults<ICustomer[]>> {
  //    return this.http.get(`${this.baseUrl}/page/${page}/${pageSize}`)
  //        .map((res: Response) => {
  //            const totalRecords = +res.headers.get('x-inlinecount');
  //            let customers = res.json();
  //            this.calculateCustomersOrderTotal(customers);
  //            return {
  //                results: customers,
  //                totalRecords: totalRecords
  //            };
  //        })
  //        .catch(this.handleError);
  //}

  //getCustomer(id: string): Observable<ICustomer> {
  //    return this.http.get(this.baseUrl + '/' + id)
  //        .map((res: Response) => res.json())
  //        .catch(this.handleError);
  //}
}
