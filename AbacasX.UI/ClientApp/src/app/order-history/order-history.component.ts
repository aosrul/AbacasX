import { Component, OnInit } from '@angular/core';
import { DataService } from '../../core/data.service';
import { IOrder, BuySellTypeEnum, OrderPriceTermsEnum, OrderTypeEnum, OrderStatusEnum } from '../../shared/interfaces';
import { orderHistory } from './order-history'
import { LoginService } from '../../core/login.service';


@Component({
  selector: 'order-history',
  templateUrl: './order-history.component.html'
})
export class OrderHistoryComponent implements OnInit {

  title: string = "";
  public orders: any[] = [];

  BuySellTypeEnum = BuySellTypeEnum;
  OrderPriceTermsEnum = OrderPriceTermsEnum;
  OrderTypeEnum = OrderTypeEnum;
  OrderStatusEnum = OrderStatusEnum;

  constructor(private dataService: DataService, private loginService: LoginService) { }

  ngOnInit(): void {
    this.title = "Historical Orders";
    this.getHistoricalOrders();
  }

  getHistoricalOrders() {
    this.dataService.getHistoricalOrders(this.loginService.userId)
      .subscribe((orders: IOrder[]) => {
        this.orders = orders;
      },
        (err: any) => console.log(err),
        () => console.log('getHistoricalOrders() retrieved orders'));
  }
}
