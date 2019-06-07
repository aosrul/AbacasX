import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { DataService } from '../../core/data.service';
import { TransferTypeEnum, TransferStatusEnum, IAssetTransfer } from '../../shared/interfaces';
import { LoginService } from '../../core/login.service';

@Component({
  selector: 'account-transfer-activity',
  templateUrl: './account-transfer-activity.component.html'
})
export class AccountTransferActivityComponent implements OnInit {
  public clientTransfers: any[];
  title: string = "";

  TransferTypeEnum = TransferTypeEnum;
  TransferStatusEnum = TransferStatusEnum;
  

  constructor(private dataService: DataService, private loginService : LoginService) { }

  ngOnInit(): void {
    this.getClientTransferActivity();
  }

  getClientTransferActivity() {

    this.dataService.getClientTransferActivity(this.loginService.userId)
      .subscribe((clientTransfers: IAssetTransfer[]) => {
        this.clientTransfers = clientTransfers;
      },
        (err: any) => console.log(err),
        () => console.log('getClientTransferActivity() has retrieved the activity'));

  }
}
