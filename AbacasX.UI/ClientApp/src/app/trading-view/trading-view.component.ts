import { Component, OnInit } from '@angular/core';

declare var TradingView: any;

@Component({
  selector: 'tradingview-chart',
  templateUrl: './trading-view.component.html',
})
export class TradingViewChartComponent implements OnInit {

  ngOnInit() {

    new TradingView.widget(
      {
        "height": 450,
        "width": 980,
        "symbol": "NASDAQ:AAPL",
        "interval": "D",
        "timezone": "Etc/UTC",
        "theme": "Light",
        "style": "1",
        "locale": "en",
        "toolbar_bg": "#f1f3f6",
        "enable_publishing": false,
        "allow_symbol_change": true,
        "container_id": "tradingview_d3500"
      }
    );
  }
}
