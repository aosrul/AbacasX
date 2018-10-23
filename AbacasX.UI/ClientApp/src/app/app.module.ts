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
import { CoreModule } from '../core/core.module';


import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { LoginService } from '../core/login.service';
import { JwtModule } from '@auth0/angular-jwt';
import { TradingViewChartComponent } from './tradingView-chart/tradingView-chart.component';
import { TradingViewCryptoComponent } from './tradingView-crypto/tradingView-crypto.component';
import { TradingViewAnalysisComponent } from './tradingView-analysis/tradingView-analysis.component';
import { TradingViewMiniChartComponent } from './tradingView-mini-chart/tradingView-mini-chart.component';
import { SafePipe } from '../shared/safepipe';
import { NewsComponent } from './news/news.component';
import { CommunityComponent } from './community/community.component';
import { AllTradingComponent } from './all-trading/all-trading.component';
import { EventLogComponent } from './event-log/event-log.component';
import { EnumAsStringPipe } from '../shared/enum.pipe';
import { QuickTradingComponent } from './quick-trading/quick-trading.component';
import { LineChartComponent } from './line-chart/line-chart.component';
import { PortfolioComponent } from './portfolio/portfolio.component';
import { PortfolioDetailComponent } from './portfolio-detail/portfolio-detail.component';
import { OrdersComponent } from './orders/orders.component';
import { OpenOrdersComponent } from './open-orders/open-orders.component';
import { OrderHistoryComponent } from './order-history/order-history.component';
import { ReportingComponent } from './reporting/reporting.component';
import { SettingsComponent } from './settings/settings.component';
import { BlockchainComponent } from './blockchain/blockchain.component';
import { BlockchainDetailComponent } from './blockchain-detail/blockchain-detail.component';
import { StandardTradingComponent } from './standard-trading/standard-trading.component';
import { AdvancedTradingComponent } from './advanced-trading/advanced-trading.component';
import { AccountsComponent } from './accounts/accounts.component';
import { OrderBookComponent } from './order-book/order-book.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    TradingViewChartComponent,
    TradingViewCryptoComponent,
    TradingViewAnalysisComponent,
    TradingViewMiniChartComponent,
    CommunityComponent,
    AllTradingComponent,
    NewsComponent,
    EventLogComponent,
    SafePipe,
    EnumAsStringPipe,
    QuickTradingComponent,
    LineChartComponent,
    PortfolioComponent,
    PortfolioDetailComponent,
    OrdersComponent,
    OpenOrdersComponent,
    OrderHistoryComponent,
    SettingsComponent,
    ReportingComponent,
    BlockchainComponent,
    BlockchainDetailComponent,
    StandardTradingComponent,
    AdvancedTradingComponent,
    AccountsComponent,
    OrderBookComponent
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
    CoreModule,
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
      { path: 'quick-trading', component: QuickTradingComponent },
      { path: 'standard-trading', component: StandardTradingComponent },
      { path: 'advanced-trading', component: AdvancedTradingComponent },

      { path: 'portfolio', component: PortfolioComponent },
      { path: 'portfolio-detail', component: PortfolioDetailComponent },
      { path: 'orders', component: OrdersComponent },
      { path: 'order-history', component: OrderHistoryComponent },
      { path: 'open-orders', component: OpenOrdersComponent },
      { path: 'blockchain', component: BlockchainComponent },
      { path: 'blockchain-detail', component: BlockchainDetailComponent },
      { path: 'reporting', component: ReportingComponent },
      { path: 'settings', component: SettingsComponent },
      { path: 'accounts', component: AccountsComponent },
      
      { path: '**', redirectTo: 'home' }
    ])
  ],
  exports: [SafePipe, EnumAsStringPipe],
  providers: [LoginService],
  bootstrap: [AppComponent]
})
export class AppModule { }
