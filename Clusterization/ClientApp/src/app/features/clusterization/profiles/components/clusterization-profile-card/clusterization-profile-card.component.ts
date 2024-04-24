import { Component, Input, OnInit } from '@angular/core';
import { ISimpleClusterizationProfile } from '../../models/responses/simple-clusterization-profile';
import { Router } from '@angular/router';
import { ClusterizationProfilesService } from '../../services/clusterization-profiles.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { AccountService } from 'src/app/features/account/services/account.service';

@Component({
  selector: 'app-clusterization-profile-card',
  templateUrl: './clusterization-profile-card.component.html',
  styleUrls: ['./clusterization-profile-card.component.scss']
})
export class ClusterizationProfileCardComponent implements OnInit{
  @Input() profile:ISimpleClusterizationProfile;

  isYour:boolean=false;

  constructor(private router:Router,
    private profileService:ClusterizationProfilesService,
    private toastr:MyToastrService,
    public accountService:AccountService){}

  ngOnInit(): void {
    let id = this.accountService.getUserId();
    if(id!=null && this.profile!=null && id==this.profile.ownerId){
      this.isYour=true;
    }
  }

  openFull(){
    this.router.navigateByUrl('profiles/full/'+this.profile.id);
  }

  elect(event:any){
    if(this.profile.isElected)return;

    if(!this.isYour){
      this.toastr.error($localize`Цей профіль не ваш!`);
      return;
    }

    this.profile.isElected=true;
    this.profileService.elect(this.profile.id).subscribe(res=>{

    },error=>{
      this.profile.isElected=false;
      this.toastr.error(error.error.Message);
    })
  }

  unelect(event:any){
    if(!this.profile.isElected)return;
    if(!this.isYour){
      this.toastr.error($localize`Цей профіль не ваш!`);
      return;
    }

    this.profile.isElected=false;
    this.profileService.unElect(this.profile.id).subscribe(res=>{

    },error=>{
      this.profile.isElected=true;
      this.toastr.error(error.error.Message);
    })
  }
}
