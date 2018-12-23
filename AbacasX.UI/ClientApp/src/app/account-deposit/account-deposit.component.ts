import { Component, OnInit } from '@angular/core';
import { IAssetTransfer, TransferStatusEnum, TransferTypeEnum } from '../../shared/interfaces';

@Component({
  selector: 'account-deposit',
  templateUrl: './account-deposit.component.html',
  styleUrls: ['./account-deposit.component.css']
})
export class AccountDepositComponent implements OnInit {

  public IsDepositRequest: boolean = true;
  changeLog: string[] = [];
  errorMessage: string = "";

  transferRequest: IAssetTransfer = {

    assetTransferId: null,
    assetAccountId: 0,
    custodianId: 0,
    tokenConversionId: null,
    assetId: "USD",
    amount: 0,
    transferStatus: TransferStatusEnum.Requested,
    transferType: TransferTypeEnum.Deposit,
    forAccountOf: "TradezDigital",
    referenceCode: "",
  };


  constructor() { }

  ngOnInit() {
  }

  updateTransferRequestAmount(amount: string) {
    this.transferRequest.amount = Number(amount);
  }

  AssetDepositClicked() {
    this.IsDepositRequest = true;
    this.transferRequest.transferType = TransferTypeEnum.Deposit;
  }

  AssetWithdrawalClicked() {
    this.IsDepositRequest = false;
    this.transferRequest.transferType = TransferTypeEnum.Withdrawal;
  }
}
