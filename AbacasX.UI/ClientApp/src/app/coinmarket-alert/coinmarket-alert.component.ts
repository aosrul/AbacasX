import { Component, OnInit, AfterViewInit, ElementRef, Renderer2 } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';


@Component({
  selector: 'coinmarket-alert',
  templateUrl: './coinmarket-alert.component.html',
})
export class CoinMarketAlertComponent implements OnInit {
  s: any = null;
  p: any = null;

  constructor(private elementRef: ElementRef, private renderer: Renderer2) { }

  ngOnInit() {

  }

  ngAfterViewInit() {

    this.p = this.renderer.createElement('div');
    this.p.id = "gcw_mainFJZCHsNpT";
    this.p.class = "gcwmainFJZCHsNpT";

    this.s = this.renderer.createElement("script");

    this.s.id = "'scFJZCHsNpT'";
    this.s.type = 'text/javascript';
    this.s.src = "https://coinmarketalert.com/ajax/widget-vertical?p=FJZCHsNpT&v=f&lang=en&currency=USD&source=PriceIncreaseDecreaseAlert&width=700&height=738&thm=A6C9E2,FCFDFD,4297D7,5C9CCC,FFFFFF,C5DBEC,FCFDFD,2E6E9E,000000&title=Cryptocurrency%20Price%20Alert";

    this.renderer.appendChild(this.p, this.s);
    this.renderer.appendChild(this.elementRef.nativeElement, this.p);
  }
}


