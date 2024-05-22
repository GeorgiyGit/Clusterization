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
import { WTelegramAdminPageComponent } from './telegram/pages/w-telegram-admin-page/w-telegram-admin-page.component';
import { ModeratorMainTaskCardComponent } from './tasks/components/moderator-main-task-card/moderator-main-task-card.component';
import { ModeratorMainTaskListComponent } from './tasks/components/moderator-main-task-list/moderator-main-task-list.component';
import { ModeratorSubTaskCardComponent } from './tasks/components/moderator-sub-task-card/moderator-sub-task-card.component';
import { ModeratorSubTaskListComponent } from './tasks/components/moderator-sub-task-list/moderator-sub-task-list.component';
import { ModeratorTaskListPageComponent } from './tasks/pages/moderator-task-list-page/moderator-task-list-page.component';


@NgModule({
  declarations: [
    CustomerCardComponent,
    CustomerListComponent,
    CustomerListPageComponent,
    AdminPanelNavigationPageComponent,
    AddQuatasPackToCustomerPageComponent,
    WTelegramAdminPageComponent,

    ModeratorMainTaskCardComponent,
    ModeratorMainTaskListComponent,
    ModeratorSubTaskCardComponent,
    ModeratorSubTaskListComponent,
    ModeratorTaskListPageComponent
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
