import { NgModule } from "@angular/core";
import { Route, RouterModule } from "@angular/router";
import { AdminPanelNavigationPageComponent } from "src/app/features/admin-panel/pages/admin-panel-navigation-page/admin-panel-navigation-page.component";
import { AddQuatasPackToCustomerPageComponent } from "src/app/features/admin-panel/quotas/page/add-quatas-pack-to-customer-page/add-quatas-pack-to-customer-page.component";
import { CustomerListPageComponent } from "src/app/features/admin-panel/users/pages/customer-list-page/customer-list-page.component";
import { CustomerQuotasListPageComponent } from "src/app/features/customer-account-details/children/quotas/pages/customer-quotas-list-page/customer-quotas-list-page.component";
import { CustomerQuotasLogsPageComponent } from "src/app/features/customer-account-details/children/quotas/pages/customer-quotas-logs-page/customer-quotas-logs-page.component";
import { CustomerQuotasMainPageComponent } from "src/app/features/customer-account-details/children/quotas/pages/customer-quotas-main-page/customer-quotas-main-page.component";
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