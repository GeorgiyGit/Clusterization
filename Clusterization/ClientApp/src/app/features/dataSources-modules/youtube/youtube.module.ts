import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { YoutubeRoutingModule } from './youtube-routing.module';
import { YoutubeChannelCardComponent } from './channels/components/youtube-channel-card/youtube-channel-card.component';
import { YoutubeChannelListComponent } from './channels/components/youtube-channel-list/youtube-channel-list.component';
import { YoutubeVideoCardComponent } from './videos/components/youtube-video-card/youtube-video-card.component';
import { YoutubeVideoListComponent } from './videos/components/youtube-video-list/youtube-video-list.component';
import { YoutubeChannelsSearchFilterComponent } from './channels/components/youtube-channels-search-filter/youtube-channels-search-filter.component';
import { YoutubeChannelListPageComponent } from './channels/pages/youtube-channel-list-page/youtube-channel-list-page.component';
import { YoutubeLoadNewChannelPageComponent } from './channels/pages/youtube-load-new-channel-page/youtube-load-new-channel-page.component';
import { YoutubeCommentCardComponent } from './comments/components/youtube-comment-card/youtube-comment-card.component';
import { YoutubeCommentListComponent } from './comments/components/youtube-comment-list/youtube-comment-list.component';
import { YoutubeCommentListPageComponent } from './comments/pages/youtube-comment-list-page/youtube-comment-list-page.component';
import { YoutubeVideoListPageComponent } from './videos/pages/youtube-video-list-page/youtube-video-list-page.component';
import { YoutubeLoadMultipleChannelsComponent } from './channels/components/youtube-load-multiple-channels/youtube-load-multiple-channels.component';
import { YoutubeLoadOneChannelComponent } from './channels/components/youtube-load-one-channel/youtube-load-one-channel.component';
import { YoutubeFullChannelPageComponent } from './channels/pages/youtube-full-channel-page/youtube-full-channel-page.component';
import { YoutubeLoadMultipleVideosComponent } from './videos/components/youtube-load-multiple-videos/youtube-load-multiple-videos.component';
import { YoutubeLoadOneVideoComponent } from './videos/components/youtube-load-one-video/youtube-load-one-video.component';
import { YoutubeVideosSearchFilterComponent } from './videos/components/youtube-videos-search-filter/youtube-videos-search-filter.component';
import { YoutubeLoadNewVideoPageComponent } from './videos/pages/youtube-load-new-video-page/youtube-load-new-video-page.component';
import { YoutubeLoadAllCommentsPageComponent } from './comments/pages/youtube-load-all-comments-page/youtube-load-all-comments-page.component';
import { YoutubeFullVideoPageComponent } from './videos/pages/youtube-full-video-page/youtube-full-video-page.component';
import { YoutubeLoadAllVideosPageComponent } from './videos/pages/youtube-load-all-videos-page/youtube-load-all-videos-page.component';
import { AddYoutubeChannelCommentsToWorkspacePageComponent } from './youtube-data-objects/pages/add-youtube-channel-comments-to-workspace-page/add-youtube-channel-comments-to-workspace-page.component';
import { YoutubeLoadCommentsByChannelPageComponent } from './comments/pages/youtube-load-comments-by-channel-page/youtube-load-comments-by-channel-page.component';
import { AddYoutubeVideosCommentsToWorkspaceComponent } from './youtube-data-objects/pages/add-youtube-videos-comments-to-workspace/add-youtube-videos-comments-to-workspace.component';
import { CoreModule } from 'src/app/core/core.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTooltipModule } from '@angular/material/tooltip';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
  declarations: [
    YoutubeChannelCardComponent,
    YoutubeChannelListComponent,
    YoutubeVideoListComponent,
    YoutubeVideoCardComponent,
    YoutubeChannelListPageComponent,
    YoutubeVideoListPageComponent,
    YoutubeCommentCardComponent,
    YoutubeCommentListComponent,
    YoutubeCommentListPageComponent,
    YoutubeLoadNewChannelPageComponent,
    YoutubeChannelsSearchFilterComponent,
    YoutubeLoadOneChannelComponent,
    YoutubeLoadMultipleChannelsComponent,
    YoutubeFullChannelPageComponent,
    YoutubeLoadNewVideoPageComponent,
    YoutubeLoadOneVideoComponent,
    YoutubeLoadMultipleVideosComponent,
    YoutubeVideosSearchFilterComponent,
    YoutubeLoadAllVideosPageComponent,
    YoutubeFullVideoPageComponent,
    YoutubeLoadAllCommentsPageComponent,
    AddYoutubeChannelCommentsToWorkspacePageComponent,
    YoutubeLoadCommentsByChannelPageComponent,
    AddYoutubeVideosCommentsToWorkspaceComponent,],
  imports: [
    CommonModule,
    YoutubeRoutingModule,
    CoreModule,
    FormsModule,
    ReactiveFormsModule,
    MatTooltipModule
  ]
})
export class YoutubeModule { }
