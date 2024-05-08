import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { YoutubeChannelListPageComponent } from './channels/pages/youtube-channel-list-page/youtube-channel-list-page.component';
import { CustomerGuard } from 'src/app/core/guard/customer.guard';
import { YoutubeFullChannelPageComponent } from './channels/pages/youtube-full-channel-page/youtube-full-channel-page.component';
import { YoutubeLoadMultipleVideosComponent } from './videos/components/youtube-load-multiple-videos/youtube-load-multiple-videos.component';
import { YoutubeVideoListPageComponent } from './videos/pages/youtube-video-list-page/youtube-video-list-page.component';
import { YoutubeFullVideoPageComponent } from './videos/pages/youtube-full-video-page/youtube-full-video-page.component';
import { YoutubeCommentListPageComponent } from './comments/pages/youtube-comment-list-page/youtube-comment-list-page.component';

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
