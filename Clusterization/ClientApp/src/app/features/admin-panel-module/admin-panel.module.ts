import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminPanelRoutingModule } from './admin-panel-routing.module';
import { AdminPanelNavigationPageComponent } from './pages/admin-panel-navigation-page/admin-panel-navigation-page.component';
import { CustomerCardComponent } from './users/components/customer-card/customer-card.component';
import { CustomerListComponent } from './users/components/customer-list/customer-list.component';
import { CustomerListPageComponent } from './users/pages/customer-list-page/customer-list-page.component';
import { AddQuatasPackToCustomerPageComponent } from './quotas/page/add-quatas-pack-to-customer-page/add-quatas-pack-to-customer-page.component';
import { SharedModule } from '../shared-module/shared.module';
import { CoreModule } from 'src/app/core/core.module';
import { MatTooltipModule } from '@angular/material/tooltip';


@NgModule({
  declarations: [
    CustomerCardComponent,
    CustomerListComponent,
    CustomerListPageComponent,
    AdminPanelNavigationPageComponent,
    AddQuatasPackToCustomerPageComponent,
  ],
  imports: [
    CommonModule,
    AdminPanelRoutingModule,
    SharedModule,
    CoreModule,
    MatTooltipModule
  ]
})
export class AdminPanelModule { }
