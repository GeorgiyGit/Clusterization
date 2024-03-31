import { NgModule } from "@angular/core";
import { Route, RouterModule } from "@angular/router";
import { AdminPanelNavigationPageComponent } from "src/app/features/admin-panel/pages/admin-panel-navigation-page/admin-panel-navigation-page.component";
import { AddQuatasPackToCustomerPageComponent } from "src/app/features/admin-panel/quotas/page/add-quatas-pack-to-customer-page/add-quatas-pack-to-customer-page.component";
import { CustomerListPageComponent } from "src/app/features/admin-panel/users/pages/customer-list-page/customer-list-page.component";
import { CustomerQuotasListPageComponent } from "src/app/features/customer-account-details/children/quotas/pages/customer-quotas-list-page/customer-quotas-list-page.component";
import { CustomerQuotasLogsPageComponent } from "src/app/features/customer-account-details/children/quotas/pages/customer-quotas-logs-page/customer-quotas-logs-page.component";
import { CustomerQuotasMainPageComponent } from "src/app/features/customer-account-details/children/quotas/pages/customer-quotas-main-page/customer-quotas-main-page.component";
import { CustomerQuotasPackLogsPageComponent } from "src/app/features/customer-account-details/children/quotas/pages/customer-quotas-pack-logs-page/customer-quotas-pack-logs-page.component";
import { CustomerTasksListComponent } from "src/app/features/customer-account-details/children/tasks/components/customer-tasks-list/customer-tasks-list.component";
import { CustomerTasksListPageComponent } from "src/app/features/customer-account-details/children/tasks/pages/customer-tasks-list-page/customer-tasks-list-page.component";
import { CustomerWorkspacesListPageComponent } from "src/app/features/customer-account-details/children/workspaces/pages/customer-workspaces-list-page/customer-workspaces-list-page.component";
import { CustomerWorkspacesMainPageComponent } from "src/app/features/customer-account-details/children/workspaces/pages/customer-workspaces-main-page/customer-workspaces-main-page.component";
import { CustomerAccountDetailsNavPageComponent } from "src/app/features/customer-account-details/pages/customer-account-details-nav-page/customer-account-details-nav-page.component";

const routes: Route[] = [
  {
    path:'',
    component:CustomerAccountDetailsNavPageComponent,
    children:[
      {
        path:'',
        redirectTo:'quotas',
        pathMatch:'full'
      },
      {
        path:'quotas',
        component:CustomerQuotasMainPageComponent,
        children:[
          {
            path:'',
            redirectTo:'list',
            pathMatch:'full'
          },
          {
            path:'list',
            component:CustomerQuotasListPageComponent
          },
          {
            path:'logs',
            component:CustomerQuotasLogsPageComponent
          },
          {
            path:'pack-logs',
            component:CustomerQuotasPackLogsPageComponent
          },
        ]
      },
      {
        path:'tasks',
        component:CustomerTasksListPageComponent,
        children:[
          {
            path:'',
            redirectTo:'list',
            pathMatch:'full'
          },
          {
            path:'list',
            component:CustomerTasksListComponent
          }
        ]
      },
      {
        path:'workspaces',
        component:CustomerWorkspacesMainPageComponent,
        children:[
          {
            path:'',
            redirectTo:'list',
            pathMatch:'full'
          },
          {
            path:'list',
            component:CustomerWorkspacesListPageComponent
          }
        ]
      }
    ]
  },
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomerDetailsRoutingModule { }