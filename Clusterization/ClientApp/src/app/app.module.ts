import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ClipboardModule } from '@angular/cdk/clipboard';
import { YoutubeChannelCardComponent } from './features/youtube/channels/components/youtube-channel-card/youtube-channel-card.component';
import { ToastNoAnimation, ToastNoAnimationModule, ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { YoutubeVideoListComponent } from './features/youtube/videos/components/youtube-video-list/youtube-video-list.component';
import { YoutubeVideoCardComponent } from './features/youtube/videos/components/youtube-video-card/youtube-video-card.component';
import { HeaderComponent } from './core/components/header/header.component';
import { YoutubeChannelListPageComponent } from './features/youtube/channels/pages/youtube-channel-list-page/youtube-channel-list-page.component';
import { YoutubeVideoListPageComponent } from './features/youtube/videos/pages/youtube-video-list-page/youtube-video-list-page.component';
import { YoutubeCommentCardComponent } from './features/youtube/comments/components/youtube-comment-card/youtube-comment-card.component';
import { YoutubeCommentListComponent } from './features/youtube/comments/components/youtube-comment-list/youtube-comment-list.component';
import { YoutubeCommentListPageComponent } from './features/youtube/comments/pages/youtube-comment-list-page/youtube-comment-list-page.component';
import { AppRoutingModule } from './core/routing/app-routing.module';
import { YoutubeLoadNewChannelPageComponent } from './features/youtube/channels/pages/youtube-load-new-channel-page/youtube-load-new-channel-page.component';
import { YoutubeChannelsSearchFilterComponent } from './features/youtube/channels/components/youtube-channels-search-filter/youtube-channels-search-filter.component';
import { SearchInputComponent } from './core/components/search-input/search-input.component';
import { CloseOutsideDirective } from './core/directives/close-outside.directive';
import { SelectOptionInputComponent } from './core/components/select-option-input/select-option-input.component';
import { YoutubeChannelListComponent } from './features/youtube/channels/components/youtube-channel-list/youtube-channel-list.component';
import { LoaderComponent } from './core/components/loader/loader.component';
import { NormalizedDateTimePipe } from './core/pipes/normalized-date-time.pipe';
import { YoutubeLoadOneChannelComponent } from './features/youtube/channels/components/youtube-load-one-channel/youtube-load-one-channel.component';
import { YoutubeLoadMultipleChannelsComponent } from './features/youtube/channels/components/youtube-load-multiple-channels/youtube-load-multiple-channels.component';
import { YoutubeFullChannelPageComponent } from './features/youtube/channels/pages/youtube-full-channel-page/youtube-full-channel-page.component';
import { CommonModule } from '@angular/common';
import { MatTooltipModule } from '@angular/material/tooltip';
import { YoutubeLoadNewVideoPageComponent } from './features/youtube/videos/pages/youtube-load-new-video-page/youtube-load-new-video-page.component';
import { YoutubeLoadOneVideoComponent } from './features/youtube/videos/components/youtube-load-one-video/youtube-load-one-video.component';
import { YoutubeLoadMultipleVideosComponent } from './features/youtube/videos/components/youtube-load-multiple-videos/youtube-load-multiple-videos.component';
import { YoutubeVideosSearchFilterComponent } from './features/youtube/videos/components/youtube-videos-search-filter/youtube-videos-search-filter.component';
import { TaskCardComponent } from './features/tasks/components/task-card/task-card.component';
import { TaskListPageComponent } from './features/tasks/pages/task-list-page/task-list-page.component';
import { TaskListComponent } from './features/tasks/components/task-list/task-list.component';
import { YoutubeLoadAllVideosPageComponent } from './features/youtube/videos/pages/youtube-load-all-videos-page/youtube-load-all-videos-page.component';
import { YoutubeFullVideoPageComponent } from './features/youtube/videos/pages/youtube-full-video-page/youtube-full-video-page.component';
import { YoutubeLoadAllCommentsPageComponent } from './features/youtube/comments/pages/youtube-load-all-comments-page/youtube-load-all-comments-page.component';
import { ClusterizationTypesSelectComponent } from './features/clusterization/clusterizationTypes/components/clusterization-types-select/clusterization-types-select.component';
import { ClusterizationDimensionTypesInputComponent } from './features/clusterization/dimensionTypes/components/clusterization-dimension-types-input/clusterization-dimension-types-input.component';
import { AddWorkspacePageComponent } from './features/clusterization/workspace/pages/add-workspace-page/add-workspace-page.component';
import { WorkspaceCardComponent } from './features/clusterization/workspace/components/workspace-card/workspace-card.component';
import { WorkspaceListComponent } from './features/clusterization/workspace/components/workspace-list/workspace-list.component';
import { WorkspaceListPageComponent } from './features/clusterization/workspace/pages/workspace-list-page/workspace-list-page.component';
import { WorkspaceSearchFilterComponent } from './features/clusterization/workspace/components/workspace-search-filter/workspace-search-filter.component';
import { WorkspaceFullPageComponent } from './features/clusterization/workspace/pages/workspace-full-page/workspace-full-page.component';
import { MoreActionSelectComponent } from './core/components/more-action-select/more-action-select.component';
import { AddChannelCommentsToWorkspacePageComponent } from './features/clusterization/workspace/pages/add-channel-comments-to-workspace-page/add-channel-comments-to-workspace-page.component';
import { AddVideoCommentsToWorkspaceComponent } from './features/clusterization/workspace/pages/add-video-comments-to-workspace/add-video-comments-to-workspace.component';
import { ClusterizationAlgorithmTypesSelectComponent } from './features/clusterization/algorithms/algorithmType/components/clusterization-algorithm-types-select/clusterization-algorithm-types-select.component';
import { AbstractAlgorithmAddPageComponent } from './features/clusterization/algorithms/abstractAlgorithm/pages/abstract-algorithm-add-page/abstract-algorithm-add-page.component';
import { AddKMeansAlgorithmComponent } from './features/clusterization/algorithms/non-hierarchical/k-means/components/add-k-means-algorithm/add-k-means-algorithm.component';
import { ClusterizationProfileCardComponent } from './features/clusterization/profiles/components/clusterization-profile-card/clusterization-profile-card.component';
import { ClusterizationProfileListComponent } from './features/clusterization/profiles/components/clusterization-profile-list/clusterization-profile-list.component';
import { ClusterizationProfileListPageComponent } from './features/clusterization/profiles/pages/clusterization-profile-list-page/clusterization-profile-list-page.component';
import { ClusterizationProfileSearchFilterComponent } from './features/clusterization/profiles/components/clusterization-profile-search-filter/clusterization-profile-search-filter.component';
import { AbstractAlgorithmsSelectComponent } from './features/clusterization/algorithms/abstractAlgorithm/abstract-algorithms-select/abstract-algorithms-select.component';
import { ClusterizationProfileAddPageComponent } from './features/clusterization/profiles/pages/clusterization-profile-add-page/clusterization-profile-add-page.component';
import { AddOneClusterAlgorithmComponent } from './features/clusterization/algorithms/non-hierarchical/oneCluster/components/add-one-cluster-algorithm/add-one-cluster-algorithm.component';
import { ClusterizationFullProfilePageComponent } from './features/clusterization/profiles/pages/clusterization-full-profile-page/clusterization-full-profile-page.component';
import { PointsMapPlaneComponent } from './features/points-map/components/points-map-plane/points-map-plane.component';
import { PointsMapPageComponent } from './features/points-map/pages/points-map-page/points-map-page.component';
@NgModule({
  declarations: [
    AppComponent,
    YoutubeChannelCardComponent,
    YoutubeChannelListComponent,
    YoutubeVideoListComponent,
    YoutubeVideoCardComponent,
    HeaderComponent,
    YoutubeChannelListPageComponent,
    YoutubeVideoListPageComponent,
    YoutubeCommentCardComponent,
    YoutubeCommentListComponent,
    YoutubeCommentListPageComponent,
    YoutubeLoadNewChannelPageComponent,
    YoutubeChannelsSearchFilterComponent,
    SearchInputComponent,
    CloseOutsideDirective,
    SelectOptionInputComponent,
    LoaderComponent,
    NormalizedDateTimePipe,
    YoutubeLoadOneChannelComponent,
    YoutubeLoadMultipleChannelsComponent,
    YoutubeFullChannelPageComponent,
    YoutubeLoadNewVideoPageComponent,
    YoutubeLoadOneVideoComponent,
    YoutubeLoadMultipleVideosComponent,
    YoutubeVideosSearchFilterComponent,
    TaskCardComponent,
    TaskListPageComponent,
    TaskListComponent,
    YoutubeLoadAllVideosPageComponent,
    YoutubeFullVideoPageComponent,
    YoutubeLoadAllCommentsPageComponent,
    ClusterizationTypesSelectComponent,
    ClusterizationDimensionTypesInputComponent,
    AddWorkspacePageComponent,
    WorkspaceCardComponent,
    WorkspaceListComponent,
    WorkspaceListPageComponent,
    WorkspaceSearchFilterComponent,
    WorkspaceFullPageComponent,
    MoreActionSelectComponent,
    AddChannelCommentsToWorkspacePageComponent,
    AddVideoCommentsToWorkspaceComponent,
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
  ],
  imports:[
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpClientModule,
    CommonModule,
    AppRoutingModule,
    FormsModule,
    RouterModule,
    MatTooltipModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
