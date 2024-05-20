import { AddTelegramMessageRepliesToWorkspaceComponent } from './../../features/dataSources-modules/telegram/replies-data-objects/pages/add-telegram-message-replies-to-workspace/add-telegram-message-replies-to-workspace.component';
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
import { TelegramLoadGroupMessagesPageComponent } from "src/app/features/dataSources-modules/telegram/messages/pages/telegram-load-group-messages-page/telegram-load-group-messages-page.component";
import { TelegramLoadGroupRepliesPageComponent } from "src/app/features/dataSources-modules/telegram/replies/pages/telegram-load-group-replies-page/telegram-load-group-replies-page.component";
import { TelegramLoadRepliesByChannelPageComponent } from "src/app/features/dataSources-modules/telegram/replies/pages/telegram-load-replies-by-channel-page/telegram-load-replies-by-channel-page.component";
import { AddTGMsgsToWorkspaceByChannelPageComponent } from "src/app/features/dataSources-modules/telegram/messages-data-objects/pages/add-tgmsgs-to-workspace-by-channel-page/add-tgmsgs-to-workspace-by-channel-page.component";
import { AddTelegramChannelRepliesToWorkspacePageComponent } from 'src/app/features/dataSources-modules/telegram/replies-data-objects/pages/add-telegram-channel-replies-to-workspace-page/add-telegram-channel-replies-to-workspace-page.component';
import { EmailVerificationPageComponent } from 'src/app/features/shared-module/account/pages/email-verification-page/email-verification-page.component';
import { EmailVerificationAlertPageComponent } from 'src/app/features/shared-module/account/pages/email-verification-alert-page/email-verification-alert-page.component';
import { LoadExternalObjectsPageComponent } from 'src/app/features/dataSources-modules/external-data/external-objects-packs/pages/load-external-objects-page/load-external-objects-page.component';
import { LoadAndAddExternalObjectsPageComponent } from 'src/app/features/dataSources-modules/external-data/external-objects-packs/pages/load-and-add-external-objects-page/load-and-add-external-objects-page.component';
import { ExternalDataLayoutComponent } from '../components/layouts/external-data-layout/external-data-layout.component';
import { UpdateWorkspacePageComponent } from 'src/app/features/clusterization-module/clusterization/workspace/pages/update-workspace-page/update-workspace-page.component';
import { UpdateExternalObjectsPackPageComponent } from 'src/app/features/dataSources-modules/external-data/external-objects-packs/pages/update-external-objects-pack-page/update-external-objects-pack-page.component';
import { DataObjectFullPageComponent } from 'src/app/features/clusterization-module/data-objects/pages/data-object-full-page/data-object-full-page.component';
import { LoadClustersFilePageComponent } from 'src/app/features/clusterization-module/clusters/pages/load-clusters-file-page/load-clusters-file-page.component';
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
    path: 'externalData-layout',
    component: ExternalDataLayoutComponent,
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
        path: 'dataSources/externalData',
        loadChildren: () => import('../../features/dataSources-modules/external-data/external-data.module').then(m => m.ExternalDataModule)
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
    path: 'clusterization/workspaces/update/:id',
    component: UpdateWorkspacePageComponent,
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
    path: 'clusterization/dataObjects/full/:id',
    component: DataObjectFullPageComponent,
    outlet: 'overflow',
    canActivate: [CustomerGuard],
  },
  {
    path: 'clusterization/clusters/download-file/:profileId',
    component: LoadClustersFilePageComponent,
    outlet: 'overflow',
    canActivate: [CustomerGuard],
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
  {
    path:'dataSources/telegram/messages/load-many-by-channel/:channelId',
    component:TelegramLoadGroupMessagesPageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'dataSources/telegram/replies/load-by-channel/:channelId',
    component:TelegramLoadRepliesByChannelPageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'dataSources/telegram/replies/load-by-message/:messageId',
    component:TelegramLoadGroupRepliesPageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'dataSources/telegram/add-data-objects/add-messages-by-channel/:channelId',
    component:AddTGMsgsToWorkspaceByChannelPageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'dataSources/telegram/add-data-objects/add-replies-by-channel/:channelId',
    component:AddTelegramChannelRepliesToWorkspacePageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'dataSources/telegram/add-data-objects/add-replies-by-messages/:channelId',
    component:AddTelegramMessageRepliesToWorkspaceComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'confirm-email',
    component:EmailVerificationPageComponent,
    outlet:'overflow'
  },
  {
    path:'confirm-email-alert',
    component:EmailVerificationAlertPageComponent,
    outlet:'overflow'
  },
  {
    path:'dataSources/externalData/load-objects',
    component:LoadExternalObjectsPageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path:'dataSources/externalData/load-and-add-data-objects/:workspaceId',
    component:LoadAndAddExternalObjectsPageComponent,
    outlet:'overflow',
    canActivate:[CustomerGuard],
    canActivateChild:[CustomerGuard],
  },
  {
    path: 'dataSources/externalData/packs/update/:id',
    component: UpdateExternalObjectsPackPageComponent,
    outlet: 'overflow',
    canActivate: [CustomerGuard],
  },
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }