import { Component, OnInit, AfterViewInit, ElementRef, Input, Renderer2, OnChanges, SimpleChange } from '@angular/core';
import { rateSignalRService } from '../../core/rate.service';
import { TokenDetail } from '../../shared/interfaces';


@Component({
  selector: 'tradingview-minichart',
  templateUrl: './tradingView-mini-chart.component.html',
  providers: [rateSignalRService]
})
export class TradingViewMiniChartComponent implements OnInit, AfterViewInit, OnChanges {
  @Input() selectedAsset: string = "";
  changeLog: string[] = [];
  s: any = null;
  p: any = null;
  public assetSymbol: string = "";
  public IsSubscribed: boolean = false;
  public tokenDetail: TokenDetail;


  constructor(private elementRef: ElementRef, private renderer: Renderer2, private rateService: rateSignalRService) {

    rateService.connectionEstablished.subscribe(() => {

      this.IsSubscribed = true;
      this.refreshTokenDetail();
    });
  }

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

    console.log("Mini-Chart Selected Asset is {0}", this.selectedAsset);

    this.refreshTokenDetail();

  }


  renderChart() {
    console.log("Mini-Chart Selected Asset is {0}", this.selectedAsset);

    if (this.s != null)
      this.renderer.removeChild(this.elementRef.nativeElement, this.p);

    if (this.tokenDetail.tradingViewSymbol != "None") {

      this.p = this.renderer.createElement('div');
      this.s = this.renderer.createElement("script");

      this.s.id = "MiniChart";
      this.s.type = 'text/javascript';
      this.s.src = "https://s3.tradingview.com/external-embedding/embed-widget-mini-symbol-overview.js";


      //this.s.text = `{  "symbol": "NASDAQ:${this.selectedAsset}",

      if (this.IsSubscribed == true)
        this.assetSymbol = this.tokenDetail.tradingViewSymbol;
      else
        this.assetSymbol = "NASDAQ:AAPL";

      this.s.text = `{  "symbol": "${this.assetSymbol}",
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

  refreshTokenDetail() {

    if (this.IsSubscribed == true) {
      this.rateService.getTokenDetail(this.selectedAsset).then((data) => {
        console.log(data);
        this.tokenDetail = data;
        this.renderChart();

      }).catch((reason: any) => { console.log(reason); });
    }
  }

  ngAfterViewInit() {
    this.refreshTokenDetail();
  }
}
