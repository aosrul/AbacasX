import { Component, Input } from '@angular/core';
import { orderBook } from './order-book';

@Component({
    selector: 'order-book',
    templateUrl: './order-book.component.html'
})
export class OrderBookComponent {
  @Input()
  selectedAssetPair: string = "";
  public bookOrders: any[] = orderBook;
}
