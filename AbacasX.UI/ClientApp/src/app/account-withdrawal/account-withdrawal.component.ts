import { Component, OnInit } from '@angular/core';
import { IAssetTransfer, TransferStatusEnum, TransferTypeEnum, IWithdrawal } from '../../shared/interfaces';
import { Router } from '@angular/router';
import { DataService } from '../../core/data.service';

@Component({
  selector: 'account-withdrawal',
  templateUrl: './account-withdrawal.component.html',
  styleUrls: ['./account-withdrawal.component.css']
})
export class AccountWithdrawalComponent implements OnInit {

  changeLog: string[] = [];
  errorMessage: string = "";

  withdrawalRequest: IWithdrawal = {
    tokenId: "@USD",
    amount: 0,
    referenceId: null,
    clientId: 0
  };


  constructor(private router: Router,
    private dataService: DataService) { }


  ngOnInit() {
    this.getNewGuid();
  }

  getNewGuid() {

    this.dataService.getNewGuid().
      subscribe((guid: any) => {
        if (guid) {
          this.withdrawalRequest.referenceId = guid;
        }
        else {
          this.errorMessage = "Unable to access new reference ID";
        }
      },
        (err: any) => console.log(err));
  }

  submit() {
    this.dataService.addWithdrawal(this.withdrawalRequest)
      .subscribe((withdrawal: IWithdrawal) => {
        if (withdrawal) {
        }
        else {
          this.errorMessage = 'Unable to add Withdrawal Request';
        }
      },
        (err: any) => console.log(err));

    this.withdrawalRequest.amount = 0;
    this.getNewGuid();
  }


}
