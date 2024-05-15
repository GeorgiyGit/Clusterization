import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerProfilesListPageComponent } from './children/profiles/pages/customer-profiles-list-page/customer-profiles-list-page.component';
import { CustomerProfilesMainPageComponent } from './children/profiles/pages/customer-profiles-main-page/customer-profiles-main-page.component';
import { CustomerQuotasListPageComponent } from './children/quotas/pages/customer-quotas-list-page/customer-quotas-list-page.component';
import { CustomerQuotasLogsPageComponent } from './children/quotas/pages/customer-quotas-logs-page/customer-quotas-logs-page.component';
import { CustomerQuotasMainPageComponent } from './children/quotas/pages/customer-quotas-main-page/customer-quotas-main-page.component';
import { CustomerQuotasPackLogsPageComponent } from './children/quotas/pages/customer-quotas-pack-logs-page/customer-quotas-pack-logs-page.component';
import { CustomerTasksListComponent } from './children/tasks/components/customer-tasks-list/customer-tasks-list.component';
import { CustomerTasksListPageComponent } from './children/tasks/pages/customer-tasks-list-page/customer-tasks-list-page.component';
import { CustomerWorkspacesListPageComponent } from './children/workspaces/pages/customer-workspaces-list-page/customer-workspaces-list-page.component';
import { CustomerWorkspacesMainPageComponent } from './children/workspaces/pages/customer-workspaces-main-page/customer-workspaces-main-page.component';
import { CustomerAccountDetailsNavPageComponent } from './pages/customer-account-details-nav-page/customer-account-details-nav-page.component';
import { CustomerPersonalInformationMainPageComponent } from './children/personal-information/pages/customer-personal-information-main-page/customer-personal-information-main-page.component';
import { CustomerPersonalInformationPageComponent } from './children/personal-information/pages/customer-personal-information-page/customer-personal-information-page.component';
import { CustomerTelegramMainPageComponent } from './children/data-sources/telegram/customer-telegram-main-page/customer-telegram-main-page.component';
import { CustomerTelegramChannelsLoadedListPageComponent } from './children/data-sources/telegram/customer-telegram-channels-loaded-list-page/customer-telegram-channels-loaded-list-page.component';
import { CustomerTelegramMessagesLoadedListPageComponent } from './children/data-sources/telegram/customer-telegram-messages-loaded-list-page/customer-telegram-messages-loaded-list-page.component';
import { CustomerYoutubeMainPageComponent } from './children/data-sources/youtube/customer-youtube-main-page/customer-youtube-main-page.component';
import { CustomerYoutubeChannelsLoadedListPageComponent } from './children/data-sources/youtube/customer-youtube-channels-loaded-list-page/customer-youtube-channels-loaded-list-page.component';
import { CustomerYoutubeVideosLoadedListPageComponent } from './children/data-sources/youtube/customer-youtube-videos-loaded-list-page/customer-youtube-videos-loaded-list-page.component';

const routes: Routes = [
  {
    path: '',
    component: CustomerAccountDetailsNavPageComponent,
    children: [
      {
        path: '',
        redirectTo: 'personal-info',
        pathMatch: 'full'
      },
      {
        path: 'personal-info',
        component: CustomerPersonalInformationMainPageComponent,
        children: [
          {
            path: '',
            redirectTo: 'main',
            pathMatch: 'full'
          },
          {
            path: 'main',
            component: CustomerPersonalInformationPageComponent
          }
        ]
      },
      {
        path: 'quotas',
        component: CustomerQuotasMainPageComponent,
        children: [
          {
            path: '',
            redirectTo: 'list',
            pathMatch: 'full'
          },
          {
            path: 'list',
            component: CustomerQuotasListPageComponent
          },
          {
            path: 'logs',
            component: CustomerQuotasLogsPageComponent
          },
          {
            path: 'pack-logs',
            component: CustomerQuotasPackLogsPageComponent
          },
        ]
      },
      {
        path: 'tasks',
        component: CustomerTasksListPageComponent,
        children: [
          {
            path: '',
            redirectTo: 'list',
            pathMatch: 'full'
          },
          {
            path: 'list',
            component: CustomerTasksListComponent
          }
        ]
      },
      {
        path: 'workspaces',
        component: CustomerWorkspacesMainPageComponent,
        children: [
          {
            path: '',
            redirectTo: 'list',
            pathMatch: 'full'
          },
          {
            path: 'list',
            component: CustomerWorkspacesListPageComponent
          }
        ]
      },
      {
        path: 'profiles',
        component: CustomerProfilesMainPageComponent,
        children: [
          {
            path: '',
            redirectTo: 'list',
            pathMatch: 'full'
          },
          {
            path: 'list',
            component: CustomerProfilesListPageComponent
          }
        ]
      },
      {
        path: 'data-sources',
        children: [
          {
            path: '',
            redirectTo: 'youtube',
            pathMatch: 'full'
          },
          {
            path: 'youtube',
            component: CustomerYoutubeMainPageComponent,
            children:[
              {
                path: '',
                redirectTo: 'loaded-channels',
                pathMatch: 'full'
              },
              {
                path:'loaded-channels',
                component:CustomerYoutubeChannelsLoadedListPageComponent
              },
              {
                path:'loaded-videos',
                component:CustomerYoutubeVideosLoadedListPageComponent
              }
            ]
          },
          {
            path: 'telegram',
            component: CustomerTelegramMainPageComponent,
            children:[
              {
                path: '',
                redirectTo: 'loaded-channels',
                pathMatch: 'full'
              },
              {
                path:'loaded-channels',
                component:CustomerTelegramChannelsLoadedListPageComponent
              },
              {
                path:'loaded-messages',
                component:CustomerTelegramMessagesLoadedListPageComponent
              }
            ]
          },
        ]
      },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomerAccountDetailsRoutingModule { }
