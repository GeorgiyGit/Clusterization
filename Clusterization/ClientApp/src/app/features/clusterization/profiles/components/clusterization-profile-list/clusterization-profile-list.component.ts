import { Component, Input } from '@angular/core';
import { ISimpleClusterizationProfile } from '../../models/responses/simple-clusterization-profile';

@Component({
  selector: 'app-clusterization-profile-list',
  templateUrl: './clusterization-profile-list.component.html',
  styleUrls: ['./clusterization-profile-list.component.scss']
})
export class ClusterizationProfileListComponent {
  @Input() profiles:ISimpleClusterizationProfile[]=[];
}
