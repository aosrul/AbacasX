import { Component, OnInit } from '@angular/core';
import { IAssetTransfer, TransferStatusEnum, TransferTypeEnum, IDeposit } from '../../shared/interfaces';
import { Router } from '@angular/router';
import { DataService } from '../../core/data.service';
import { LoginService } from '../../core/login.service';
import { rateSignalRService } from '../../core/rate.service';

@Component({
  selector: 'account-deposit',
  templateUrl: './account-deposit.component.html',
  styleUrls: ['./account-deposit.component.css'],
  providers: [rateSignalRService]
})
export class AccountDepositComponent implements OnInit {

  changeLog: string[] = [];
  errorMessage: string = "";
  IsSubscribed: boolean = false;
  IsDepositAmountInvalid = true;
  public assetList: any[] = [];

  public depositRequest: IDeposit = {
    assetId: "USD",
    amount: 0,
    referenceId: null,
    clientId: 0
  };


  constructor(private router: Router,
    private dataService: DataService,
    private loginService: LoginService,
    private rateService: rateSignalRService) {

    rateService.connectionEstablished.subscribe(() => {

    this.IsSubscribed = true;

    console.log("Account Deposit Subscription Connection");

    rateService.getAssetList().then((data) => {
      console.log(data);
      this.assetList = data;
      this.assetList.sort();
      }).catch((reason: any) => { console.log(reason); });
    });

  }


  updateDepositAmount(assetAmount: string) {
    this.depositRequest.amount = Number(assetAmount);

    if (this.depositRequest.amount <= 0)
      this.IsDepositAmountInvalid = true;
    else
      this.IsDepositAmountInvalid = false;
  }



  ngOnInit() {
    this.getNewGuid();
  }

  getNewGuid() {

    this.dataService.getNewGuid().
      subscribe((guid: any) => {
        if (guid) {
          this.depositRequest.referenceId = guid;
        }
        else {
          this.errorMessage = "Unable to access new reference ID";
        }
      },
        (err: any) => console.log(err));
  }


  submit() {
    this.depositRequest.clientId = this.loginService.userId;

    this.dataService.addDeposit(this.depositRequest)
      .subscribe((deposit: IDeposit) => {
        if (deposit) {
        }
        else {
          this.errorMessage = 'Unable to add Deposit Request';
        }
      },
        (err: any) => console.log(err));

    this.depositRequest.amount = 0;
    this.getNewGuid();
  }


}
