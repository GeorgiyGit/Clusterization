import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ExternalDataRoutingModule } from './external-data-routing.module';
import { ExternalObjectListComponent } from './external-objects/components/external-object-list/external-object-list.component';
import { ExternalObjectCardComponent } from './external-objects/components/external-object-card/external-object-card.component';
import { ExternalObjectsPackListComponent } from './external-objects-packs/components/external-objects-pack-list/external-objects-pack-list.component';
import { ExternalObjectsPackListPageComponent } from './external-objects-packs/pages/external-objects-pack-list-page/external-objects-pack-list-page.component';
import { ExternalObjectsPackFullPageComponent } from './external-objects-packs/pages/external-objects-pack-full-page/external-objects-pack-full-page.component';
import { ExternalObjectsPackCardComponent } from './external-objects-packs/components/external-objects-pack-card/external-objects-pack-card.component';
import { CoreModule } from 'src/app/core/core.module';
import { LoadExternalObjectsPageComponent } from './external-objects-packs/pages/load-external-objects-page/load-external-objects-page.component';
import { LoadAndAddExternalObjectsPageComponent } from './external-objects-packs/pages/load-and-add-external-objects-page/load-and-add-external-objects-page.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ClusterizationModule } from '../../clusterization-module/clusterization.module';
import { MatTooltip } from '@angular/material/tooltip';
import { ExternalObjectListPageComponent } from './external-objects/pages/external-object-list-page/external-object-list-page.component';


@NgModule({
  declarations: [
    ExternalObjectCardComponent,
    ExternalObjectListComponent,
    ExternalObjectListPageComponent,

    ExternalObjectsPackCardComponent,
    ExternalObjectsPackListComponent,
    ExternalObjectsPackListPageComponent,
    ExternalObjectsPackFullPageComponent,

    LoadExternalObjectsPageComponent,
    LoadAndAddExternalObjectsPageComponent,
  ],
  imports: [
    CommonModule,
    ExternalDataRoutingModule,
    CoreModule,
    FormsModule,
    ReactiveFormsModule,
    ClusterizationModule,
    MatTooltip
  ]
})
export class ExternalDataModule { }
