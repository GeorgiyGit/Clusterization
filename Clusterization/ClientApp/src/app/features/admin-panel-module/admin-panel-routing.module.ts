import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminPanelNavigationPageComponent } from './pages/admin-panel-navigation-page/admin-panel-navigation-page.component';
import { CustomerListPageComponent } from './users/pages/customer-list-page/customer-list-page.component';
import { AddQuatasPackToCustomerPageComponent } from './quotas/page/add-quatas-pack-to-customer-page/add-quatas-pack-to-customer-page.component';

const routes: Routes = [
  {
    path: '',
    component: AdminPanelNavigationPageComponent,
    children: [
      {
        path: '',
        redirectTo: 'customers',
        pathMatch: 'full'
      },
      {
        path: 'customers',
        component: CustomerListPageComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminPanelRoutingModule { }
