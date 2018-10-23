import { Component, OnInit, AfterViewInit, ElementRef } from '@angular/core';


@Component({
  selector: 'tradingview-crypto',
  templateUrl: './tradingView-crypto.component.html',
})
export class TradingViewCryptoComponent implements OnInit {

  constructor(private elementRef: ElementRef) { }

  ngOnInit() {
  }

  ngAfterViewInit() {

    let s = document.createElement("script");
    s.type = 'text/javascript';
    s.src = "https://s3.tradingview.com/external-embedding/embed-widget-screener.js";
    s.text = `{
            "width": "100%",
            "height": "100%",
            "defaultColumn": "performance",
            "screener_type": "crypto_mkt",
            "displayCurrency": "USD",
            "locale": "en"
          }`;

    this.elementRef.nativeElement.appendChild(s);
  }
}

