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
    if (this.selectedAssetPair == "@AAPL - @GOOG") {
      this.token1Id = "@AAPL";
      this.token2Id = "@GOOG";

    }

    if (this.selectedAssetPair == "@AAPL - @GOLD") {
      this.token1Id = "@AAPL";
      this.token2Id = "@GOLD";

    }

    if (this.selectedAssetPair == "@AAPL - @MSFT") {
      this.token1Id = "@AAPL";
      this.token2Id = "@MSFT";

    }

    if (this.selectedAssetPair == "@AAPL - @BTC") {
      this.token1Id = "@AAPL";
      this.token2Id = "@BTC";


    }

    if (this.selectedAssetPair == "@AAPL - @USD") {
      this.token1Id = "@AAPL";
      this.token2Id = "@USD";

    }
  }
}
