import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogInPageComponent } from './account/pages/log-in-page/log-in-page.component';
import { SignUpPageComponent } from './account/pages/sign-up-page/sign-up-page.component';
import { QuotasLogsCardComponent } from './quotas/components/quotas-logs-card/quotas-logs-card.component';
import { QuotasPackCardComponent } from './quotas/components/quotas-pack-card/quotas-pack-card.component';
import { QuotasPackLogsCardComponent } from './quotas/components/quotas-pack-logs-card/quotas-pack-logs-card.component';
import { QuotasTypesSelectComponent } from './quotas/components/quotas-types-select/quotas-types-select.component';
import { QuotasPackListComponent } from './quotas/components/quotas-pack-list/quotas-pack-list.component';
import { TaskStatusesSelectComponent } from './tasks/components/task-statuses-select/task-statuses-select.component';
import { FullTaskPageComponent } from './tasks/pages/full-task-page/full-task-page.component';
import { CoreModule } from 'src/app/core/core.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTooltipModule } from '@angular/material/tooltip';
import { EmailVerificationPageComponent } from './account/pages/email-verification-page/email-verification-page.component';
import { EmailVerificationAlertPageComponent } from './account/pages/email-verification-alert-page/email-verification-alert-page.component';
import { SubTaskCardComponent } from './tasks/components/sub-task-card/sub-task-card.component';
import { SubTaskListComponent } from './tasks/components/sub-task-list/sub-task-list.component';
import { MainTaskCardComponent } from './tasks/components/main-task-card/main-task-card.component';
import { MainTaskListComponent } from './tasks/components/main-task-list/main-task-list.component';


@NgModule({
  declarations: [
    QuotasPackCardComponent,
    QuotasPackListComponent,
    QuotasTypesSelectComponent,
    QuotasLogsCardComponent,
    QuotasPackLogsCardComponent,

    FullTaskPageComponent,
    SubTaskCardComponent,
    SubTaskListComponent,
    MainTaskCardComponent,
    MainTaskListComponent,
    TaskStatusesSelectComponent,

    LogInPageComponent,
    SignUpPageComponent,
    EmailVerificationPageComponent,
    EmailVerificationAlertPageComponent
  ],
  exports:[
    QuotasPackListComponent,
    QuotasTypesSelectComponent,
    QuotasLogsCardComponent,
    QuotasPackLogsCardComponent,
    TaskStatusesSelectComponent,
    MainTaskCardComponent,
    SubTaskCardComponent
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
