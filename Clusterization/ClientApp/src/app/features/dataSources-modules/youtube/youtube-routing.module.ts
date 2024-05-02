import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { YoutubeChannelListPageComponent } from './channels/pages/youtube-channel-list-page/youtube-channel-list-page.component';
import { CustomerGuard } from 'src/app/core/guard/customer.guard';
import { YoutubeFullChannelPageComponent } from './channels/pages/youtube-full-channel-page/youtube-full-channel-page.component';
import { YoutubeLoadMultipleVideosComponent } from './videos/components/youtube-load-multiple-videos/youtube-load-multiple-videos.component';
import { YoutubeVideoListPageComponent } from './videos/pages/youtube-video-list-page/youtube-video-list-page.component';
import { YoutubeLoadMultipleChannelsComponent } from './channels/components/youtube-load-multiple-channels/youtube-load-multiple-channels.component';
import { YoutubeLoadOneChannelComponent } from './channels/components/youtube-load-one-channel/youtube-load-one-channel.component';
import { YoutubeLoadNewChannelPageComponent } from './channels/pages/youtube-load-new-channel-page/youtube-load-new-channel-page.component';
import { YoutubeFullVideoPageComponent } from './videos/pages/youtube-full-video-page/youtube-full-video-page.component';
import { YoutubeLoadAllVideosPageComponent } from './videos/pages/youtube-load-all-videos-page/youtube-load-all-videos-page.component';
import { YoutubeCommentListPageComponent } from './comments/pages/youtube-comment-list-page/youtube-comment-list-page.component';
import { YoutubeLoadCommentsByChannelPageComponent } from './comments/pages/youtube-load-comments-by-channel-page/youtube-load-comments-by-channel-page.component';
import { YoutubeLoadAllCommentsPageComponent } from './comments/pages/youtube-load-all-comments-page/youtube-load-all-comments-page.component';
import { AddYoutubeChannelCommentsToWorkspacePageComponent } from './youtube-data-objects/pages/add-youtube-channel-comments-to-workspace-page/add-youtube-channel-comments-to-workspace-page.component';
import { AddYoutubeVideosCommentsToWorkspaceComponent } from './youtube-data-objects/pages/add-youtube-videos-comments-to-workspace/add-youtube-videos-comments-to-workspace.component';

const routes: Routes = [
  {
    path:'',
    redirectTo:'channels',
    pathMatch:'full'
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
        component: YoutubeChannelListPageComponent
      },
      {
        path: 'full/:id',
        component: YoutubeFullChannelPageComponent,
        children: [
          {
            path: '',
            redirectTo: 'list',
            pathMatch: 'full'
          },
          {
            path: 'list/:id',
            component: YoutubeVideoListPageComponent
          },
          {
            path: 'add-many/:id',
            component: YoutubeLoadMultipleVideosComponent,
            canActivate: [CustomerGuard],
            canActivateChild: [CustomerGuard],
          }
        ]
      }
    ]
  },
  {
    path:'videos',
    children:[
      {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full'
      },
      {
        path: 'list',
        component: YoutubeVideoListPageComponent
      },
      {
        path: 'full/:id',
        component: YoutubeFullVideoPageComponent
      }
    ]
  },
  {
    path:'comments',
    children:[
      {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full'
      },
      {
        path: 'list',
        component: YoutubeCommentListPageComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class YoutubeRoutingModule { }
