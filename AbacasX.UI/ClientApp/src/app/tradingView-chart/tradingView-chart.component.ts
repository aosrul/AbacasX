import { Component, OnInit } from '@angular/core';

declare var TradingView: any;

@Component({
  selector: 'tradingview-chart',
  templateUrl: './tradingView-chart.component.html',
})
export class TradingViewChartComponent implements OnInit {

  ngOnInit() {

    new TradingView.widget(
      {
        "height": "330",
        "width": "100%",
        "symbol": "NASDAQ:AAPL",
        "interval": "D",
        "timezone": "Etc/UTC",
        "theme": "Light",
        "style": "1",
        "locale": "en",
        "toolbar_bg": "#f1f3f6",
        "enable_publishing": false,
        "allow_symbol_change": true,
        "container_id": "tradingview_chart"
      }
    );
  }
}
