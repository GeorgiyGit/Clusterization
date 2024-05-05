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
import { TelegramLoadMultipleMessagesComponent } from './messages/components/telegram-load-multiple-messages/telegram-load-multiple-messages.component';
import { TelegramMessageCardComponent } from './messages/components/telegram-message-card/telegram-message-card.component';
import { TelegramMessageListComponent } from './messages/components/telegram-message-list/telegram-message-list.component';
import { TelegramFullMessagePageComponent } from './messages/pages/telegram-full-message-page/telegram-full-message-page.component';
import { TelegramLoadGroupMessagesPageComponent } from './messages/pages/telegram-load-group-messages-page/telegram-load-group-messages-page.component';
import { TelegramMessageListPageComponent } from './messages/pages/telegram-message-list-page/telegram-message-list-page.component';
import { TelegramMessagesSearchFilterComponent } from './messages/components/telegram-messages-search-filter/telegram-messages-search-filter.component';
import { TelegramLoadGroupRepliesPageComponent } from './replies/pages/telegram-load-group-replies-page/telegram-load-group-replies-page.component';
import { TelegramLoadRepliesByChannelPageComponent } from './replies/pages/telegram-load-replies-by-channel-page/telegram-load-replies-by-channel-page.component';
import { AddTGMsgsToWorkspaceByChannelPageComponent } from './messages-data-objects/pages/add-tgmsgs-to-workspace-by-channel-page/add-tgmsgs-to-workspace-by-channel-page.component';
import { AddTelegramMessageRepliesToWorkspaceComponent } from './replies-data-objects/pages/add-telegram-message-replies-to-workspace/add-telegram-message-replies-to-workspace.component';
import { AddTelegramChannelRepliesToWorkspacePageComponent } from './replies-data-objects/pages/add-telegram-channel-replies-to-workspace-page/add-telegram-channel-replies-to-workspace-page.component';


@NgModule({
  declarations: [
    TelegramChannelCardComponent,
    TelegramChannelListComponent,
    TelegramChannelListPageComponent,
    TelegramLoadNewChannelsPageComponent,
    TelegramChannelsSearchFilterComponent,
    TelegramLoadOneChannelComponent,
    TelegramLoadMultipleChannelsComponent,
    TelegramFullChannelPageComponent,
  
    TelegramLoadMultipleMessagesComponent,
    TelegramMessageCardComponent,
    TelegramMessageListComponent,
    TelegramMessagesSearchFilterComponent,
    TelegramFullMessagePageComponent,
    TelegramLoadGroupMessagesPageComponent,
    TelegramMessageListPageComponent,

    TelegramLoadRepliesByChannelPageComponent,
    TelegramLoadGroupRepliesPageComponent,

    AddTGMsgsToWorkspaceByChannelPageComponent,
    AddTelegramChannelRepliesToWorkspacePageComponent,
    AddTelegramMessageRepliesToWorkspaceComponent
  ],
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
