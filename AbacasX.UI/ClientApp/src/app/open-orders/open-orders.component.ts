import { Component, OnInit } from '@angular/core';
import { DataService } from '../../core/data.service';
import { IOrder, BuySellTypeEnum, OrderPriceTermsEnum, OrderTypeEnum} from '../../shared/interfaces';
import { openOrders } from './open-orders';


@Component({
    selector: 'open-orders',
    templateUrl: './open-orders.component.html'
})
export class OpenOrdersComponent implements OnInit {

    title: string = "";
    public orders: any[] = openOrders;
    BuySellTypeEnum = BuySellTypeEnum;
    OrderPriceTermsEnum = OrderPriceTermsEnum;
    OrderTypeEnum = OrderTypeEnum;


    constructor(private dataService: DataService) { }

    ngOnInit(): void {
        this.title = "Open Orders";
        this.getOrders();
    }

    getOrders() {
        this.dataService.getOrders()
            .subscribe((orders: IOrder[]) => {
                this.orders = orders;
            },
                (err: any) => console.log(err),
                () => console.log('getOrders() retrieved orders'));
    }
}
