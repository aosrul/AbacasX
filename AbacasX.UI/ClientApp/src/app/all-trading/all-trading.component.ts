import { Component } from '@angular/core';

@Component({
  selector: 'all-trading',
  templateUrl: './all-trading.component.html',
})

export class AllTradingComponent {

  public source: Array<string> = ["@AAPL", "@GOOG","@MSFT","@GOLD", "@BTC", "@ETH", "@BNP"];
  public data: Array<string>;
  public events: string[] = [];
  public value: number = 20;

  constructor() { this.data = this.source.slice();}


  public valueChange(value: any): void {
    this.log('valueChange', value);
  }

  public selectionChange(value: any): void {
    this.log('selectionChange', value);
  }

  public filterChange(filter: any): void {
    this.log('filterChange', filter);
    this.data = this.source.filter((s) => s.toLowerCase().indexOf(filter.toLowerCase()) !== -1);
  }

  private log(event: string, arg: any): void {
    this.events.push(`${event} ${arg || ''}`);
  }

}


