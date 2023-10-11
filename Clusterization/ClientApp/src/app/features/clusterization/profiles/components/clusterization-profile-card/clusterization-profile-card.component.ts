import { Component, Input } from '@angular/core';
import { ISimpleClusterizationProfile } from '../../models/simple-clusterization-profile';
import { Router } from '@angular/router';

@Component({
  selector: 'app-clusterization-profile-card',
  templateUrl: './clusterization-profile-card.component.html',
  styleUrls: ['./clusterization-profile-card.component.scss']
})
export class ClusterizationProfileCardComponent {
  @Input() profile:ISimpleClusterizationProfile;

  constructor(private router:Router){}

  openFull(){
    this.router.navigateByUrl('profiles/full/'+this.profile.id);
  }
}
