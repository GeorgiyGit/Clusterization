import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TelegramFullChannelPageComponent } from './channels/pages/telegram-full-channel-page/telegram-full-channel-page.component';
import { TelegramChannelListPageComponent } from './channels/pages/telegram-channel-list-page/telegram-channel-list-page.component';
import { CustomerGuard } from 'src/app/core/guard/customer.guard';

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
            component: TelegramChannelListPageComponent
          },
          {
            path: 'add-many/:id',
            component: TelegramChannelListPageComponent,
            canActivate: [CustomerGuard],
            canActivateChild: [CustomerGuard],
          }
        ]
      }
    ]
  },];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TelegramRoutingModule { }
