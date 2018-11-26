import { Component } from '@angular/core';

@Component({
  selector: 'ops-monitor',
  templateUrl: './ops-monitor.component.html'
})
export class OpsMonitorComponent {
  public source: Array<string> = ["@AAPL", "@GOOG", "@MSFT", "@GOLD", "@BTC", "@ETH", "@BNP"];
  public data: Array<string>;
  public events: string[] = [];

  public value: number = 20;
  public orderValue: number = 2;
  public liquidityValue: number = 30;
  public rateValue: number = 85;
  public positionValue: number = 45;
  public workflowValue: number = 55;

  public colors: any[] = [{
    to: 25,
    color: '#0058e9'
  }, {
    from: 25,
    to: 50,
    color: '#37b400'
  }, {
    from: 50,
    to: 75,
    color: '#ffc000'
  }, {
    from: 75,
    color: '#f31700'
  }];

  constructor() { this.data = this.source.slice(); }


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
