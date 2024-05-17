import { Component, Input } from '@angular/core';
import { IClusterDataObject } from '../../models/responses/cluster-data-object';

@Component({
  selector: 'app-cluster-data-object-card',
  templateUrl: './cluster-data-object-card.component.html',
  styleUrl: './cluster-data-object-card.component.scss'
})
export class ClusterDataObjectCardComponent {
@Input() dataObject:IClusterDataObject;
}
