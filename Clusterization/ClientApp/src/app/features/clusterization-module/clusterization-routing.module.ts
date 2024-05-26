import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WorkspaceListPageComponent } from './clusterization/workspace/pages/workspace-list-page/workspace-list-page.component';
import { WorkspaceFullPageComponent } from './clusterization/workspace/pages/workspace-full-page/workspace-full-page.component';
import { ClusterizationProfileListPageComponent } from './clusterization/profiles/pages/clusterization-profile-list-page/clusterization-profile-list-page.component';
import { WorkspaceAddDataPackListPageComponent } from './clusterization/workspaceAddDataPacks/pages/workspace-add-data-pack-list-page/workspace-add-data-pack-list-page.component';
import { ClusterizationFullProfilePageComponent } from './clusterization/profiles/pages/clusterization-full-profile-page/clusterization-full-profile-page.component';
import { PointsMapPageComponent } from './points-map/pages/points-map-page/points-map-page.component';
import { FastClusteringMainPageComponent } from './clusterization/fast-сlustering/pages/fast-clustering-main-page/fast-clustering-main-page.component';
import { CustomerGuard } from 'src/app/core/guard/customer.guard';
import { ClusterizationProfileTasksListPageComponent } from './clusterization/profiles/pages/clusterization-profile-tasks-list-page/clusterization-profile-tasks-list-page.component';
import { WorkspaceTasksListPageComponent } from './clusterization/workspace/pages/workspace-tasks-list-page/workspace-tasks-list-page.component';
const routes: Routes = [
  {
    path: 'workspaces',
    children: [
      {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full'
      },
      {
        path: 'list',
        component: WorkspaceListPageComponent
      },
      {
        path: 'full/:id',
        component: WorkspaceFullPageComponent,
        children: [
          {
            path: '',
            redirectTo: 'profiles-list',
            pathMatch: 'full'
          },
          {
            path:'profiles-list/:workspaceId',
            component:ClusterizationProfileListPageComponent
          },
          {
            path:'workspace-add-data-packs/list/:workspaceId',
            component:WorkspaceAddDataPackListPageComponent
          },
          {
            path:'tasks/:workspaceId',
            component:WorkspaceTasksListPageComponent
          },
        ]
      },
    ]
  },
  {
    path: 'profiles',
    children: [
      {
        path: 'full/:id',
        component: ClusterizationFullProfilePageComponent,
        children:[
          {
            path:'profile-points-map/:profileId',
            component:PointsMapPageComponent
          },
          {
            path:'tasks/:profileId',
            component:ClusterizationProfileTasksListPageComponent
          }
        ]
      },
    ]
  },
  {
    path:'fast-clustering',
    component:FastClusteringMainPageComponent,
    canActivate: [CustomerGuard],
    canActivateChild: [CustomerGuard],
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClusterizationRoutingModule { }
