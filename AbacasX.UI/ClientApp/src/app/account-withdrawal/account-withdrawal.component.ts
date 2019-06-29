import { Component, OnInit, SimpleChange, OnChanges } from '@angular/core';
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
  TokenBalanceAvailable: number = 0;
  public IsTokenBalanceExceeded: boolean = false;

  public withdrawalRequest: IWithdrawal = {
    tokenId: "@USD",
    amount: 0,
    referenceId: null,
    clientId: 0
  };


  constructor(private router: Router,
    private dataService: DataService, private loginService: LoginService, private rateService: rateSignalRService) {

    this.withdrawalRequest.clientId = this.loginService.userId;

    rateService.connectionEstablished.subscribe(() => {

      this.IsSubscribed = true;

      console.log("Account Withdrawal Subscription Connection");

      rateService.getTokenList().then((data) => {
        console.log(data);
        
        this.tokenList = data;
        this.tokenList.sort();
        
      }).catch((reason: any) => { console.log(reason); });
    });
  }

  ngOnInit() {
    this.getNewGuid();
    this.CheckTokenBalance(this.withdrawalRequest.tokenId);
    this.IsTokenBalanceExceeded = true;
  }

  
  tokenChange(selectedTokenId: string) {
    this.withdrawalRequest.tokenId = selectedTokenId;
    this.CheckTokenBalance(selectedTokenId);
  }

  updateTokenAmount(tokenAmount: string) {
    this.withdrawalRequest.amount = Number(tokenAmount);
    this.CheckTokenBalance(this.withdrawalRequest.tokenId);
  }

  
  CheckTokenBalance(tokenId: string) {

    console.log("Checking token balance for {0}", tokenId);

    this.dataService.getClientTokenBalance(this.withdrawalRequest.clientId, tokenId)
      .subscribe((tokenBalance: number) => {

        if (tokenBalance)
        {
          this.TokenBalanceAvailable = tokenBalance;

          if (this.withdrawalRequest.amount > this.TokenBalanceAvailable)
            this.IsTokenBalanceExceeded = true;
          else if (this.withdrawalRequest.amount == 0)
            this.IsTokenBalanceExceeded = true;
          else
            this.IsTokenBalanceExceeded = false;

          console.log("Token Balance is", this.TokenBalanceAvailable);

        }
        else {
          this.errorMessage = 'Unable to get client token balance';
          this.IsTokenBalanceExceeded = true;
        }
      },
      (err: any) => console.log(err));

    if (this.withdrawalRequest.amount == 0)
      this.IsTokenBalanceExceeded = true;
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
