import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DocumentationRoutingModule } from './documentation-routing.module';
import { DocYoutubeChannelsMainPageComponent } from './children/youtube/channels/pages/doc-youtube-channels-main-page/doc-youtube-channels-main-page.component';
import { DocYoutubeChannelsLoadingByIdPageComponent } from './children/youtube/channels/pages/doc-youtube-channels-loading-by-id-page/doc-youtube-channels-loading-by-id-page.component';
import { DocYoutubeChannelsLoadingByNamePageComponent } from './children/youtube/channels/pages/doc-youtube-channels-loading-by-name-page/doc-youtube-channels-loading-by-name-page.component';
import { DocYoutubeChannelsDisplayingPageComponent } from './children/youtube/channels/pages/doc-youtube-channels-displaying-page/doc-youtube-channels-displaying-page.component';
import { DocYoutubeVideosMainPageComponent } from './children/youtube/videos/pages/doc-youtube-videos-main-page/doc-youtube-videos-main-page.component';
import { DocYoutubeVideosLoadingByIdsPageComponent } from './children/youtube/videos/pages/doc-youtube-videos-loading-by-ids-page/doc-youtube-videos-loading-by-ids-page.component';
import { DocYoutubeVideosLoadingManyPageComponent } from './children/youtube/videos/pages/doc-youtube-videos-loading-many-page/doc-youtube-videos-loading-many-page.component';
import { DocYoutubeVideosDisplayingPageComponent } from './children/youtube/videos/pages/doc-youtube-videos-displaying-page/doc-youtube-videos-displaying-page.component';
import { DocYoutubeCommentsMainPageComponent } from './children/youtube/comments/pages/doc-youtube-comments-main-page/doc-youtube-comments-main-page.component';
import { DocYoutubeCommentsLoadFromVideoPageComponent } from './children/youtube/comments/pages/doc-youtube-comments-load-from-video-page/doc-youtube-comments-load-from-video-page.component';
import { DocYoutubeCommentsLoadFromChannelPageComponent } from './children/youtube/comments/pages/doc-youtube-comments-load-from-channel-page/doc-youtube-comments-load-from-channel-page.component';
import { DocYoutubeMainPageComponent } from './children/youtube/pages/doc-youtube-main-page/doc-youtube-main-page.component';
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
import { DocWorkspacesAddingDataPageComponent } from './children/workspaces/pages/doc-workspaces-adding-data-page/doc-workspaces-adding-data-page.component';
import { DocWorkspacesLoadingEmbeddingsPageComponent } from './children/workspaces/pages/doc-workspaces-loading-embeddings-page/doc-workspaces-loading-embeddings-page.component';
import { DocWorkspacesDisplayingPageComponent } from './children/workspaces/pages/doc-workspaces-displaying-page/doc-workspaces-displaying-page.component';
import { DocProfilesMainPageComponent } from './children/profiles/pages/doc-profiles-main-page/doc-profiles-main-page.component';
import { DocProfilesCreationPageComponent } from './children/profiles/pages/doc-profiles-creation-page/doc-profiles-creation-page.component';
import { DocProfilesClusterizationPageComponent } from './children/profiles/pages/doc-profiles-clusterization-page/doc-profiles-clusterization-page.component';
import { DocProfilesDisplayingPageComponent } from './children/profiles/pages/doc-profiles-displaying-page/doc-profiles-displaying-page.component';
import { DocPointsMapMainPageComponent } from './children/points-map/pages/doc-points-map-main-page/doc-points-map-main-page.component';
import { DocPointsMapDynamicLoadingPageComponent } from './children/points-map/pages/doc-points-map-dynamic-loading-page/doc-points-map-dynamic-loading-page.component';
import { DocNavigationPageComponent } from './pages/doc-navigation-page/doc-navigation-page.component';


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
    DocWorkspacesAddingDataPageComponent,
    DocWorkspacesLoadingEmbeddingsPageComponent,
    DocWorkspacesDisplayingPageComponent,

    DocProfilesMainPageComponent,
    DocProfilesCreationPageComponent,
    DocProfilesClusterizationPageComponent,
    DocProfilesDisplayingPageComponent,

    DocPointsMapMainPageComponent,
    DocPointsMapDynamicLoadingPageComponent,

    DocNavigationPageComponent
  ],
  imports: [
    CommonModule,
    DocumentationRoutingModule
  ]
})
export class DocumentationModule { }
