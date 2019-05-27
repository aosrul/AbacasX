import { Component, OnInit, AfterViewInit, ElementRef, Renderer2, Input, OnChanges, SimpleChange } from '@angular/core';


@Component({
  selector: 'tradingview-analysis',
  templateUrl: './tradingView-analysis.component.html'
})
export class TradingViewAnalysisComponent implements OnInit, AfterViewInit, OnChanges {
  @Input() selectedAsset: string = "";
  changeLog: string[] = [];
  s: any = null;
  p: any = null;
  public assetSymbol: string = "";


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

      if (propName === "selectedAsset")
      {
        this.changeLog.push(log.join(', '));
        this.selectedAssetChanged(changedProp.currentValue);
      }
    }
  }


  selectedAssetChanged(currentValue: string) {

    if (this.s != null)
    {
      this.renderer.removeChild(this.elementRef.nativeElement, this.p);

      this.p = this.renderer.createElement('div');
      this.s = this.renderer.createElement("script");

      this.s.id = "TechAnalysis";
      this.s.type = 'text/javascript';
      this.s.src = "https://s3.tradingview.com/external-embedding/embed-widget-technical-analysis.js";

      this.assetSymbol = "NASDAQ:" + this.selectedAsset;

      if (this.selectedAsset == "BNP")
        this.assetSymbol = "MIL:BNP";
      else if (this.selectedAsset == "GOLD")
        this.assetSymbol = "TVC:GOLD"
      else if (this.selectedAsset == "BTC")
        this.assetSymbol = "COINBASE:BTCUSD";
      else if (this.selectedAsset == "ETH")
        this.assetSymbol = "KFRAKE:ETHUSD";

      this.s.text = `{
      "width": "100\%",
      "height": "300",
      "symbol": "${this.assetSymbol}",
      "locale": "en",
      "interval": "1D"}`;

      this.renderer.appendChild(this.p, this.s);
      this.renderer.appendChild(this.elementRef.nativeElement, this.p);
    }
  }

  ngAfterViewInit() {

    this.p = this.renderer.createElement('div');
    this.s = this.renderer.createElement("script");

    this.s.id = "TechAnalysis";
    this.s.type = 'text/javascript';
    this.s.src = "https://s3.tradingview.com/external-embedding/embed-widget-technical-analysis.js";
    
    this.s.text = `{
      "width": "100\%",
      "height": "300",
      "symbol": "NASDAQ:${this.selectedAsset}",
      "locale": "en",
      "interval": "1D"}`;

    this.renderer.appendChild(this.p, this.s);
    this.renderer.appendChild(this.elementRef.nativeElement, this.p);
  }
}
