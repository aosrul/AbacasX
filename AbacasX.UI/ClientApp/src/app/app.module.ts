import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { GridModule } from '@progress/kendo-angular-grid';
import { GaugesModule } from '@progress/kendo-angular-gauges';
import { ChartsModule } from 'ng2-charts';


import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { LoginService } from '../core/login.service';
import { JwtModule } from '@auth0/angular-jwt';
import { TradingViewChartComponent } from './tradingView-chart/tradingView-chart.component';
import { TradingViewCryptoComponent } from './tradingView-crypto/tradingView-crypto.component';
import { TradingViewAnalysisComponent } from './tradingView-analysis/tradingView-analysis.component';
import { SafePipe } from '../shared/safepipe';
import { NewsComponent } from './news/news.component';
import { CommunityComponent } from './community/community.component';
import { AllTradingComponent } from './all-trading/all-trading.component';
import { EventLogComponent } from './event-log/event-log.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    TradingViewChartComponent,
    TradingViewCryptoComponent,
    TradingViewAnalysisComponent,
    CommunityComponent,
    AllTradingComponent,
    NewsComponent,
    EventLogComponent,
    SafePipe
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ButtonsModule,
    GridModule,
    ChartsModule,
    DropDownsModule,
    GaugesModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => { return localStorage.getItem('jwt') },
        whitelistedDomains: ['localhost:63720', 'localhost:63720/api']
      }
    }),
    RouterModule.forRoot([
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'all-trading', component: AllTradingComponent },
      { path: '**', redirectTo: 'home' }
    ])
  ],
  exports: [SafePipe],
  providers: [LoginService],
  bootstrap: [AppComponent]
})
export class AppModule { }
