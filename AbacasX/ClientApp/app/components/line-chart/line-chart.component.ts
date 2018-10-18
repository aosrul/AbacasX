import { Component, OnInit, Input, OnChanges, SimpleChange } from '@angular/core';
import { ILineData } from '../../shared/interfaces';
import { ChartDataService } from '../chart-data/chart-data.service';

@Component({
    selector: 'line-chart',
    providers: [ChartDataService],
    templateUrl: './line-chart.component.html'
})
export class LineChartComponent implements OnInit, OnChanges {
    @Input()
    selectedAssetPair: string = "";
    changeLog: string[] = [];

    selectedAsset1: string = "AAPL";
    selectedAsset2: string = "GOOG";

    private token1Id: string = "@AAPL";
    private token2Id: string = "@GOOG";


    public lineChartData1: Array<ILineData> = [
        { data: [180, 165, 175, 163, 185,194], label: '@AAPL' },];
    public lineChartData2: Array<ILineData> = [
        { data: [6.33, 6.42, 5.66, 6.63, 5.66, 6.5360], label: '@AAPL Per @GOOG' }];
    public lineChartData3: Array<ILineData> = [
        { data: [1140.0, 1060.0, 980.0, 1080.0, 1048.0, 1268], label: '@GOOG' },];


    private lineChartDataAAPLMSFT: Array<ILineData> = [
        { data: [1, 2, 3, 4, 5, 6], label: 'AAPL Per MSFT' }];
    private lineChartDataAAPL: Array<ILineData> = [
        { data: [165, 175, 163, 185, 191, 194], label: '@AAPL' },];
    private lineChartDataGOOG: Array<ILineData> = [
        { data: [1060.0, 980.0, 1080.0, 1120.0, 1129, 1268], label: '@GOOG' },];
    private lineChartDataMSFT: Array<ILineData> = [
        { data: [86, 95, 93, 90, 95, 101], label: '@MSFT' },];
    private lineChartDataGOLD: Array<ILineData> = [
        { data: [1240.0, 1340.0, 1310, 1330, 1300, 1290], label: '@GOLD' },];
    private lineChartDataBTC: Array<ILineData> = [
        { data: [12613.0, 10151.0, 10293.0, 6939.0, 9223.0, 7627.0], label: '@BTC' },];
    private lineChartDataBNP: Array<ILineData> = [
        { data: [63, 66, 65, 60, 63, 54], label: '@BNP' },];
    private lineChartDataEUR: Array<ILineData> = [
        { data: [.83045, .8147, .8170, .8127, .8279, .8570], label: '@EUR' }];
    private lineChartDataUSD: Array<ILineData> = [
        { data: [1, 1, 1, 1, 1, 1], label: '@USD' }];


    constructor(chartDataService: ChartDataService) {
    }

    ngOnInit() {
        this.lineChartData1 = this.lineChartDataAAPL;
        this.lineChartData3 = this.lineChartDataGOOG;

        this.lineChartData2[0].label = this.token1Id + ' per ' + this.token2Id;

        for (let i = 0; i < 6; i++) {
            this.lineChartData2[0].data[i] = (1.0 / this.lineChartData1[0].data[i]) *
                this.lineChartData3[0].data[i];
        }
    };

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

            if (propName === "selectedAssetPair")
                this.selectedAssetPairChanged(changedProp.currentValue);
        }

        this.changeLog.push(log.join(', '));
    }


    selectedAssetPairChanged(newAssetPair: string) {
        if (this.selectedAssetPair == "@AAPL - @GOOG") {
            this.token1Id = "@AAPL";
            this.token2Id = "@GOOG";
            this.lineChartData1 = this.lineChartDataAAPL;
            this.lineChartData3 = this.lineChartDataGOOG;
        }

        if (this.selectedAssetPair == "@AAPL - @GOLD") {
            this.token1Id = "@AAPL";
            this.token2Id = "@GOLD";
            this.lineChartData1 = this.lineChartDataAAPL;
            this.lineChartData3 = this.lineChartDataGOLD;

        }

        if (this.selectedAssetPair == "@AAPL - @MSFT") {
            this.token1Id = "@AAPL";
            this.token2Id = "@MSFT";

            this.lineChartData1 = this.lineChartDataAAPL;
            this.lineChartData3 = this.lineChartDataMSFT;
            this.lineChartData2 = this.lineChartDataAAPLMSFT;
        }

        if (this.selectedAssetPair == "@AAPL - @BTC") {
            this.token1Id = "@AAPL";
            this.token2Id = "@BTC";

            this.lineChartData1 = this.lineChartDataAAPL;
            this.lineChartData3 = this.lineChartDataBTC;

        }

        if (this.selectedAssetPair == "@AAPL - @USD") {
            this.token1Id = "@AAPL";
            this.token2Id = "@USD";

            this.lineChartData1 = this.lineChartDataAAPL;
            this.lineChartData3 = this.lineChartDataUSD;
        }

        this.lineChartData2[0].label = this.token1Id + ' per ' + this.token2Id;

        for (let i = 0; i < 6; i++) {
            this.lineChartData2[0].data[i] = (1.0 / this.lineChartData1[0].data[i]) *
                this.lineChartData3[0].data[i];
        }
    }

    public lineChartLabels: Array<any> = ['February', 'March', 'April', 'May', 'June','July'];
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