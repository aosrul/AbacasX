import { Component, OnInit, AfterViewInit, ElementRef, Input, Renderer2, OnChanges, SimpleChange } from '@angular/core';


@Component({
  selector: 'tradingview-minichart',
  templateUrl: './tradingView-mini-chart.component.html'
})
export class TradingViewMiniChartComponent implements OnInit, AfterViewInit, OnChanges {
  @Input() selectedAsset: string = "";
  changeLog: string[] = [];
  s: any = null;
  p: any = null;

  constructor(private elementRef: ElementRef, private renderer: Renderer2) { }

  ngOnInit() {
  }

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

      if (propName === "selectedAsset") {
        this.changeLog.push(log.join(', '));
        this.selectedAssetChanged(changedProp.currentValue);
      }
    }
  }

  selectedAssetChanged(currentValue: string) {

    if (this.s != null) {
      this.renderer.removeChild(this.elementRef.nativeElement, this.p);

      this.p = this.renderer.createElement('div');
      this.s = this.renderer.createElement("script");

      this.s.id = "MiniChart";
      this.s.type = 'text/javascript';
      this.s.src = "https://s3.tradingview.com/external-embedding/embed-widget-mini-symbol-overview.js";

      this.s.text = `{     "symbol": "NASDAQ:${this.selectedAsset}",
                        "width": "100\%",
                        "height": "300",
                        "locale": "en",
                        "dateRange": "1y",
                        "colorTheme": "light",
                        "trendLineColor": "#37a6ef",
                        "underLineColor": "#e3f2fd",
                        "isTransparent": false,
                        "autosize": true,
                        "largeChartUrl": ""
                    }`;

      this.renderer.appendChild(this.p, this.s);
      this.renderer.appendChild(this.elementRef.nativeElement, this.p);
    }
  }



  ngAfterViewInit() {

    this.p = this.renderer.createElement('div');
    this.s = this.renderer.createElement("script");

    this.s.id = "MiniChart";
    this.s.type = 'text/javascript';
    this.s.src = "https://s3.tradingview.com/external-embedding/embed-widget-mini-symbol-overview.js";

    this.s.text = `{     "symbol": "NASDAQ:${this.selectedAsset}",
                        "width": "100\%",
                        "height": "300",
                        "locale": "en",
                        "dateRange": "1y",
                        "colorTheme": "light",
                        "trendLineColor": "#37a6ef",
                        "underLineColor": "#e3f2fd",
                        "isTransparent": false,
                        "autosize": true,
                        "largeChartUrl": ""
                    }`;

    this.renderer.appendChild(this.p, this.s);
    this.renderer.appendChild(this.elementRef.nativeElement, this.p);
  }
}
