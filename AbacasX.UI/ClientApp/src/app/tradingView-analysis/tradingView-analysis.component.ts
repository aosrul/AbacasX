import { Component, OnInit, AfterViewInit, ElementRef, Renderer2, Input, OnChanges, SimpleChange } from '@angular/core';
import { rateSignalRService } from '../../core/rate.service';
import { TokenDetail } from '../../shared/interfaces';


@Component({
  selector: 'tradingview-analysis',
  templateUrl: './tradingView-analysis.component.html',
  providers: [rateSignalRService]
})
export class TradingViewAnalysisComponent implements OnInit, AfterViewInit, OnChanges {
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

  refreshTokenDetail() {

    if (this.IsSubscribed == true) {
      this.rateService.getTokenDetail(this.selectedAsset).then((data) => {
        console.log(data);
        this.tokenDetail = data;
        this.renderChart();

      }).catch((reason: any) => { console.log(reason); });
    }
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

  renderChart() {
    if (this.s != null)
      this.renderer.removeChild(this.elementRef.nativeElement, this.p);

    this.p = this.renderer.createElement('div');
    this.s = this.renderer.createElement("script");

    this.s.id = "TechAnalysis";
    this.s.type = 'text/javascript';
    this.s.src = "https://s3.tradingview.com/external-embedding/embed-widget-technical-analysis.js";

    if (this.IsSubscribed == true)
      this.assetSymbol = this.tokenDetail.tradingViewSymbol;
    else
      this.assetSymbol = "NASDAQ:AAPL";

    this.s.text = `{
      "width": "100\%",
      "height": "300",
      "symbol": "${this.assetSymbol}",
      "locale": "en",
      "interval": "1D"}`;

    this.renderer.appendChild(this.p, this.s);
    this.renderer.appendChild(this.elementRef.nativeElement, this.p);
  }


  selectedAssetChanged(currentValue: string) {
    this.refreshTokenDetail();
  }

  ngAfterViewInit() {
    this.refreshTokenDetail();
  }
}
