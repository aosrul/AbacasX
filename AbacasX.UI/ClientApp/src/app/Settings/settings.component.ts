import { Component } from '@angular/core';
import { DataService } from '../../core/data.service';
import { rateSignalRService } from '../../core/rate.service';

@Component({
  selector: 'settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent {


  public brokerLiquidityIsOn: boolean = true;
  public rateFeedIsOn: boolean = true;
  public indirectLiquidityIsOn: boolean = false;

  public brokerLiquiditySetting: string = "On";
  public rateFeedSetting: string = "On";
  public indirectLiquiditySetting: string = "Off";

  public errorMessage: string;

  constructor(private dataService: DataService, private rateService: rateSignalRService) {
    this.checkBrokerLiquiditySetting();
    this.checkRateFeedSetting();
  };

  checkBrokerLiquiditySetting() {
    this.dataService.isBrokerLiquidityOn().
      subscribe((response: any) => {
        if (response != null) {
          this.brokerLiquidityIsOn = response;

          if (this.brokerLiquidityIsOn)
            this.brokerLiquiditySetting = "On";
          else
            this.brokerLiquiditySetting = "Off";
        }
        else {
          this.errorMessage = "Unable to check liquidity setting";
        }
      },
        (err: any) => console.log(err));
  }

  toggleLiquidity() {
    this.dataService.toggleBrokerLiquidity().
      subscribe((response: any) => {
        if (response != null) {
          this.brokerLiquidityIsOn = response;

          if (this.brokerLiquidityIsOn)
            this.brokerLiquiditySetting = "On";
          else
            this.brokerLiquiditySetting = "Off";
        }
        else {
          this.errorMessage = "Unable to toggle liquidity";
        }
      },
        (err: any) => console.log(err));
  }

  checkRateFeedSetting() {
    this.rateService.isRateFeedOn().then((data) => {
      console.log(data);
      this.rateFeedIsOn = data;
      this.rateFeedSetting = (this.rateFeedIsOn == true ? "On" : "Off");
    }).catch((reason: any) => { console.log(reason); });
  }

  toggleRateFeed() {

    this.rateService.toggleRateFeed().then((data) => {
      console.log(data);
      this.rateFeedIsOn = data;
      this.rateFeedSetting = (this.rateFeedIsOn == true ? "On" : "Off");
    }).catch((reason: any) => { console.log(reason); });
  }

  toggleIndirectLiquidity() {
    this.indirectLiquidityIsOn = false;
    this.indirectLiquiditySetting = "Off";
  }

   
}
