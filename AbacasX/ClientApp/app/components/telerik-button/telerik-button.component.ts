import { Component } from '@angular/core';

@Component({
    selector: 'telerikbutton',
    templateUrl: './telerik-button.component.html',
})
export class TelerikButtonComponent {
    title = 'Hello Tour !';

    onButtonClick() {
        this.title = 'Hello from Kendo UI!';
    }
}