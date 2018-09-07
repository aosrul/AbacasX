import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { GridModule } from '@progress/kendo-angular-grid';

import { CoreModule } from './core/core.module';
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { StandardTradingComponent } from './components/standard-trading/standard-trading.component';
import { AdvancedTradingComponent } from './components/advanced-trading/advanced-trading.component';
import { QuickTradingComponent } from './components/quick-trading/quick-trading.component';
import { QuickTradingReactiveComponent } from './components/quick-trading-reactive/quick-trading-reactive.component';
import { AccountsComponent } from './components/accounts/accounts.component';
import { SettingsComponent } from './components/settings/settings.component';
import { OrdersComponent } from './components/orders/orders.component';
import { ChartsModule } from 'ng2-charts';
import { LineChartComponent } from './components/line-chart/line-chart.component';
import { BlockchainComponent } from './components/blockchain/blockchain.component';
import { ReportingComponent } from './components/reporting/reporting.component';
import { PortfolioComponent } from './components/portfolio/portfolio.component';
import { OrderBookComponent } from './components/order-book/order-book.component';
import { AllTradingComponent } from './components/all-trading/all-trading.component';
import { RateService } from './components/rate-service/rate.service';
import { OrderHistoryComponent } from './components/order-history/order-history.component'
import { OpenOrdersComponent } from './components/open-orders/open-orders.component'
import { PortfolioDetailComponent } from './components/portfolio-detail/portfolio-detail.component'
import { BlockchainDetailComponent } from './components/blockchain-detail/blockchain-detail.component'
import { EnumAsStringPipe } from './shared/pipes/enum.pipe'


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        StandardTradingComponent,
        AdvancedTradingComponent,
        QuickTradingComponent,
        QuickTradingReactiveComponent,
        OrdersComponent,
        AccountsComponent,
        SettingsComponent,
        LineChartComponent,
        BlockchainComponent,
        PortfolioComponent,
        ReportingComponent,
        OrderBookComponent,
        AllTradingComponent,
        OrderHistoryComponent,
        OpenOrdersComponent,
        PortfolioDetailComponent,
        BlockchainDetailComponent,
        EnumAsStringPipe
    ],
    providers: [RateService],
    exports:[EnumAsStringPipe],
    imports: [
        CoreModule,
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        ButtonsModule,
        GridModule,
        ChartsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },

            { path: 'all-trading', component: AllTradingComponent },
            { path: 'standard-trading', component: StandardTradingComponent },
            { path: 'advanced-trading', component: AdvancedTradingComponent },
            { path: 'quick-trading', component: QuickTradingComponent },
            { path: 'quick-trading-reactive', component: QuickTradingReactiveComponent },
            { path: 'orders',   component: OrdersComponent },
            { path: 'accounts', component: AccountsComponent },
            { path: 'settings', component: SettingsComponent },
            { path: 'charts', component: LineChartComponent },
            { path: 'reporting' , component: ReportingComponent },
            { path: 'portfolio' , component: PortfolioComponent },
            { path: 'blockchain', component: BlockchainComponent },
            { path: 'order-history', component: OrderHistoryComponent },
            { path: 'open-orders', component: OpenOrdersComponent },
            { path: 'portfolio-detail', component: PortfolioDetailComponent },
            { path: 'blockchain-detail', component: BlockchainDetailComponent },
            
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
