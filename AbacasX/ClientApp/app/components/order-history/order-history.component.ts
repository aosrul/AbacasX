import { Component, OnInit } from '@angular/core';
import { DataService } from '../../core/data.service';
import { IOrder, BuySellTypeEnum, OrderPriceTermsEnum, OrderTypeEnum, OrderStatusEnum } from '../../shared/interfaces';
import { orderHistory } from './order-history'


@Component({
    selector: 'order-history',
    templateUrl: './order-history.component.html'
})
export class OrderHistoryComponent implements OnInit {

    title: string = "";
    public orders: any[] = orderHistory;

    BuySellTypeEnum = BuySellTypeEnum;
    OrderPriceTermsEnum = OrderPriceTermsEnum;
    OrderTypeEnum = OrderTypeEnum;
    OrderStatusEnum = OrderStatusEnum;

    constructor(private dataService: DataService) { }

    ngOnInit(): void {
        this.title = "Historical Orders";
        this.getHistoricalOrders();
    }

    getHistoricalOrders() {
        this.dataService.getHistoricalOrders()
            .subscribe((orders: IOrder[]) => {
                this.orders = orders;
            },
                (err: any) => console.log(err),
                () => console.log('getHistoricalOrders() retrieved orders'));
    }
}
