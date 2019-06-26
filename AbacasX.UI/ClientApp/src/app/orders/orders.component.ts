import { Component, OnInit } from '@angular/core';



@Component({
    selector: 'orders',
    templateUrl: './orders.component.html'
})
export class OrdersComponent implements OnInit {

    title: string = "Orders";
   
    constructor () { }

    ngOnInit(): void {
        this.title = "Orders";
    }
}
