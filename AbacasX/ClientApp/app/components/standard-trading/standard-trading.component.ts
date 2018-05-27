import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'standard-trading',
    templateUrl: './standard-trading.component.html',
    styleUrls:['./standard-trading.component.css']
    
})
export class StandardTradingComponent implements OnInit {

    quickTradeForm: FormGroup;


    ngOnInit() {

    }
}
