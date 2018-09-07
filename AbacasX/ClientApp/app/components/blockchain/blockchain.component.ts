import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { transactions } from './blockchain.transactions';


@Component({
    selector: 'blockchain',
    templateUrl: './blockchain.component.html'
})
export class BlockchainComponent {
    public gridData: any[] = transactions;
}
