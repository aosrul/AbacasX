import { Component, OnInit } from '@angular/core';
import { custodianAssetBalances } from './custodianAssetBalances';

@Component({
  selector: 'custodian-asset-detail',
  templateUrl: './custodian-asset-detail.component.html',
  styleUrls: ['./custodian-asset-detail.component.css']
})
export class CustodianAssetDetailComponent implements OnInit {

  public custodianBalances: any[] = custodianAssetBalances;
  title: string = "";

  constructor() { }

  ngOnInit() {
    this.title = "Asset Balances";
  }

}
