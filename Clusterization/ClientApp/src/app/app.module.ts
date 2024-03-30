import { BrowserModule } from '@angular/platform-browser';
import { LOCALE_ID,NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS,HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import {LayoutModule} from '@angular/cdk/layout';
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
import { CommonModule, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { MatTooltipModule } from '@angular/material/tooltip';
import { YoutubeLoadNewVideoPageComponent } from './features/youtube/videos/pages/youtube-load-new-video-page/youtube-load-new-video-page.component';
import { YoutubeLoadOneVideoComponent } from './features/youtube/videos/components/youtube-load-one-video/youtube-load-one-video.component';
import { YoutubeLoadMultipleVideosComponent } from './features/youtube/videos/components/youtube-load-multiple-videos/youtube-load-multiple-videos.component';
import { YoutubeVideosSearchFilterComponent } from './features/youtube/videos/components/youtube-videos-search-filter/youtube-videos-search-filter.component';
import { TaskCardComponent } from './features/tasks/components/task-card/task-card.component';
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
import { DimensionalityReductionTechniquesSelectComponent } from './features/dimensionalityReduction/dimensionalityReductionTechniques/components/dimensionality-reduction-techniques-select/dimensionality-reduction-techniques-select.component';
import { YoutubeLoadCommentsByChannelPageComponent } from './features/youtube/comments/pages/youtube-load-comments-by-channel-page/youtube-load-comments-by-channel-page.component';
import { AddVideosCommentsToWorkspaceComponent } from './features/clusterization/workspace/pages/add-videos-comments-to-workspace/add-videos-comments-to-workspace.component';
import { AddDbscanAlgorithmComponent } from './features/clusterization/algorithms/non-hierarchical/dbscan/components/add-dbscan-algorithm/add-dbscan-algorithm.component';
import { AddSpectralClusteringAlgorithmComponent } from './features/clusterization/algorithms/non-hierarchical/spectral-clustering/components/add-spectral-clustering-algorithm/add-spectral-clustering-algorithm.component';
import { AddGaussianMixtureAlgorithmComponent } from './features/clusterization/algorithms/non-hierarchical/gaussian-mixture/components/add-gaussian-mixture-algorithm/add-gaussian-mixture-algorithm.component';
import { AddExternalDataToWorkspaceComponent } from './features/clusterization/workspace/pages/add-external-data-to-workspace/add-external-data-to-workspace.component';
import { LogInPageComponent } from './features/account/pages/log-in-page/log-in-page.component';
import { SignUpPageComponent } from './features/account/pages/sign-up-page/sign-up-page.component';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';
import { ChangingTypesSelectOptionComponent } from './core/components/changing-types-select-option/changing-types-select-option.component';
import { VisibleTypesSelectOptionComponent } from './core/components/visible-types-select-option/visible-types-select-option.component';
import { CustomHttpInterceptor } from './core/interceptors/custom-http.interceptor';
import { CustomerGuard } from './core/guard/customer.guard';
import { CustomerCardComponent } from './features/admin-panel/users/components/customer-card/customer-card.component';
import { CustomerListComponent } from './features/admin-panel/users/components/customer-list/customer-list.component';
import { CustomerListPageComponent } from './features/admin-panel/users/pages/customer-list-page/customer-list-page.component';
import { AdminPanelNavigationPageComponent } from './features/admin-panel/pages/admin-panel-navigation-page/admin-panel-navigation-page.component';
import { ModeratorGuard } from './core/guard/moderator.guard';
import { AdminPanelRoutingModule } from './core/routing/admin-panel-routing.module';
import { QuotasPackListComponent } from './features/admin-panel/quotas/components/quotas-pack-list/quotas-pack-list.component';
import { QuotasPackCardComponent } from './features/admin-panel/quotas/components/quotas-pack-card/quotas-pack-card.component';
import { AddQuatasPackToCustomerPageComponent } from './features/admin-panel/quotas/page/add-quatas-pack-to-customer-page/add-quatas-pack-to-customer-page.component';
import { CustomerAccountDetailsNavPageComponent } from './features/customer-account-details/pages/customer-account-details-nav-page/customer-account-details-nav-page.component';
import { CustomerQuotasListPageComponent } from './features/customer-account-details/children/quotas/pages/customer-quotas-list-page/customer-quotas-list-page.component';
import { CustomerQuotasMainPageComponent } from './features/customer-account-details/children/quotas/pages/customer-quotas-main-page/customer-quotas-main-page.component';
import { CustomerQuotasLogsPageComponent } from './features/customer-account-details/children/quotas/pages/customer-quotas-logs-page/customer-quotas-logs-page.component';
import { CustomerDetailsRoutingModule } from './core/routing/customer-details-routing.module';
import { CustomerQuotasItemCardComponent } from './features/customer-account-details/children/quotas/components/customer-quotas-item-card/customer-quotas-item-card.component';
import { TaskStatusesSelectComponent } from './features/tasks/components/task-statuses-select/task-statuses-select.component';
import { CustomerTasksListComponent } from './features/customer-account-details/children/tasks/components/customer-tasks-list/customer-tasks-list.component';
import { CustomerTasksListPageComponent } from './features/customer-account-details/children/tasks/pages/customer-tasks-list-page/customer-tasks-list-page.component';

export function tokenGetter() {
  return localStorage.getItem("user-token");
}
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
    DimensionalityReductionTechniquesSelectComponent,
    YoutubeLoadCommentsByChannelPageComponent,
    AddVideosCommentsToWorkspaceComponent,
    AddDbscanAlgorithmComponent,
    AddSpectralClusteringAlgorithmComponent,
    AddGaussianMixtureAlgorithmComponent,
    AddExternalDataToWorkspaceComponent,
    LogInPageComponent,
    SignUpPageComponent,
    VisibleTypesSelectOptionComponent,
    ChangingTypesSelectOptionComponent,

    CustomerCardComponent,
    CustomerListComponent,
    CustomerListPageComponent,
    AdminPanelNavigationPageComponent,

    QuotasPackCardComponent,
    QuotasPackListComponent,
    AddQuatasPackToCustomerPageComponent,

    CustomerAccountDetailsNavPageComponent,
    CustomerQuotasListPageComponent,
    CustomerQuotasMainPageComponent,
    CustomerQuotasLogsPageComponent,
    CustomerQuotasItemCardComponent,
    TaskStatusesSelectComponent,
    CustomerTasksListComponent,
    CustomerTasksListPageComponent
  ],
  imports:[
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    CommonModule,
    AppRoutingModule,
    AdminPanelRoutingModule,
    CustomerDetailsRoutingModule,
    FormsModule,
    RouterModule,
    MatTooltipModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    LayoutModule,
    ToastrModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: [
          "sladkovskygeorge.website",
        ]
      },
    }),
  ],
  providers: [
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    JwtHelperService,
    {provide: LOCALE_ID, useValue: 'en-US' },
    {
      provide:HTTP_INTERCEPTORS,
      useClass: CustomHttpInterceptor,
      multi: true
    },
    CustomerGuard,
    ModeratorGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
