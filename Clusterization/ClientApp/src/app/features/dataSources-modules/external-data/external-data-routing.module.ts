import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ExternalObjectsPackFullPageComponent } from './external-objects-packs/pages/external-objects-pack-full-page/external-objects-pack-full-page.component';
import { ExternalObjectListComponent } from './external-objects/components/external-object-list/external-object-list.component';
import { ExternalObjectsPackListPageComponent } from './external-objects-packs/pages/external-objects-pack-list-page/external-objects-pack-list-page.component';
import { ExternalObjectListPageComponent } from './external-objects/pages/external-object-list-page/external-object-list-page.component';

const routes: Routes = [  {
  path:'',
  redirectTo:'packs',
  pathMatch:'full'
},
{
  path: 'packs',
  children: [
    {
      path: '',
      redirectTo: 'list',
      pathMatch: 'full'
    },
    {
      path: 'list',
      component: ExternalObjectsPackListPageComponent
    },
    {
      path: 'full/:id',
      component: ExternalObjectsPackFullPageComponent,
      children: [
        {
          path: '',
          redirectTo: 'list',
          pathMatch: 'full'
        },
        {
          path: 'list/:id',
          component: ExternalObjectListPageComponent
        }
      ]
    }
  ]
},];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ExternalDataRoutingModule { }
