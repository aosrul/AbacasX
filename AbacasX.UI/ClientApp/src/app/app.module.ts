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
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';


import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { LoginService } from '../core/login.service';
import { rateSignalRService } from '../core/rate.service';
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
import { OpsMonitorComponent } from './ops-monitor/ops-monitor.component';
import { AdminMonitorComponent } from './admin-monitor/admin-monitor.component';
import { StockComponent } from './stock/stock.component';
import { CustodianAssetComponent } from './custodian/custodian-asset/custodian-asset.component';
import { CustodianDepositComponent } from './custodian/custodian-deposit/custodian-deposit.component';
import { CustodianWithdrawalComponent } from './custodian/custodian-withdrawal/custodian-withdrawal.component';
import { CustodianDefinitionComponent } from './custodian/custodian-definition/custodian-definition.component';
import { CustodianReportingComponent } from './custodian/custodian-reporting/custodian-reporting.component';
import { CustodianSettingsComponent } from './custodian/custodian-settings/custodian-settings.component';
import { CustodianAssetDetailComponent } from './custodian/custodian-asset-detail/custodian-asset-detail.component';
import { CustodianDepositPendingComponent } from './custodian/custodian-deposit-pending/custodian-deposit-pending.component';
import { CustodianDepositHistoryComponent } from './custodian/custodian-deposit-history/custodian-deposit-history.component';
import { CustodianWithdrawalPendingComponent } from './custodian/custodian-withdrawal-pending/custodian-withdrawal-pending.component';
import { CustodianWithdrawalHistoryComponent } from './custodian/custodian-withdrawal-history/custodian-withdrawal-history.component';
import { AccountDepositComponent } from './account-deposit/account-deposit.component';
import { AccountWithdrawalComponent } from './account-withdrawal/account-withdrawal.component';
import { AccountTransferActivityComponent } from './account-transfer-activity/account-transfer-activity.component';
import { CoinMarketAlertComponent } from './coinmarket-alert/coinmarket-alert.component';


export function tokenGetter(){
  return localStorage.getItem('jwt');
}

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
    OrderBookComponent,
    OpsMonitorComponent,
    AdminMonitorComponent,
    StockComponent,
    CustodianAssetComponent,
    CustodianDepositComponent,
    CustodianWithdrawalComponent,
    CustodianDefinitionComponent,
    CustodianReportingComponent,
    CustodianSettingsComponent,
    CustodianAssetDetailComponent,
    CustodianDepositPendingComponent,
    CustodianDepositHistoryComponent,
    CustodianWithdrawalPendingComponent,
    CustodianWithdrawalHistoryComponent,
    AccountDepositComponent,
    AccountWithdrawalComponent,
    AccountTransferActivityComponent,
    CoinMarketAlertComponent
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
    DateInputsModule,
    NgbModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:63720', 'localhost:63720/api']
      }
    }),
    RouterModule.forRoot([
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent },

      { path: 'all-trading', component: AllTradingComponent },
      { path: 'standard-trading', component: StandardTradingComponent },
      { path: 'advanced-trading', component: AdvancedTradingComponent },
      { path: 'quick-trading', component: QuickTradingComponent },
      { path: 'orders', component: OrdersComponent },
      { path: 'accounts', component: AccountsComponent },

      { path: 'account-deposit', component: AccountDepositComponent },
      { path: 'account-withdrawal', component: AccountWithdrawalComponent },
      { path: 'account-transfer-activity', component: AccountTransferActivityComponent},


      { path: 'settings', component: SettingsComponent },
      { path: 'reporting', component: ReportingComponent },
      { path: 'portfolio', component: PortfolioComponent },
      { path: 'blockchain', component: BlockchainComponent },
      { path: 'order-history', component: OrderHistoryComponent },
      { path: 'open-orders', component: OpenOrdersComponent },
      { path: 'portfolio-detail', component: PortfolioDetailComponent },
      { path: 'blockchain-detail', component: BlockchainDetailComponent },
      { path: 'ops-monitor', component: OpsMonitorComponent },
      { path: 'admin-monitor', component: AdminMonitorComponent },
      { path: 'stocks', component: StockComponent },

      { path: 'custodian-asset', component: CustodianAssetComponent },
      { path: 'custodian-asset-detail', component: CustodianAssetDetailComponent },
      { path: 'custodian-deposit', component: CustodianDepositComponent },
      { path: 'custodian-deposit-pending', component: CustodianDepositPendingComponent },
      { path: 'custodian-deposit-history', component: CustodianDepositHistoryComponent },

      { path: 'custodian-withdrawal', component: CustodianWithdrawalComponent },
      { path: 'custodian-withdrawal-pending', component: CustodianWithdrawalPendingComponent },
      { path: 'custodian-withdrawal-history', component: CustodianWithdrawalHistoryComponent },

      { path: 'custodian-reporting', component: CustodianReportingComponent },
      { path: 'custodian-definition', component: CustodianDefinitionComponent },
      { path: 'custodian-settings', component: CustodianSettingsComponent },
      { path: 'coinmarket-alert', component: CoinMarketAlertComponent},

      { path: '**', redirectTo: 'home' }
    ])
  ],
  exports: [SafePipe, EnumAsStringPipe],
  providers: [LoginService, rateSignalRService],
  bootstrap: [AppComponent]
})
export class AppModule { }
