import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogInPageComponent } from './account/pages/log-in-page/log-in-page.component';
import { SignUpPageComponent } from './account/pages/sign-up-page/sign-up-page.component';
import { QuotasLogsCardComponent } from './quotas/components/quotas-logs-card/quotas-logs-card.component';
import { QuotasPackCardComponent } from './quotas/components/quotas-pack-card/quotas-pack-card.component';
import { QuotasPackLogsCardComponent } from './quotas/components/quotas-pack-logs-card/quotas-pack-logs-card.component';
import { QuotasTypesSelectComponent } from './quotas/components/quotas-types-select/quotas-types-select.component';
import { QuotasPackListComponent } from './quotas/components/quotas-pack-list/quotas-pack-list.component';
import { TaskCardComponent } from './tasks/components/task-card/task-card.component';
import { TaskListComponent } from './tasks/components/task-list/task-list.component';
import { TaskStatusesSelectComponent } from './tasks/components/task-statuses-select/task-statuses-select.component';
import { FullTaskPageComponent } from './tasks/pages/full-task-page/full-task-page.component';
import { CoreModule } from 'src/app/core/core.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTooltipModule } from '@angular/material/tooltip';


@NgModule({
  declarations: [
    QuotasPackCardComponent,
    QuotasPackListComponent,
    QuotasTypesSelectComponent,
    QuotasLogsCardComponent,
    QuotasPackLogsCardComponent,

    FullTaskPageComponent,
    TaskCardComponent,
    TaskListComponent,
    TaskStatusesSelectComponent,

    LogInPageComponent,
    SignUpPageComponent,
  ],
  exports:[
    QuotasPackListComponent,
    QuotasTypesSelectComponent,
    QuotasLogsCardComponent,
    QuotasPackLogsCardComponent,
    TaskStatusesSelectComponent,
    TaskCardComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    FormsModule,
    ReactiveFormsModule,
    MatTooltipModule
  ]
})
export class SharedModule { }
