import { Injectable } from '@angular/core';
import { ILineData } from '../../shared/interfaces';

@Injectable()
export class ChartDataService {
    constructor() { }


    private lineChartDataAAPLMSFT: Array<ILineData> = [
        { data: [1, 2, 3, 4, 5, 6], label: 'AAPL Per MSFT' }];
    private lineChartDataAAPL: Array<ILineData> = [
        { data: [180, 165, 175, 163, 185, 191], label: '@AAPL' },];
    private lineChartDataGOOG: Array<ILineData> = [
        { data: [1140.0, 1060.0, 980.0, 1080.0, 1120.0, 1129], label: '@GOOG' },];
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

    public lineChartData1: Array<ILineData> = [
        { data: [155, 180, 165, 175, 163, 185], label: '@AAPL' },];
    public lineChartData2: Array<ILineData> = [
        { data: [6.96, 6.33, 6.42, 5.66, 6.63, 5.66], label: '@AAPL Per @GOOG' }];
    public lineChartData3: Array<ILineData> = [
        { data: [1080.0, 1140.0, 1060.0, 980.0, 1080.0, 1048.0], label: '@GOOG' },];


}