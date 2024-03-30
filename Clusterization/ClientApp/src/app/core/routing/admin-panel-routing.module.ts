import { NgModule } from "@angular/core";
import { Route, RouterModule } from "@angular/router";
import { AdminPanelNavigationPageComponent } from "src/app/features/admin-panel/pages/admin-panel-navigation-page/admin-panel-navigation-page.component";
import { AddQuatasPackToCustomerPageComponent } from "src/app/features/admin-panel/quotas/page/add-quatas-pack-to-customer-page/add-quatas-pack-to-customer-page.component";
import { CustomerListPageComponent } from "src/app/features/admin-panel/users/pages/customer-list-page/customer-list-page.component";

const routes: Route[] = [
  {
    path:'',
    component:AdminPanelNavigationPageComponent,
    children:[
      {
        path:'',
        redirectTo:'customers',
        pathMatch:'full'
      },
      {
        path:'customers',
        component:CustomerListPageComponent
      }
    ]
  },
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminPanelRoutingModule { }