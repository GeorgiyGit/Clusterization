import { NgModule } from "@angular/core";
import { Route, RouterModule } from "@angular/router";
import { CustomerGuard } from "../guard/customer.guard";
import { ModeratorGuard } from "../guard/moderator.guard";
import { LogInPageComponent } from "src/app/features/shared-module/account/pages/log-in-page/log-in-page.component";
import { SignUpPageComponent } from "src/app/features/shared-module/account/pages/sign-up-page/sign-up-page.component";
import { FullTaskPageComponent } from "src/app/features/shared-module/tasks/pages/full-task-page/full-task-page.component";
import { MainLayoutComponent } from "../components/layouts/main-layout/main-layout.component";
import { AbstractAlgorithmAddPageComponent } from "src/app/features/clusterization-module/clusterization/algorithms/abstractAlgorithm/pages/abstract-algorithm-add-page/abstract-algorithm-add-page.component";
import { ClusterizationProfileAddPageComponent } from "src/app/features/clusterization-module/clusterization/profiles/pages/clusterization-profile-add-page/clusterization-profile-add-page.component";
import { AddWorkspacePageComponent } from "src/app/features/clusterization-module/clusterization/workspace/pages/add-workspace-page/add-workspace-page.component";
import { WorkspaceAddPackFullPageComponent } from "src/app/features/clusterization-module/clusterization/workspaceAddDataPacks/pages/workspace-add-pack-full-page/workspace-add-pack-full-page.component";
import { LoadEmbeddingsByPackPageComponent } from "src/app/features/clusterization-module/embeddings/pages/load-embeddings-by-pack-page/load-embeddings-by-pack-page.component";
import { AddQuatasPackToCustomerPageComponent } from "src/app/features/admin-panel-module/quotas/page/add-quatas-pack-to-customer-page/add-quatas-pack-to-customer-page.component";
import { YoutubeLoadMultipleChannelsComponent } from "src/app/features/dataSources-modules/youtube/channels/components/youtube-load-multiple-channels/youtube-load-multiple-channels.component";
import { YoutubeLoadOneChannelComponent } from "src/app/features/dataSources-modules/youtube/channels/components/youtube-load-one-channel/youtube-load-one-channel.component";
import { YoutubeLoadNewChannelPageComponent } from "src/app/features/dataSources-modules/youtube/channels/pages/youtube-load-new-channel-page/youtube-load-new-channel-page.component";
import { YoutubeLoadAllCommentsPageComponent } from "src/app/features/dataSources-modules/youtube/comments/pages/youtube-load-all-comments-page/youtube-load-all-comments-page.component";
import { YoutubeLoadCommentsByChannelPageComponent } from "src/app/features/dataSources-modules/youtube/comments/pages/youtube-load-comments-by-channel-page/youtube-load-comments-by-channel-page.component";
import { YoutubeLoadAllVideosPageComponent } from "src/app/features/dataSources-modules/youtube/videos/pages/youtube-load-all-videos-page/youtube-load-all-videos-page.component";
import { AddYoutubeChannelCommentsToWorkspacePageComponent } from "src/app/features/dataSources-modules/youtube/youtube-data-objects/pages/add-youtube-channel-comments-to-workspace-page/add-youtube-channel-comments-to-workspace-page.component";
import { AddYoutubeVideosCommentsToWorkspaceComponent } from "src/app/features/dataSources-modules/youtube/youtube-data-objects/pages/add-youtube-videos-comments-to-workspace/add-youtube-videos-comments-to-workspace.component";
import { YoutubeLayoutComponent } from "../components/layouts/youtube-layout/youtube-layout.component";
import { TelegramLayoutComponent } from "../components/layouts/telegram-layout/telegram-layout.component";
import { TelegramLoadNewChannelsPageComponent } from "src/app/features/dataSources-modules/telegram/channels/pages/telegram-load-new-channels-page/telegram-load-new-channels-page.component";
import { TelegramLoadOneChannelComponent } from "src/app/features/dataSources-modules/telegram/channels/components/telegram-load-one-channel/telegram-load-one-channel.component";
import { TelegramLoadMultipleChannelsComponent } from "src/app/features/dataSources-modules/telegram/channels/components/telegram-load-multiple-channels/telegram-load-multiple-channels.component";
const routes: Route[] = [
  {
    path: '',
    redirectTo: 'main-layout/clusterization/workspaces/list',
    pathMatch: 'full'
  },
  {
    path: 'main-layout',
    component: MainLayoutComponent,
    children: [
      {
        path: 'admin-panel',
        loadChildren: () => import('../../features/admin-panel-module/admin-panel.module').then(m => m.AdminPanelModule),
        canActivate: [ModeratorGuard],
        canActivateChild: [ModeratorGuard],
      },
      {
        path: 'clusterization',
        loadChildren: () => import('../../features/clusterization-module/clusterization.module').then(m => m.ClusterizationModule)
      },
      {
        path: 'account',
        loadChildren: () => import('../../features/customer-account-details-module/customer-account-details.module').then(m => m.CustomerAccountDetailsModule),
        canActivate: [CustomerGuard],
        canActivateChild: [CustomerGuard],
      },
      {
        path: 'documentation',
        loadChildren: () => import('../../features/documentation-module/documentation.module').then(m => m.DocumentationModule)
      }
    ]
  },
  {
    path: 'youtube-layout',
    component: YoutubeLayoutComponent,
    children: [
      {
        path: 'admin-panel',
        loadChildren: () => import('../../features/admin-panel-module/admin-panel.module').then(m => m.AdminPanelModule),
        canActivate: [ModeratorGuard],
        canActivateChild: [ModeratorGuard],
      },
      {
        path: 'account',
        loadChildren: () => import('../../features/customer-account-details-module/customer-account-details.module').then(m => m.CustomerAccountDetailsModule),
        canActivate: [CustomerGuard],
        canActivateChild: [CustomerGuard],
      },
      {
        path: 'documentation',
        loadChildren: () => import('../../features/documentation-module/documentation.module').then(m => m.DocumentationModule)
      },
      {
        path: 'dataSources/youtube',
        loadChildren: () => import('../../features/dataSources-modules/youtube/youtube.module').then(m => m.YoutubeModule)
      },
    ]
  },
  {
    path: 'telegram-layout',
    component: TelegramLayoutComponent,
    children: [
      {
        path: 'admin-panel',
        loadChildren: () => import('../../features/admin-panel-module/admin-panel.module').then(m => m.AdminPanelModule),
        canActivate: [ModeratorGuard],
        canActivateChild: [ModeratorGuard],
      },
      {
        path: 'account',
        loadChildren: () => import('../../features/customer-account-details-module/customer-account-details.module').then(m => m.CustomerAccountDetailsModule),
        canActivate: [CustomerGuard],
        canActivateChild: [CustomerGuard],
      },
      {
        path: 'documentation',
        loadChildren: () => import('../../features/documentation-module/documentation.module').then(m => m.DocumentationModule)
      },
      {
        path: 'dataSources/telegram',
        loadChildren: () => import('../../features/dataSources-modules/telegram/telegram.module').then(m => m.TelegramModule)
      },
    ]
  },
  {
    path: 'sign-up',
    component: SignUpPageComponent,
    outlet: 'overflow'
  },
  {
    path: 'log-in',
    component: LogInPageComponent,
    outlet: 'overflow'
  },
  {
    path: 'tasks/full/:id',
    component: FullTaskPageComponent,
    outlet: 'overflow'
  },
  {
    path:'admin-panel/add-quotas-pack/:customerId',
    component:AddQuatasPackToCustomerPageComponent,
    outlet:'overflow'
  },
  {
    path: 'clusterization/workspaces/add',
    component: AddWorkspacePageComponent,
    outlet: 'overflow',
    canActivate: [CustomerGuard],
  },
  {
    path: 'clusterization/workspace-add-data-packs/full/:id',
    component: WorkspaceAddPackFullPageComponent,
    outlet: 'overflow',
  },
  {
    path: 'clusterization/embeddings-loading/load-by-data-pack/:id',
    component: LoadEmbeddingsByPackPageComponent,
    outlet: 'overflow',
  },
  {
    path: 'clusterization/algorithms/add',
    component: AbstractAlgorithmAddPageComponent,
    outlet: 'overflow',
    canActivate: [CustomerGuard],
    canActivateChild: [CustomerGuard],
  },
  {
    path: 'clusterization/profiles/add/:workspaceId',
    component: ClusterizationProfileAddPageComponent,
    outlet: 'overflow',
    canActivate: [CustomerGuard],
    canActivateChild: [CustomerGuard],
  },
  {
    path:'dataSources/youtube/channels/load',
    component:YoutubeLoadNewChannelPageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
    children:[
      {
        path:'',
        redirectTo:'by-id',
        pathMatch:'full'
      },
      {
        path:'by-id',
        component:YoutubeLoadOneChannelComponent
      },
      {
        path:'by-name',
        component:YoutubeLoadMultipleChannelsComponent
      },
    ]
  },
  {
    path:'dataSources/youtube/videos/load-many-by-channel/:channelId',
    component:YoutubeLoadAllVideosPageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'dataSources/youtube/comments/load-by-channel/:channelId',
    component:YoutubeLoadCommentsByChannelPageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'dataSources/youtube/comments/load-by-video/:videoId',
    component:YoutubeLoadAllCommentsPageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'dataSources/youtube/add-data-objects/add-comments-by-channel/:channelId',
    component:AddYoutubeChannelCommentsToWorkspacePageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'dataSources/youtube/add-data-objects/add-comments-by-videos/:channelId',
    component:AddYoutubeVideosCommentsToWorkspaceComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  /*{
    path:'workspace/add-external-data/:workspaceId',
    component:AddExternalDataToWorkspaceComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },*/
  {
    path:'dataSources/telegram/channels/load',
    component:TelegramLoadNewChannelsPageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
    children:[
      {
        path:'',
        redirectTo:'by-username',
        pathMatch:'full'
      },
      {
        path:'by-username',
        component:TelegramLoadOneChannelComponent
      },
      {
        path:'by-name',
        component:TelegramLoadMultipleChannelsComponent
      },
    ]
  },
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }