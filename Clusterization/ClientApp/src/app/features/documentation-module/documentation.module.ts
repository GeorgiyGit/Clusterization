import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DocumentationRoutingModule } from './documentation-routing.module';
import { DocAlgorithmsMainPageComponent } from './children/algorithms/pages/doc-algorithms-main-page/doc-algorithms-main-page.component';
import { DocAlgorithmsOneClusterPageComponent } from './children/algorithms/pages/doc-algorithms-one-cluster-page/doc-algorithms-one-cluster-page.component';
import { DocAlgorithmsKMeansPageComponent } from './children/algorithms/pages/doc-algorithms-k-means-page/doc-algorithms-k-means-page.component';
import { DocAlgorithmsDbscanPageComponent } from './children/algorithms/pages/doc-algorithms-dbscan-page/doc-algorithms-dbscan-page.component';
import { DocAlgorithmsGaussianMixturePageComponent } from './children/algorithms/pages/doc-algorithms-gaussian-mixture-page/doc-algorithms-gaussian-mixture-page.component';
import { DocAlgorithmsSpectralClusteringPageComponent } from './children/algorithms/pages/doc-algorithms-spectral-clustering-page/doc-algorithms-spectral-clustering-page.component';
import { DocEmbeddingsMainPageComponent } from './children/embeddings/pages/doc-embeddings-main-page/doc-embeddings-main-page.component';
import { DocInfoMainPageComponent } from './children/pages/doc-info-main-page/doc-info-main-page.component';
import { DocWorkspacesMainPageComponent } from './children/workspaces/pages/doc-workspaces-main-page/doc-workspaces-main-page.component';
import { DocWorkspacesCreationPageComponent } from './children/workspaces/pages/doc-workspaces-creation-page/doc-workspaces-creation-page.component';
import { DocWorkspacesLoadingEmbeddingsPageComponent } from './children/workspaces/pages/doc-workspaces-loading-embeddings-page/doc-workspaces-loading-embeddings-page.component';
import { DocWorkspacesDisplayingPageComponent } from './children/workspaces/pages/doc-workspaces-displaying-page/doc-workspaces-displaying-page.component';
import { DocProfilesMainPageComponent } from './children/profiles/pages/doc-profiles-main-page/doc-profiles-main-page.component';
import { DocProfilesCreationPageComponent } from './children/profiles/pages/doc-profiles-creation-page/doc-profiles-creation-page.component';
import { DocProfilesClusterizationPageComponent } from './children/profiles/pages/doc-profiles-clusterization-page/doc-profiles-clusterization-page.component';
import { DocProfilesDisplayingPageComponent } from './children/profiles/pages/doc-profiles-displaying-page/doc-profiles-displaying-page.component';
import { DocPointsMapMainPageComponent } from './children/points-map/pages/doc-points-map-main-page/doc-points-map-main-page.component';
import { DocPointsMapDynamicLoadingPageComponent } from './children/points-map/pages/doc-points-map-dynamic-loading-page/doc-points-map-dynamic-loading-page.component';
import { DocNavigationPageComponent } from './pages/doc-navigation-page/doc-navigation-page.component';
import { DocYoutubeChannelsDisplayingPageComponent } from './children/dataSources/youtube/channels/pages/doc-youtube-channels-displaying-page/doc-youtube-channels-displaying-page.component';
import { DocYoutubeChannelsLoadingByIdPageComponent } from './children/dataSources/youtube/channels/pages/doc-youtube-channels-loading-by-id-page/doc-youtube-channels-loading-by-id-page.component';
import { DocYoutubeChannelsLoadingByNamePageComponent } from './children/dataSources/youtube/channels/pages/doc-youtube-channels-loading-by-name-page/doc-youtube-channels-loading-by-name-page.component';
import { DocYoutubeChannelsMainPageComponent } from './children/dataSources/youtube/channels/pages/doc-youtube-channels-main-page/doc-youtube-channels-main-page.component';
import { DocYoutubeCommentsLoadFromChannelPageComponent } from './children/dataSources/youtube/comments/pages/doc-youtube-comments-load-from-channel-page/doc-youtube-comments-load-from-channel-page.component';
import { DocYoutubeCommentsLoadFromVideoPageComponent } from './children/dataSources/youtube/comments/pages/doc-youtube-comments-load-from-video-page/doc-youtube-comments-load-from-video-page.component';
import { DocYoutubeCommentsMainPageComponent } from './children/dataSources/youtube/comments/pages/doc-youtube-comments-main-page/doc-youtube-comments-main-page.component';
import { DocYoutubeMainPageComponent } from './children/dataSources/youtube/pages/doc-youtube-main-page/doc-youtube-main-page.component';
import { DocYoutubeVideosDisplayingPageComponent } from './children/dataSources/youtube/videos/pages/doc-youtube-videos-displaying-page/doc-youtube-videos-displaying-page.component';
import { DocYoutubeVideosLoadingByIdsPageComponent } from './children/dataSources/youtube/videos/pages/doc-youtube-videos-loading-by-ids-page/doc-youtube-videos-loading-by-ids-page.component';
import { DocYoutubeVideosLoadingManyPageComponent } from './children/dataSources/youtube/videos/pages/doc-youtube-videos-loading-many-page/doc-youtube-videos-loading-many-page.component';
import { DocYoutubeVideosMainPageComponent } from './children/dataSources/youtube/videos/pages/doc-youtube-videos-main-page/doc-youtube-videos-main-page.component';
import { DocQuotasMainPageComponent } from './children/quotas/pages/doc-quotas-main-page/doc-quotas-main-page.component';
import { DocQuotasPacksPageComponent } from './children/quotas/pages/doc-quotas-packs-page/doc-quotas-packs-page.component';
import { DocCustomerQuotasPageComponent } from './children/quotas/pages/doc-customer-quotas-page/doc-customer-quotas-page.component';
import { DocQuotasLogsPageComponent } from './children/quotas/pages/doc-quotas-logs-page/doc-quotas-logs-page.component';
import { DocQuotasPricesPageComponent } from './children/quotas/pages/doc-quotas-prices-page/doc-quotas-prices-page.component';
import { DocTelegramChannelsDisplayingPageComponent } from './children/dataSources/telegram/channels/doc-telegram-channels-displaying-page/doc-telegram-channels-displaying-page.component';
import { DocTelegramChannelsLoadingByNamePageComponent } from './children/dataSources/telegram/channels/doc-telegram-channels-loading-by-name-page/doc-telegram-channels-loading-by-name-page.component';
import { DocTelegramChannelsMainPageComponent } from './children/dataSources/telegram/channels/doc-telegram-channels-main-page/doc-telegram-channels-main-page.component';
import { DocTelegramChannelsLoadingByUsernamePageComponent } from './children/dataSources/telegram/channels/doc-telegram-channels-loading-by-username-page/doc-telegram-channels-loading-by-username-page.component';
import { DocTelegramMessagesDisplayingPageComponent } from './children/dataSources/telegram/messages/doc-telegram-messages-displaying-page/doc-telegram-messages-displaying-page.component';
import { DocTelegramMessagesLoadingByIdsPageComponent } from './children/dataSources/telegram/messages/doc-telegram-messages-loading-by-ids-page/doc-telegram-messages-loading-by-ids-page.component';
import { DocTelegramMessagesLoadingGroupPageComponent } from './children/dataSources/telegram/messages/doc-telegram-messages-loading-group-page/doc-telegram-messages-loading-group-page.component';
import { DocTelegramMessagesMainPageComponent } from './children/dataSources/telegram/messages/doc-telegram-messages-main-page/doc-telegram-messages-main-page.component';
import { DocTelegramRepliesLoadFromChannelPageComponent } from './children/dataSources/telegram/replies/doc-telegram-replies-load-from-channel-page/doc-telegram-replies-load-from-channel-page.component';
import { DocTelegramRepliesLoadFromMsgPageComponent } from './children/dataSources/telegram/replies/doc-telegram-replies-load-from-msg-page/doc-telegram-replies-load-from-msg-page.component';
import { DocTelegramRepliesMainPageComponent } from './children/dataSources/telegram/replies/doc-telegram-replies-main-page/doc-telegram-replies-main-page.component';
import { DocTelegramAddDataObjectsMainPageComponent } from './children/dataSources/telegram/add-data-objects/doc-telegram-add-data-objects-main-page/doc-telegram-add-data-objects-main-page.component';
import { DocEmbeddingModelsMainPageComponent } from './children/embedding-models/doc-embedding-models-main-page/doc-embedding-models-main-page.component';
import { DocYoutubeAddDataObjectsMainPageComponent } from './children/dataSources/youtube/add-data-objects/doc-youtube-add-data-objects-main-page/doc-youtube-add-data-objects-main-page.component';
import { DocWorkspacesAddDataObjectsPageComponent } from './children/workspaces/pages/doc-workspaces-add-data-objects-page/doc-workspaces-add-data-objects-page.component';
import { DocExternalObjectsMainPageComponent } from './children/dataSources/externalData/doc-external-objects-main-page/doc-external-objects-main-page.component';
import { DocExternalDataMainPageComponent } from './children/dataSources/externalData/doc-external-data-main-page/doc-external-data-main-page.component';
import { DocExternalObjectsPacksMainPageComponent } from './children/dataSources/externalData/doc-external-objects-packs-main-page/doc-external-objects-packs-main-page.component';


@NgModule({
  declarations: [
    DocYoutubeMainPageComponent,

    DocYoutubeChannelsMainPageComponent,
    DocYoutubeChannelsLoadingByIdPageComponent,
    DocYoutubeChannelsLoadingByNamePageComponent,
    DocYoutubeChannelsDisplayingPageComponent,

    DocYoutubeVideosMainPageComponent,
    DocYoutubeVideosLoadingByIdsPageComponent,
    DocYoutubeVideosLoadingManyPageComponent,
    DocYoutubeVideosDisplayingPageComponent,

    DocYoutubeCommentsMainPageComponent,
    DocYoutubeCommentsLoadFromVideoPageComponent,
    DocYoutubeCommentsLoadFromChannelPageComponent,

    DocYoutubeAddDataObjectsMainPageComponent,

    DocAlgorithmsMainPageComponent,
    DocAlgorithmsOneClusterPageComponent,
    DocAlgorithmsKMeansPageComponent,
    DocAlgorithmsDbscanPageComponent,
    DocAlgorithmsGaussianMixturePageComponent,
    DocAlgorithmsSpectralClusteringPageComponent,

    DocEmbeddingsMainPageComponent,

    DocInfoMainPageComponent,

    DocWorkspacesMainPageComponent,
    DocWorkspacesCreationPageComponent,
    DocWorkspacesLoadingEmbeddingsPageComponent,
    DocWorkspacesDisplayingPageComponent,
    DocWorkspacesAddDataObjectsPageComponent,

    DocProfilesMainPageComponent,
    DocProfilesCreationPageComponent,
    DocProfilesClusterizationPageComponent,
    DocProfilesDisplayingPageComponent,

    DocPointsMapMainPageComponent,
    DocPointsMapDynamicLoadingPageComponent,

    DocNavigationPageComponent,

    DocQuotasPricesPageComponent,
    DocQuotasLogsPageComponent,
    DocQuotasMainPageComponent,
    DocQuotasPacksPageComponent,
    DocCustomerQuotasPageComponent,

    DocTelegramChannelsDisplayingPageComponent,
    DocTelegramChannelsLoadingByNamePageComponent,
    DocTelegramChannelsMainPageComponent,
    DocTelegramChannelsLoadingByUsernamePageComponent,

    DocTelegramMessagesDisplayingPageComponent,
    DocTelegramMessagesLoadingByIdsPageComponent,
    DocTelegramMessagesLoadingGroupPageComponent,
    DocTelegramMessagesMainPageComponent,

    DocTelegramRepliesLoadFromChannelPageComponent,
    DocTelegramRepliesLoadFromMsgPageComponent,
    DocTelegramRepliesMainPageComponent,

    DocTelegramAddDataObjectsMainPageComponent,

    DocEmbeddingModelsMainPageComponent,

    DocExternalObjectsMainPageComponent,
    DocExternalDataMainPageComponent,
    DocExternalObjectsPacksMainPageComponent
  ],
  imports: [
    CommonModule,
    DocumentationRoutingModule
  ]
})
export class DocumentationModule { }
