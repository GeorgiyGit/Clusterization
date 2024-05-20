import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClusterizationRoutingModule } from './clusterization-routing.module';
import { ClusterizationTypesSelectComponent } from './clusterization/clusterizationTypes/components/clusterization-types-select/clusterization-types-select.component';
import { ClusterizationDimensionTypesInputComponent } from './clusterization/dimensionTypes/components/clusterization-dimension-types-input/clusterization-dimension-types-input.component';
import { WorkspaceCardComponent } from './clusterization/workspace/components/workspace-card/workspace-card.component';
import { WorkspaceListComponent } from './clusterization/workspace/components/workspace-list/workspace-list.component';
import { WorkspaceSearchFilterComponent } from './clusterization/workspace/components/workspace-search-filter/workspace-search-filter.component';
import { AddWorkspacePageComponent } from './clusterization/workspace/pages/add-workspace-page/add-workspace-page.component';
import { WorkspaceFullPageComponent } from './clusterization/workspace/pages/workspace-full-page/workspace-full-page.component';
import { WorkspaceListPageComponent } from './clusterization/workspace/pages/workspace-list-page/workspace-list-page.component';
import { AbstractAlgorithmsSelectComponent } from './clusterization/algorithms/abstractAlgorithm/abstract-algorithms-select/abstract-algorithms-select.component';
import { AbstractAlgorithmAddPageComponent } from './clusterization/algorithms/abstractAlgorithm/pages/abstract-algorithm-add-page/abstract-algorithm-add-page.component';
import { ClusterizationAlgorithmTypesSelectComponent } from './clusterization/algorithms/algorithmType/components/clusterization-algorithm-types-select/clusterization-algorithm-types-select.component';
import { AddDbscanAlgorithmComponent } from './clusterization/algorithms/non-hierarchical/dbscan/components/add-dbscan-algorithm/add-dbscan-algorithm.component';
import { AddGaussianMixtureAlgorithmComponent } from './clusterization/algorithms/non-hierarchical/gaussian-mixture/components/add-gaussian-mixture-algorithm/add-gaussian-mixture-algorithm.component';
import { AddKMeansAlgorithmComponent } from './clusterization/algorithms/non-hierarchical/k-means/components/add-k-means-algorithm/add-k-means-algorithm.component';
import { AddOneClusterAlgorithmComponent } from './clusterization/algorithms/non-hierarchical/oneCluster/components/add-one-cluster-algorithm/add-one-cluster-algorithm.component';
import { ClusterizationProfileCardComponent } from './clusterization/profiles/components/clusterization-profile-card/clusterization-profile-card.component';
import { ClusterizationProfileListComponent } from './clusterization/profiles/components/clusterization-profile-list/clusterization-profile-list.component';
import { ClusterizationProfileSearchFilterComponent } from './clusterization/profiles/components/clusterization-profile-search-filter/clusterization-profile-search-filter.component';
import { ClusterizationFullProfilePageComponent } from './clusterization/profiles/pages/clusterization-full-profile-page/clusterization-full-profile-page.component';
import { ClusterizationProfileAddPageComponent } from './clusterization/profiles/pages/clusterization-profile-add-page/clusterization-profile-add-page.component';
import { ClusterizationProfileListPageComponent } from './clusterization/profiles/pages/clusterization-profile-list-page/clusterization-profile-list-page.component';
import { PointsMapPlaneComponent } from './points-map/components/points-map-plane/points-map-plane.component';
import { PointsMapPageComponent } from './points-map/pages/points-map-page/points-map-page.component';
import { ChangingTypesSelectOptionComponent } from 'src/app/core/components/changing-types-select-option/changing-types-select-option.component';
import { VisibleTypesSelectOptionComponent } from 'src/app/core/components/visible-types-select-option/visible-types-select-option.component';
import { SimpleWorkspaceAddDataPackCardComponent } from './clusterization/workspaceAddDataPacks/components/simple-workspace-add-data-pack-card/simple-workspace-add-data-pack-card.component';
import { WorkspaceAddDataPackListComponent } from './clusterization/workspaceAddDataPacks/components/workspace-add-data-pack-list/workspace-add-data-pack-list.component';
import { WorkspaceAddDataPackListPageComponent } from './clusterization/workspaceAddDataPacks/pages/workspace-add-data-pack-list-page/workspace-add-data-pack-list-page.component';
import { WorkspaceAddPackFullPageComponent } from './clusterization/workspaceAddDataPacks/pages/workspace-add-pack-full-page/workspace-add-pack-full-page.component';
import { EmbeddingLoadingStateCardComponent } from './embedding-loading-states/components/embedding-loading-state-card/embedding-loading-state-card.component';
import { EmbeddingModelsSelectComponent } from './embedding-models/components/embedding-models-select/embedding-models-select.component';
import { LoadEmbeddingsByPackPageComponent } from './embeddings/pages/load-embeddings-by-pack-page/load-embeddings-by-pack-page.component';
import { CoreModule } from 'src/app/core/core.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddScAlgorithmComponent } from './clusterization/algorithms/non-hierarchical/spectral-clustering/components/add-sc-algorithm/add-sc-algorithm.component';
import { DRTechniquesSelectComponent } from './dimensionalityReduction/DR-techniques/components/dr-techniques-select/dr-techniques-select.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { UpdateWorkspacePageComponent } from './clusterization/workspace/pages/update-workspace-page/update-workspace-page.component';
import { ClustersListComponent } from './clusters/components/clusters-list/clusters-list.component';
import { ClusterCardComponent } from './clusters/components/cluster-card/cluster-card.component';
import { ClusterDataObjectCardComponent } from './clusters/components/cluster-data-object-card/cluster-data-object-card.component';
import { YoutubeCommentDataObjectComponent } from './data-objects/components/youtube-comment-data-object/youtube-comment-data-object.component';
import { TelegramMessageDataObjectComponent } from './data-objects/components/telegram-message-data-object/telegram-message-data-object.component';
import { TelegramReplyDataObjectComponent } from './data-objects/components/telegram-reply-data-object/telegram-reply-data-object.component';
import { ExternalDataDataObjectComponent } from './data-objects/components/external-data-data-object/external-data-data-object.component';
import { DataObjectFullPageComponent } from './data-objects/pages/data-object-full-page/data-object-full-page.component';
import { TelegramModule } from '../dataSources-modules/telegram/telegram.module';
import { LoadClustersFilePageComponent } from './clusters/pages/load-clusters-file-page/load-clusters-file-page.component';


@NgModule({
  declarations: [
    ClusterizationTypesSelectComponent,
    ClusterizationDimensionTypesInputComponent,
    AddWorkspacePageComponent,
    WorkspaceCardComponent,
    WorkspaceListComponent,
    WorkspaceListPageComponent,
    WorkspaceSearchFilterComponent,
    WorkspaceFullPageComponent,
    UpdateWorkspacePageComponent,
    ClusterizationAlgorithmTypesSelectComponent,
    AbstractAlgorithmAddPageComponent,
    AddKMeansAlgorithmComponent,
    ClusterizationProfileCardComponent,
    ClusterizationProfileListComponent,
    ClusterizationProfileListPageComponent,
    ClusterizationProfileSearchFilterComponent,
    AbstractAlgorithmsSelectComponent,
    ClusterizationProfileAddPageComponent,
    AddOneClusterAlgorithmComponent,
    ClusterizationFullProfilePageComponent,
    PointsMapPlaneComponent,
    PointsMapPageComponent,
    DRTechniquesSelectComponent,

    AddDbscanAlgorithmComponent,
    AddScAlgorithmComponent,
    AddGaussianMixtureAlgorithmComponent,

    EmbeddingModelsSelectComponent,

    SimpleWorkspaceAddDataPackCardComponent,
    WorkspaceAddDataPackListComponent,
    WorkspaceAddDataPackListPageComponent,

    WorkspaceAddPackFullPageComponent,
    EmbeddingLoadingStateCardComponent,
    LoadEmbeddingsByPackPageComponent,

    VisibleTypesSelectOptionComponent,
    ChangingTypesSelectOptionComponent,

    ClustersListComponent,
    ClusterCardComponent,
    ClusterDataObjectCardComponent,
    LoadClustersFilePageComponent,

    YoutubeCommentDataObjectComponent,
    TelegramMessageDataObjectComponent,
    TelegramReplyDataObjectComponent,
    ExternalDataDataObjectComponent,
    DataObjectFullPageComponent,
  ],
  exports:[
    ClusterizationProfileSearchFilterComponent,
    ClusterizationProfileListComponent,
    WorkspaceSearchFilterComponent,
    WorkspaceListComponent,
    VisibleTypesSelectOptionComponent,
    ChangingTypesSelectOptionComponent
  ],
  imports: [
    CommonModule,
    ClusterizationRoutingModule,
    CoreModule,
    FormsModule,
    ReactiveFormsModule,
    MatTooltipModule,
    TelegramModule
  ]
})
export class ClusterizationModule { }
