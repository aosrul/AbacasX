﻿import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'telerikgrid',
    templateUrl: './telerik-grid.component.html'
})
export class TelerikGridComponent {
    public forecasts: WeatherForecast[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
            this.forecasts = result.json() as WeatherForecast[];
        }, error => console.error(error));
    }

}

interface WeatherForecast {
    dateFormatted: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}