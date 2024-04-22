import { NgModule } from "@angular/core";
import { Route, RouterModule } from "@angular/router";
import { LogInPageComponent } from "src/app/features/account/pages/log-in-page/log-in-page.component";
import { SignUpPageComponent } from "src/app/features/account/pages/sign-up-page/sign-up-page.component";
import { AbstractAlgorithmAddPageComponent } from "src/app/features/clusterization/algorithms/abstractAlgorithm/pages/abstract-algorithm-add-page/abstract-algorithm-add-page.component";
import { ClusterizationFullProfilePageComponent } from "src/app/features/clusterization/profiles/pages/clusterization-full-profile-page/clusterization-full-profile-page.component";
import { ClusterizationProfileAddPageComponent } from "src/app/features/clusterization/profiles/pages/clusterization-profile-add-page/clusterization-profile-add-page.component";
import { ClusterizationProfileListPageComponent } from "src/app/features/clusterization/profiles/pages/clusterization-profile-list-page/clusterization-profile-list-page.component";
import { AddChannelCommentsToWorkspacePageComponent } from "src/app/features/clusterization/workspace/pages/add-channel-comments-to-workspace-page/add-channel-comments-to-workspace-page.component";
import { AddExternalDataToWorkspaceComponent } from "src/app/features/clusterization/workspace/pages/add-external-data-to-workspace/add-external-data-to-workspace.component";
import { AddVideosCommentsToWorkspaceComponent } from "src/app/features/clusterization/workspace/pages/add-videos-comments-to-workspace/add-videos-comments-to-workspace.component";
import { AddWorkspacePageComponent } from "src/app/features/clusterization/workspace/pages/add-workspace-page/add-workspace-page.component";
import { WorkspaceFullPageComponent } from "src/app/features/clusterization/workspace/pages/workspace-full-page/workspace-full-page.component";
import { WorkspaceListPageComponent } from "src/app/features/clusterization/workspace/pages/workspace-list-page/workspace-list-page.component";
import { PointsMapPageComponent } from "src/app/features/points-map/pages/points-map-page/points-map-page.component";
import { YoutubeLoadMultipleChannelsComponent } from "src/app/features/youtube/channels/components/youtube-load-multiple-channels/youtube-load-multiple-channels.component";
import { YoutubeLoadOneChannelComponent } from "src/app/features/youtube/channels/components/youtube-load-one-channel/youtube-load-one-channel.component";
import { YoutubeChannelListPageComponent } from "src/app/features/youtube/channels/pages/youtube-channel-list-page/youtube-channel-list-page.component";
import { YoutubeFullChannelPageComponent } from "src/app/features/youtube/channels/pages/youtube-full-channel-page/youtube-full-channel-page.component";
import { YoutubeLoadNewChannelPageComponent } from "src/app/features/youtube/channels/pages/youtube-load-new-channel-page/youtube-load-new-channel-page.component";
import { YoutubeCommentListPageComponent } from "src/app/features/youtube/comments/pages/youtube-comment-list-page/youtube-comment-list-page.component";
import { YoutubeLoadAllCommentsPageComponent } from "src/app/features/youtube/comments/pages/youtube-load-all-comments-page/youtube-load-all-comments-page.component";
import { YoutubeLoadCommentsByChannelPageComponent } from "src/app/features/youtube/comments/pages/youtube-load-comments-by-channel-page/youtube-load-comments-by-channel-page.component";
import { YoutubeLoadMultipleVideosComponent } from "src/app/features/youtube/videos/components/youtube-load-multiple-videos/youtube-load-multiple-videos.component";
import { YoutubeFullVideoPageComponent } from "src/app/features/youtube/videos/pages/youtube-full-video-page/youtube-full-video-page.component";
import { YoutubeLoadAllVideosPageComponent } from "src/app/features/youtube/videos/pages/youtube-load-all-videos-page/youtube-load-all-videos-page.component";
import { YoutubeVideoListPageComponent } from "src/app/features/youtube/videos/pages/youtube-video-list-page/youtube-video-list-page.component";
import { CustomerGuard } from "../guard/customer.guard";
import { ModeratorGuard } from "../guard/moderator.guard";
import { AddQuatasPackToCustomerPageComponent } from "src/app/features/admin-panel/quotas/page/add-quatas-pack-to-customer-page/add-quatas-pack-to-customer-page.component";
import { FullTaskPageComponent } from "src/app/features/tasks/pages/full-task-page/full-task-page.component";
import { WorkspaceAddDataPackListPageComponent } from "src/app/features/clusterization/workspaceAddDataPacks/pages/workspace-add-data-pack-list-page/workspace-add-data-pack-list-page.component";
import { WorkspaceAddPackFullPageComponent } from "src/app/features/clusterization/workspaceAddDataPacks/pages/workspace-add-pack-full-page/workspace-add-pack-full-page.component";
import { LoadEmbeddingsByPackPageComponent } from "src/app/features/embeddings/pages/load-embeddings-by-pack-page/load-embeddings-by-pack-page.component";
const routes: Route[] = [
  {
    path:'',
    redirectTo:'channel-list',
    pathMatch:'full'
  },
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
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
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
        component:YoutubeLoadMultipleVideosComponent,
        canActivate:[CustomerGuard],
        canActivateChild:[CustomerGuard],
      }
    ]
  },
  {
    path:'load-videos-by-channel/:channelId',
    component:YoutubeLoadAllVideosPageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'load-comments-by-channel/:channelId',
    component:YoutubeLoadCommentsByChannelPageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'video-full-info/:id',
    component:YoutubeFullVideoPageComponent
  },
  {
    path:'load-comments-by-video/:videoId',
    component:YoutubeLoadAllCommentsPageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'workspaces_add',
    component:AddWorkspacePageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
  },
  {
    path:'workspaces',
    component:WorkspaceListPageComponent
  },
  {
    path:'workspaces/full/:id',
    component:WorkspaceFullPageComponent,
    children:[
      {
        path:'profiles-list/:workspaceId',
        component:ClusterizationProfileListPageComponent
      },
      {
        path:'workspace-add-data-packs/list/:workspaceId',
        component:WorkspaceAddDataPackListPageComponent
      }
    ]
  },
  {
    path:'profiles/full/:id',
    component:ClusterizationFullProfilePageComponent,
    children:[
      {
        path:'profile-points-map/:profileId',
        component:PointsMapPageComponent
      }
    ]
  },
  {
    path:'workspaces/add-comments-by-channel/:channelId',
    component:AddChannelCommentsToWorkspacePageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'workspaces/add-comments-by-videos/:channelId',
    component:AddVideosCommentsToWorkspaceComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'algorithms/add',
    component:AbstractAlgorithmAddPageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'profiles/add/:workspaceId',
    component:ClusterizationProfileAddPageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'workspace/add-external-data/:workspaceId',
    component:AddExternalDataToWorkspaceComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'sign-up',
    component:SignUpPageComponent,
    outlet:'overflow'
  },
  {
    path:'log-in',
    component:LogInPageComponent,
    outlet:'overflow'
  },
  {
    path:'documentation',
    loadChildren: () => import('../../features/documentation/documentation.module').then(m => m.DocumentationModule)
  },
  {
    path:'admin-panel',
    loadChildren: () => import('./admin-panel-routing.module').then(m => m.AdminPanelRoutingModule),
    canActivate:[ModeratorGuard],
    canActivateChild:[ModeratorGuard],
  },
  {
    path:'admin-panel/add-quotas-pack/:customerId',
    component:AddQuatasPackToCustomerPageComponent,
    outlet:'overflow'
  },
  {
    path:'customer-details',
    loadChildren: () => import('./customer-details-routing.module').then(m => m.CustomerDetailsRoutingModule),
    canActivate:[CustomerGuard],
  },
  {
    path:'tasks-details/:id',
    component:FullTaskPageComponent,
    outlet:'overflow'
  },
  {
    path:'workspace-add-data-packs/full/:id',
    component:WorkspaceAddPackFullPageComponent,
    outlet:'overflow',
  },
  {
    path:'embeddings-loading/load-by-data-pack/:id',
    component:LoadEmbeddingsByPackPageComponent,
    outlet:'overflow',
  },
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }