import { Component, OnInit } from '@angular/core';
import { IGetClusterizationProfilesRequest } from '../../models/get-clusterization-profiles-request';
import { ISimpleClusterizationProfile } from '../../models/simple-clusterization-profile';
import { ClusterizationProfilesService } from '../../services/clusterization-profiles.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IProfileFilter } from '../../models/profile-filter';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-clusterization-profile-list-page',
  templateUrl: './clusterization-profile-list-page.component.html',
  styleUrls: ['./clusterization-profile-list-page.component.scss']
})
export class ClusterizationProfileListPageComponent implements OnInit{
  request:IGetClusterizationProfilesRequest={
    algorithmTypeId:undefined,
    dimensionCount:undefined,
    pageParameters:{
      pageNumber:1,
      pageSize:10
    },
    workspaceId:0
  }

  profiles:ISimpleClusterizationProfile[]=[];

  constructor(private profilesService:ClusterizationProfilesService,
    private toastr:MyToastrService,
    private route:ActivatedRoute){}
  ngOnInit(): void {
    let workspaceId = this.route.snapshot.params['workspaceId'];

    if(workspaceId!=null){
      this.request.workspaceId=workspaceId;

      this.loadFirst();
    }
  }

  changeFilter(filter:IProfileFilter){
    let flag=true;
    if(this.request.algorithmTypeId==filter.algorithmTypeId &&
       this.request.dimensionCount==filter.dimensionCount){
        flag=false;
    }
    this.request.algorithmTypeId=filter.algorithmTypeId;
    this.request.dimensionCount=filter.dimensionCount;

    if(flag){
      this.loadFirst();
    }
  }

  isLoading:boolean;
  loadFirst(){
    this.request.pageParameters.pageNumber=1;

    this.isLoading=true;
    this.profilesService.getCollection(this.request).subscribe(res=>{
      this.profiles=res;
      this.isLoading=false;

      if(res.length<this.request.pageParameters.pageSize)this.isLoadMoreAvailable=false;
      else this.isLoadMoreAvailable=true;

      console.log(this.profiles);
    },error=>{
      this.isLoading=false;
      this.toastr.error(error.error.Message);
    });
  }
  isLoading2:boolean;
  loadMore(){
    this.isLoading2=true;
    this.request.pageParameters.pageNumber++;
    this.profilesService.getCollection(this.request).subscribe(res=>{
      this.profiles=this.profiles.concat(res);
      this.isLoading2=false;

      if(res.length<this.request.pageParameters.pageSize)this.isLoadMoreAvailable=false;
      else this.isLoadMoreAvailable=true;
    },error=>{
      this.isLoading2=false;
      this.toastr.error(error.error.Message);
    });
  }

  isLoadMoreAvailable:boolean;
  addMore(){
    if(this.isLoadMoreAvailable){
      this.loadMore();
    }
  }
}
