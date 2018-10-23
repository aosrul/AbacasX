import { Component, OnInit } from '@angular/core';
import { DataService } from '../../core/data.service';
import { IOrder, BuySellTypeEnum } from '../../shared/interfaces';


@Component({
    selector: 'orders',
    templateUrl: './orders.component.html'
})
export class OrdersComponent implements OnInit {

    title: string = "";
    orders: IOrder[] = [];
   
    constructor (private dataService: DataService) { }

    ngOnInit(): void {
        this.title = "Orders";
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
