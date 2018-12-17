import { Component } from '@angular/core';
import { news } from './news';

@Component({
  selector: 'news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent {
  public abacasNews: any[] = news;
}
