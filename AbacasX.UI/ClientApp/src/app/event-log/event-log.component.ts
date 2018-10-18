import { Component, Input } from '@angular/core';

@Component({
  selector: 'event-log',
  template: `
    <div class="example-config">
      <h5>{{ title }}</h5>
      <ul class="event-log">
        <li *ngFor="let event of events.reverse()">{{ event }}</li>
      </ul>
    </div>
  `
})
export class EventLogComponent {
  @Input() title: string;
  @Input() events: string[];
}
