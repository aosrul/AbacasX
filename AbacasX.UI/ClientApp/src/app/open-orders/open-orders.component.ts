import { Component, OnInit } from '@angular/core';
import { DataService } from '../../core/data.service';
import { IOrder, BuySellTypeEnum, OrderPriceTermsEnum, OrderTypeEnum, OrderLegFillStatusEnum } from '../../shared/interfaces';
import { openOrders } from './open-orders';
import { LoginService } from '../../core/login.service';


@Component({
  selector: 'open-orders',
  templateUrl: './open-orders.component.html'
})
export class OpenOrdersComponent implements OnInit {

  title: string = "";
  public orders: any[] = [];
  BuySellTypeEnum = BuySellTypeEnum;
  OrderPriceTermsEnum = OrderPriceTermsEnum;
  OrderTypeEnum = OrderTypeEnum;
  OrderLegFillStatusEnum = OrderLegFillStatusEnum;


  constructor(private dataService: DataService, private loginService: LoginService) { }

  ngOnInit(): void {
    this.title = "Open Orders";
    this.getOrders();
  }

  getOrders() {
    this.dataService.getClientOrders(this.loginService.userId)
      .subscribe((orders: IOrder[]) => {
        this.orders = orders;
      },
        (err: any) => console.log(err),
        () => console.log('getOrders() retrieved orders'));
  }
}
