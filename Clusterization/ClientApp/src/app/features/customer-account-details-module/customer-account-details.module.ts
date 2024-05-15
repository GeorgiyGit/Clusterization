import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerAccountDetailsRoutingModule } from './customer-account-details-routing.module';
import { CustomerProfilesListPageComponent } from './children/profiles/pages/customer-profiles-list-page/customer-profiles-list-page.component';
import { CustomerProfilesMainPageComponent } from './children/profiles/pages/customer-profiles-main-page/customer-profiles-main-page.component';
import { CustomerQuotasItemCardComponent } from './children/quotas/components/customer-quotas-item-card/customer-quotas-item-card.component';
import { CustomerQuotasListPageComponent } from './children/quotas/pages/customer-quotas-list-page/customer-quotas-list-page.component';
import { CustomerQuotasLogsPageComponent } from './children/quotas/pages/customer-quotas-logs-page/customer-quotas-logs-page.component';
import { CustomerQuotasMainPageComponent } from './children/quotas/pages/customer-quotas-main-page/customer-quotas-main-page.component';
import { CustomerQuotasPackLogsPageComponent } from './children/quotas/pages/customer-quotas-pack-logs-page/customer-quotas-pack-logs-page.component';
import { CustomerTasksListComponent } from './children/tasks/components/customer-tasks-list/customer-tasks-list.component';
import { CustomerTasksListPageComponent } from './children/tasks/pages/customer-tasks-list-page/customer-tasks-list-page.component';
import { CustomerWorkspacesListPageComponent } from './children/workspaces/pages/customer-workspaces-list-page/customer-workspaces-list-page.component';
import { CustomerWorkspacesMainPageComponent } from './children/workspaces/pages/customer-workspaces-main-page/customer-workspaces-main-page.component';
import { CustomerAccountDetailsNavPageComponent } from './pages/customer-account-details-nav-page/customer-account-details-nav-page.component';
import { CoreModule } from 'src/app/core/core.module';
import { ClusterizationModule } from '../clusterization-module/clusterization.module';
import { SharedModule } from '../shared-module/shared.module';
import { CustomerPersonalInformationMainPageComponent } from './children/personal-information/pages/customer-personal-information-main-page/customer-personal-information-main-page.component';
import { CustomerPersonalInformationPageComponent } from './children/personal-information/pages/customer-personal-information-page/customer-personal-information-page.component';
import { CustomerTelegramChannelsLoadedListPageComponent } from './children/data-sources/telegram/customer-telegram-channels-loaded-list-page/customer-telegram-channels-loaded-list-page.component';
import { CustomerTelegramMainPageComponent } from './children/data-sources/telegram/customer-telegram-main-page/customer-telegram-main-page.component';
import { CustomerTelegramMessagesLoadedListPageComponent } from './children/data-sources/telegram/customer-telegram-messages-loaded-list-page/customer-telegram-messages-loaded-list-page.component';
import { TelegramModule } from '../dataSources-modules/telegram/telegram.module';
import { CustomerYoutubeChannelsLoadedListPageComponent } from './children/data-sources/youtube/customer-youtube-channels-loaded-list-page/customer-youtube-channels-loaded-list-page.component';
import { CustomerYoutubeMainPageComponent } from './children/data-sources/youtube/customer-youtube-main-page/customer-youtube-main-page.component';
import { CustomerYoutubeVideosLoadedListPageComponent } from './children/data-sources/youtube/customer-youtube-videos-loaded-list-page/customer-youtube-videos-loaded-list-page.component';
import { YoutubeModule } from "../dataSources-modules/youtube/youtube.module";


@NgModule({
    declarations: [
        CustomerAccountDetailsNavPageComponent,
        CustomerQuotasListPageComponent,
        CustomerQuotasMainPageComponent,
        CustomerQuotasLogsPageComponent,
        CustomerQuotasItemCardComponent,
        CustomerTasksListComponent,
        CustomerTasksListPageComponent,
        CustomerQuotasPackLogsPageComponent,
        CustomerWorkspacesMainPageComponent,
        CustomerWorkspacesListPageComponent,
        CustomerProfilesMainPageComponent,
        CustomerProfilesListPageComponent,
        CustomerPersonalInformationMainPageComponent,
        CustomerPersonalInformationPageComponent,
        CustomerTelegramChannelsLoadedListPageComponent,
        CustomerTelegramMainPageComponent,
        CustomerTelegramMessagesLoadedListPageComponent,
        CustomerYoutubeChannelsLoadedListPageComponent,
        CustomerYoutubeMainPageComponent,
        CustomerYoutubeVideosLoadedListPageComponent
    ],
    imports: [
        CommonModule,
        CustomerAccountDetailsRoutingModule,
        CoreModule,
        ClusterizationModule,
        TelegramModule,
        SharedModule,
        YoutubeModule
    ]
})
export class CustomerAccountDetailsModule { }
