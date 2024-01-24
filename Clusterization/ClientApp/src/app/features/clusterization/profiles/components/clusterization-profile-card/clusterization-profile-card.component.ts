import { Component, Input } from '@angular/core';
import { ISimpleClusterizationProfile } from '../../models/simple-clusterization-profile';
import { Router } from '@angular/router';
import { ClusterizationProfilesService } from '../../services/clusterization-profiles.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-clusterization-profile-card',
  templateUrl: './clusterization-profile-card.component.html',
  styleUrls: ['./clusterization-profile-card.component.scss']
})
export class ClusterizationProfileCardComponent {
  @Input() profile:ISimpleClusterizationProfile;

  constructor(private router:Router,
    private profileService:ClusterizationProfilesService,
    private toastr:MyToastrService){}

  openFull(){
    this.router.navigateByUrl('profiles/full/'+this.profile.id);
  }

  elect(event:any){
    if(this.profile.isElected)return;

    this.profile.isElected=true;
    this.profileService.elect(this.profile.id).subscribe(res=>{

    },error=>{
      this.profile.isElected=false;
      this.toastr.error(error.error.Message);
    })
  }

  unelect(event:any){
    if(!this.profile.isElected)return;

    this.profile.isElected=false;
    this.profileService.unElect(this.profile.id).subscribe(res=>{

    },error=>{
      this.profile.isElected=true;
      this.toastr.error(error.error.Message);
    })
  }
}
