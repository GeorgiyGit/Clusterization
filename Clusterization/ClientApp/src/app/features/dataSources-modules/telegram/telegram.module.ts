import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TelegramRoutingModule } from './telegram-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTooltipModule } from '@angular/material/tooltip';
import { CoreModule } from 'src/app/core/core.module';
import { TelegramChannelCardComponent } from './channels/components/telegram-channel-card/telegram-channel-card.component';
import { TelegramChannelListComponent } from './channels/components/telegram-channel-list/telegram-channel-list.component';
import { TelegramChannelsSearchFilterComponent } from './channels/components/telegram-channels-search-filter/telegram-channels-search-filter.component';
import { TelegramLoadMultipleChannelsComponent } from './channels/components/telegram-load-multiple-channels/telegram-load-multiple-channels.component';
import { TelegramLoadOneChannelComponent } from './channels/components/telegram-load-one-channel/telegram-load-one-channel.component';
import { TelegramChannelListPageComponent } from './channels/pages/telegram-channel-list-page/telegram-channel-list-page.component';
import { TelegramFullChannelPageComponent } from './channels/pages/telegram-full-channel-page/telegram-full-channel-page.component';
import { TelegramLoadNewChannelsPageComponent } from './channels/pages/telegram-load-new-channels-page/telegram-load-new-channels-page.component';


@NgModule({
  declarations: [
    TelegramChannelCardComponent,
    TelegramChannelListComponent,
    TelegramChannelListPageComponent,
    TelegramLoadNewChannelsPageComponent,
    TelegramChannelsSearchFilterComponent,
    TelegramLoadOneChannelComponent,
    TelegramLoadMultipleChannelsComponent,
    TelegramFullChannelPageComponent,],
  imports: [
    CommonModule,
    TelegramRoutingModule,
    CoreModule,
    FormsModule,
    ReactiveFormsModule,
    MatTooltipModule
  ]
})
export class TelegramModule { }
