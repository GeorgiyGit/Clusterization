import { NgModule } from "@angular/core";
import { Route, RouterModule } from "@angular/router";
import { TaskListPageComponent } from "src/app/features/tasks/pages/task-list-page/task-list-page.component";
import { YoutubeLoadMultipleChannelsComponent } from "src/app/features/youtube/channels/components/youtube-load-multiple-channels/youtube-load-multiple-channels.component";
import { YoutubeLoadOneChannelComponent } from "src/app/features/youtube/channels/components/youtube-load-one-channel/youtube-load-one-channel.component";
import { YoutubeChannelListPageComponent } from "src/app/features/youtube/channels/pages/youtube-channel-list-page/youtube-channel-list-page.component";
import { YoutubeFullChannelPageComponent } from "src/app/features/youtube/channels/pages/youtube-full-channel-page/youtube-full-channel-page.component";
import { YoutubeLoadNewChannelPageComponent } from "src/app/features/youtube/channels/pages/youtube-load-new-channel-page/youtube-load-new-channel-page.component";
import { YoutubeCommentListPageComponent } from "src/app/features/youtube/comments/pages/youtube-comment-list-page/youtube-comment-list-page.component";
import { YoutubeLoadAllCommentsPageComponent } from "src/app/features/youtube/comments/pages/youtube-load-all-comments-page/youtube-load-all-comments-page.component";
import { YoutubeLoadMultipleVideosComponent } from "src/app/features/youtube/videos/components/youtube-load-multiple-videos/youtube-load-multiple-videos.component";
import { YoutubeFullVideoPageComponent } from "src/app/features/youtube/videos/pages/youtube-full-video-page/youtube-full-video-page.component";
import { YoutubeLoadAllVideosPageComponent } from "src/app/features/youtube/videos/pages/youtube-load-all-videos-page/youtube-load-all-videos-page.component";
import { YoutubeVideoListPageComponent } from "src/app/features/youtube/videos/pages/youtube-video-list-page/youtube-video-list-page.component";
const routes: Route[] = [
  {
      path:'channel-list',
      component:YoutubeChannelListPageComponent
  },
  {
    path:'video-list',
    component:YoutubeVideoListPageComponent
  },
  {
    path:'comments-list',
    component:YoutubeCommentListPageComponent
  },
  {
    path:'load-channel',
    component:YoutubeLoadNewChannelPageComponent,
    outlet:'overflow',
    children:[
      {
        path:'',
        redirectTo:'by_id',
        pathMatch:'full'
      },
      {
        path:'by_id',
        component:YoutubeLoadOneChannelComponent
      },
      {
        path:'by_name',
        component:YoutubeLoadMultipleChannelsComponent
      }
    ]
  },
  {
    path:'channel-full-info/:id',
    component:YoutubeFullChannelPageComponent,
    children:[
      {
        path:'',
        redirectTo:'list',
        pathMatch:'full'
      },
      {
        path:'list/:id',
        component:YoutubeVideoListPageComponent
      },
      {
        path:'add_many/:id',
        component:YoutubeLoadMultipleVideosComponent
      }
    ]
  },
  {
    path:'tasks-list',
    component:TaskListPageComponent
  },
  {
    path:'load-videos-by-channel/:channelId',
    component:YoutubeLoadAllVideosPageComponent,
    outlet:'overflow'
  },
  {
    path:'video-full-info/:id',
    component:YoutubeFullVideoPageComponent
  },
  {
    path:'load-comments-by-video/:videoId',
    component:YoutubeLoadAllCommentsPageComponent,
    outlet:'overflow'
  }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }