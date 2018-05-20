import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { GridModule } from '@progress/kendo-angular-grid';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { TelerikButtonComponent } from './components/telerik-button/telerik-button.component';
import { TelerikGridComponent } from './components/telerik-grid/telerik-grid.component';
import { TradingComponent } from './components/trading/trading.component';
import { AdvancedTradingComponent } from './components/advanced-trading/advanced-trading.component';
import { BasicTradingComponent } from './components/basic-trading/basic-trading.component';
import { AccountsComponent } from './components/accounts/accounts.component';
import { SettingsComponent } from './components/settings/settings.component';
import { OrdersComponent } from './components/orders/orders.component';
import { ChartsModule } from 'ng2-charts';
import { LineChartComponent } from './components/line-chart/line-chart.component';
import { BlockchainComponent } from './components/blockchain/blockchain.component';
import { ReportingComponent } from './components/reporting/reporting.component';
import { PortfolioComponent } from './components/portfolio/portfolio.component';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        TelerikButtonComponent,
        TelerikGridComponent,
        TradingComponent,
        AdvancedTradingComponent,
        BasicTradingComponent,
        OrdersComponent,
        AccountsComponent,
        SettingsComponent,
        LineChartComponent,
        BlockchainComponent,
        PortfolioComponent,
        ReportingComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        BrowserAnimationsModule,
        ButtonsModule,
        GridModule,
        ChartsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },

            { path: 'trading', component: TradingComponent },
            { path: 'advanced-trading', component: AdvancedTradingComponent },
            { path: 'basic-trading', component: BasicTradingComponent },
            { path: 'orders',   component: OrdersComponent },
            { path: 'accounts', component: AccountsComponent },
            { path: 'settings', component: SettingsComponent },
            { path: 'charts', component: LineChartComponent },
            { path: 'reporting' , component: ReportingComponent },
            { path: 'portfolio' , component: PortfolioComponent },
            { path: 'blockchain', component: BlockchainComponent },

            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: 'telerik-button', component: TelerikButtonComponent },
            { path: 'telerik-grid', component: TelerikGridComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
