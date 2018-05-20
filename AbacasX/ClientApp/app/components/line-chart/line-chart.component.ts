import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'line-chart',
    templateUrl: './line-chart.component.html'
})
export class LineChartComponent implements OnInit {
    
    // lineChart
    public lineChartData: Array<any> = [
        { data: [155, 180, 165, 175, 163, 185], label: 'AAPL' },
        { data: [108.0, 114.0, 106.0, 98.0, 108.0, 104.8], label: 'GOOG x .1' },
        { data: [69.6, 63.3, 64.2, 56.6, 66.3, 56.6], label: 'GOOG-APPL x 10' }
    ];

    public lineChartData1: Array<any> = [
        { data: [155, 180, 165, 175, 163, 185], label: '@AAPL' },];
    public lineChartData3: Array<any> = [
        { data: [1080.0, 1140.0, 1060.0, 980.0, 1080.0, 1048.0], label: '@GOOG' },];
    public lineChartData2: Array<any> = [
        { data: [6.96, 6.33, 6.42, 5.66, 6.63, 5.66], label: '@AAPL Per @GOOG' }];

    ngOnInit() {
         };

    public lineChartLabels: Array<any> = ['December','January', 'February', 'March', 'April', 'May'];
    public lineChartOptions: any = {
        responsive: true
    };


    public lineChartColors: Array<any> = [
        { // grey
            backgroundColor: 'rgba(148,159,177,0.2)',
            borderColor: 'rgba(148,159,177,1)',
            pointBackgroundColor: 'rgba(148,159,177,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(148,159,177,0.8)'
        },
        { // dark grey
            backgroundColor: 'rgba(77,83,96,0.2)',
            borderColor: 'rgba(77,83,96,1)',
            pointBackgroundColor: 'rgba(77,83,96,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(77,83,96,1)'
        },
        { // grey
            backgroundColor: 'rgba(148,159,177,0.2)',
            borderColor: 'rgba(148,159,177,1)',
            pointBackgroundColor: 'rgba(148,159,177,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(148,159,177,0.8)'
        }
    ];
    public lineChartLegend: boolean = true;
    public lineChartType: string = 'line';

   
    // events
    public chartClicked(e: any): void {
        console.log(e);
    }

    public chartHovered(e: any): void {
        console.log(e);
    }
}