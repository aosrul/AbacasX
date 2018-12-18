import { Component, OnInit } from "@angular/core";
import { stockSignalRService } from "../../core/stock.service";


@Component({
  templateUrl: './stock.component.html',
  selector: "stocks",
  providers: [stockSignalRService]
})

export class StockComponent {


  stocks = [];
  marketStatus: string;
  rateFeedStatus: string;

  constructor(private stockService: stockSignalRService) {
    this.stocks = [];
    this.marketStatus = 'closed';
    this.rateFeedStatus = 'Off';

    //subscribe for connection eastablish
    //fetch the stocks details
    console.log("Stock Component Constructor");


    stockService.connectionEstablished.subscribe(() => {
      console.log("Stock Component Subscription to Connection");

      stockService.getAllStocks().then((data) => {
        console.log(data);
        this.stocks = data;
      }).catch((reason: any) => { console.log(reason); });

      stockService.getMarketState().then((data) => {
        console.log(data);
        if (data == "Open") {
          this.marketStatus = 'open';
          this.rateFeedStatus = 'On';
          this.startStreaming();
        }
      });
    });


    //subscribe for market open
    stockService.marketOpened.subscribe(() => {
      this.rateFeedStatus = "On";
      this.marketStatus = 'open';
      this.startStreaming();
    });

    //subscribe for market close
    stockService.marketClosed.subscribe(() => {
      this.marketStatus = 'closed';
      this.rateFeedStatus = 'Off';
    });

  }

  openMarketClicked() {
    this.stockService.openMarket();
  }

  startStreaming() {
    this.stockService.startStreaming().subscribe({
      next: (data) => {
        this.displayStock(data);
      },
      error: function (err) {
        console.log('Error:' + err);
      },
      complete: function () {
        console.log('completed');
      }
    });
  }

  closeMarketClicked() {
    this.stockService.CloseMarket();
  }

  resetClicked() {
    this.stockService.ResetMarket();
  }

  displayStock(stock) {
    console.log("stock updated:" + stock.symbol);
    for (let i in this.stocks) {
      //console.log(i);
      if (this.stocks[i].symbol == stock.symbol) {
        this.stocks[i] = stock;
      }
    }
  }

}
