import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { transactions } from './blockchain.transactions';
import { DataService } from '../../core/data.service';
import { IClientBlockChainTransaction } from '../../shared/interfaces';


@Component({
    selector: 'blockchain-detail',
    templateUrl: './blockchain-detail.component.html'
})
export class BlockchainDetailComponent implements OnInit {
    public clientTransactions: any[] = transactions;
    title: string = "";

    constructor(private dataService: DataService) { }

    ngOnInit(): void {
        this.title = "Blockchain Transactions";
        this.getClientBlockChainTransactions();
    }

    getClientBlockChainTransactions() {
        this.dataService.getClientBlockChainTransactions()
            .subscribe((clientTransactions: IClientBlockChainTransaction[]) => {
                this.clientTransactions = clientTransactions;
            },
                (err: any) => console.log(err),
                () => console.log('getClientBlockChainTransactions() retrieved transactions'));
    
    }
}
