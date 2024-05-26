import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TelegramFullChannelPageComponent } from './channels/pages/telegram-full-channel-page/telegram-full-channel-page.component';
import { TelegramChannelListPageComponent } from './channels/pages/telegram-channel-list-page/telegram-channel-list-page.component';
import { CustomerGuard } from 'src/app/core/guard/customer.guard';
import { TelegramMessageListPageComponent } from './messages/pages/telegram-message-list-page/telegram-message-list-page.component';
import { TelegramFullMessagePageComponent } from './messages/pages/telegram-full-message-page/telegram-full-message-page.component';
import { TelegramLoadGroupMessagesPageComponent } from './messages/pages/telegram-load-group-messages-page/telegram-load-group-messages-page.component';
import { TelegramLoadMultipleMessagesComponent } from './messages/components/telegram-load-multiple-messages/telegram-load-multiple-messages.component';
import { TelegramMessagesTasksListPageComponent } from './messages/pages/telegram-messages-tasks-list-page/telegram-messages-tasks-list-page.component';
import { TelegramChannelsTasksListPageComponent } from './channels/pages/telegram-channels-tasks-list-page/telegram-channels-tasks-list-page.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'channels',
    pathMatch: 'full'
  },
  {
    path: 'channels',
    children: [
      {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full'
      },
      {
        path: 'list',
        component: TelegramChannelListPageComponent
      },
      {
        path: 'full/:id',
        component: TelegramFullChannelPageComponent,
        children: [
          {
            path: '',
            redirectTo: 'list',
            pathMatch: 'full'
          },
          {
            path: 'list/:id',
            component: TelegramMessageListPageComponent
          },
          {
            path: 'add-many/:id',
            component: TelegramLoadMultipleMessagesComponent,
            canActivate: [CustomerGuard],
            canActivateChild: [CustomerGuard],
          },
          {
            path: 'tasks/:channelId',
            component: TelegramChannelsTasksListPageComponent
          },
        ]
      }
    ]
  },
  {
    path:'messages',
    children:[
      {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full'
      },
      {
        path: 'list',
        component: TelegramMessageListPageComponent
      },
      {
        path: 'full/:id',
        component: TelegramFullMessagePageComponent,
        children:[
          {
            path: 'tasks/:messageId',
            component: TelegramMessagesTasksListPageComponent
          }
        ]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TelegramRoutingModule { }
