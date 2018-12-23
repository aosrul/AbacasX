import { Component, OnInit } from '@angular/core';
import { custodianAssetDepositHistory } from './custodianAssetDepositHistory';

@Component({
  selector: 'custodian-deposit-history',
  templateUrl: './custodian-deposit-history.component.html',
  styleUrls: ['./custodian-deposit-history.component.css']
})
export class CustodianDepositHistoryComponent implements OnInit {

  public custodianDepositHistory: any[] = custodianAssetDepositHistory;
  title: string = "";

  constructor() { }

  ngOnInit() {
    this.title = "Deposit History"
  }
}
