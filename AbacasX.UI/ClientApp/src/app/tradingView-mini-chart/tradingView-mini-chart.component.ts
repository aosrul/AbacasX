import { Component, OnInit, AfterViewInit, ElementRef, Input } from '@angular/core';


@Component({
    selector: 'tradingview-minichart',
    templateUrl: './tradingView-mini-chart.component.html'
})
export class TradingViewMiniChartComponent implements OnInit, AfterViewInit {
    @Input() selectedAsset: string = "";
    changeLog: string[] = [];

    constructor(private elementRef: ElementRef) { }

    ngOnInit() {
    }

    ngAfterViewInit() {

        let s = document.createElement("script");
        s.type = 'text/javascript';
        s.src = "https://s3.tradingview.com/external-embedding/embed-widget-mini-symbol-overview.js";

        s.text = `{     "symbol": "NASDAQ:${this.selectedAsset}",
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

        this.elementRef.nativeElement.appendChild(s);
    }
}
