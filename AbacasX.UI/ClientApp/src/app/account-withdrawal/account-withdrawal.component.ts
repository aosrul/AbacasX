import { Component, OnInit } from '@angular/core';
import { IAssetTransfer, TransferStatusEnum, TransferTypeEnum, IWithdrawal } from '../../shared/interfaces';
import { Router } from '@angular/router';
import { DataService } from '../../core/data.service';
import { LoginService } from '../../core/login.service';
import { rateSignalRService } from '../../core/rate.service';

@Component({
  selector: 'account-withdrawal',
  templateUrl: './account-withdrawal.component.html',
  styleUrls: ['./account-withdrawal.component.css'],
  providers: [rateSignalRService]
})
export class AccountWithdrawalComponent implements OnInit {

  changeLog: string[] = [];
  errorMessage: string = "";
  IsSubscribed: boolean = false;
  public tokenList: any[] = [];

  public withdrawalRequest: IWithdrawal = {
    tokenId: "@USD",
    amount: 0,
    referenceId: null,
    clientId: 0
  };


  constructor(private router: Router,
    private dataService: DataService, private loginService: LoginService, private rateService: rateSignalRService) {

    rateService.connectionEstablished.subscribe(() => {

      this.IsSubscribed = true;

      console.log("Account Withdrawal Subscription Connection");

      rateService.getTokenList().then((data) => {
        console.log(data);
        this.tokenList = data;
      }).catch((reason: any) => { console.log(reason); });
    });
  }

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
    this.withdrawalRequest.clientId = this.loginService.userId;

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
