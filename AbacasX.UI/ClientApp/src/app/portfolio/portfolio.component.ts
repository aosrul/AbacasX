import { Component } from '@angular/core';
import { positions } from './positions';

@Component({
    selector: 'portfolio',
    templateUrl: './portfolio.component.html'
})
export class PortfolioComponent
{
    public gridData: any[] = positions;
}


