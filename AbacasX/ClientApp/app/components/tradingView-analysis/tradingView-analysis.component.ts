import { Component, OnInit, AfterViewInit, ElementRef, Input } from '@angular/core';


@Component({
    selector: 'tradingview-analysis',
    templateUrl: './tradingView-analysis.component.html'
})
export class TradingViewAnalysisComponent implements OnInit, AfterViewInit {

    @Input() selectedAsset: string = "";
    changeLog: string[] = [];

    constructor(private elementRef: ElementRef) { }

    ngOnInit() {
    }

    
    ngAfterViewInit() {

        let s = document.createElement("script");
        s.type = 'text/javascript';
        s.src = "https://s3.tradingview.com/external-embedding/embed-widget-technical-analysis.js";
        s.text = `{
      "width": "100\%",
      "height": "300",
      "symbol": "NASDAQ:${this.selectedAsset}",
      "locale": "en",
      "interval": "1M"}`;

        this.elementRef.nativeElement.appendChild(s);
    }
}
