import { Component, Input } from '@angular/core';
import { IClusterDataObject } from '../../models/responses/cluster-data-object';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cluster-data-object-card',
  templateUrl: './cluster-data-object-card.component.html',
  styleUrl: './cluster-data-object-card.component.scss'
})
export class ClusterDataObjectCardComponent {
  @Input() dataObject: IClusterDataObject;

  constructor(private router:Router){}
  openFull(){
    this.router.navigate([{ outlets: { overflow: 'clusterization/dataObjects/full/'+this.dataObject.id } }]);
  }
}
