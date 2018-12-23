import { Component, OnInit } from '@angular/core';
import { custodianAssetDepositPending } from './CustodianAssetDepositPending';

@Component({
  selector: 'custodian-deposit-pending',
  templateUrl: './custodian-deposit-pending.component.html',
  styleUrls: ['./custodian-deposit-pending.component.css']
})
export class CustodianDepositPendingComponent implements OnInit {

  public custodianPendingDeposits: any[] = custodianAssetDepositPending;
  title: string = "";

  constructor() { }

  ngOnInit() {
    this.title = "Pending Deposits"
  }

}

