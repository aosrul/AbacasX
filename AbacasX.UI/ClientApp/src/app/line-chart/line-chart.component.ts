import { Component, OnInit, Input, OnChanges, SimpleChange } from '@angular/core';

@Component({
  selector: 'line-chart',
  templateUrl: './line-chart.component.html'
})
export class LineChartComponent implements OnInit, OnChanges {
  @Input()
  selectedAssetPair: string = "";
  changeLog: string[] = [];

  selectedAsset1: string = "AAPL";
  selectedAsset2: string = "GOOG";

  private token1Id: string = "@AAPL";
  private token2Id: string = "@GOOG";

  constructor() {
  }

  ngOnInit() {
  };

  ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
    let log: string[] = [];

    for (let propName in changes) {
      let changedProp = changes[propName];
      let to = JSON.stringify(changedProp.currentValue);

      if (changedProp.isFirstChange()) {
        log.push(`Initial value of ${propName} set to ${to}`);
      } else {
        let from = JSON.stringify(changedProp.previousValue);
        log.push(`${propName} changed from ${from} to ${to}`);
      }

      if (propName === "selectedAssetPair")
        this.selectedAssetPairChanged(changedProp.currentValue);
    }

    this.changeLog.push(log.join(', '));
  }


  selectedAssetPairChanged(newAssetPair: string) {

    // Locate the assets which start with an @
    var results = newAssetPair.match(/@\S*/g);

    console.log("Asset 1 {0}", results[0]);
    console.log("Asset 2 {0}", results[1]);
    this.token1Id = results[0];
    this.token2Id = results[1];

    switch (results[0]) {
      case "@AAPL":
        this.selectedAsset1 = "AAPL";
        break;
      case "@GOOG":
        this.selectedAsset1 = "GOOG";
        break;
      case "@MSFT":
        this.selectedAsset1 = "MSFT";
        break;
      case "@GOLD":
        this.selectedAsset1 = "GOLD";
        break;
      case "@BTC":
        this.selectedAsset1 = "BTC";
        break;
      case "@ETH":
        this.selectedAsset1 = "ETH";
        break;
      case "@BNP":
        this.selectedAsset1 = "BNP";
        break;
      default:
        this.selectedAsset1 = "AAPL";
        break;
    }

    switch (results[1]) {
      case "@AAPL":
        this.selectedAsset2 = "AAPL";
        break;
      case "@GOOG":
        this.selectedAsset2 = "GOOG";
        break;
      case "@MSFT":
        this.selectedAsset2 = "MSFT";
        break;
      case "@GOLD":
        this.selectedAsset2 = "GOLD";
        break;
      case "@BTC":
        this.selectedAsset2 = "BTC";
        break;
      case "@ETH":
        this.selectedAsset2 = "ETH";
        break;
      case "@BNP":
        this.selectedAsset2 = "BNP";
        break;
      default:
        this.selectedAsset2 = "AAPL";
        break;
    }



  //  if (this.selectedAssetPair == "@AAPL - @GOOG") {
  //    this.token1Id = "@AAPL";
  //    this.token2Id = "@GOOG";
  //    this.selectedAsset1 = "AAPL";
  //    this.selectedAsset2 = "GOOG";
      
  //  }

  //  if (this.selectedAssetPair == "@AAPL - @GOLD") {
  //    this.token1Id = "@AAPL";
  //    this.token2Id = "@GOLD";
  //    this.selectedAsset1 = "AAPL";
  //    this.selectedAsset2 = "GOLD";

  //  }

  //  if (this.selectedAssetPair == "@AAPL - @MSFT") {
  //    this.token1Id = "@AAPL";
  //    this.token2Id = "@MSFT";

  //    this.selectedAsset1 = "AAPL";
  //    this.selectedAsset2 = "MSFT";
  //  }

  //  if (this.selectedAssetPair == "@MSFT - @BTC") {
  //    this.token1Id = "@MSFT";
  //    this.token2Id = "@BTC";

  //    this.selectedAsset1 = "MSFT";
  //    this.selectedAsset2 = "BTC";
  //  }

  //  if (this.selectedAssetPair == "@AAPL - @BTC") {
  //    this.token1Id = "@AAPL";
  //    this.token2Id = "@BTC";
  //    this.selectedAsset1 = "AAPL";
  //    this.selectedAsset2 = "BTC";

  //  }

  //  if (this.selectedAssetPair == "@AAPL - @USD") {
  //    this.token1Id = "@AAPL";
  //    this.selectedAsset2 = "USD";

  //  }
  }
}
