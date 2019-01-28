import { Component, OnInit } from '@angular/core';
import { positions } from './positions';
import { DataService } from '../../core/data.service';
import { IClientPosition } from '../../shared/interfaces';

@Component({
    selector: 'portfolio-detail',
    templateUrl: './portfolio-detail.component.html'
})
export class PortfolioDetailComponent implements OnInit {

  public clientPositions: any[] = [];
    title: string = "";

    constructor(private dataService: DataService) { }


    ngOnInit(): void {
        //for(let entry of this.gridData)
        //{
        //    entry.TokenValue = entry.TokenAmount * entry.TokenRate;
        //}

        this.title = "Portfolio";
        this.getClientPositions();
    }

    getClientPositions() {
        this.dataService.getClientPositions()
            .subscribe((clientPositions: IClientPosition[]) => {
                this.clientPositions = clientPositions;
            },
                (err: any) => console.log(err),
                () => console.log('getClientPositions() retrieved positions'));
    }
}


