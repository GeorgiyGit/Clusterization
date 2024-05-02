import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IProfileFilter } from 'src/app/features/clusterization-module/clusterization/profiles/models/profile-filter';
import { ICustomerGetClusterizationProfilesRequest } from 'src/app/features/clusterization-module/clusterization/profiles/models/requests/customer-get-profiles-request';
import { ISimpleClusterizationProfile } from 'src/app/features/clusterization-module/clusterization/profiles/models/responses/simple-clusterization-profile';
import { ClusterizationProfilesService } from 'src/app/features/clusterization-module/clusterization/profiles/services/clusterization-profiles.service';

@Component({
  selector: 'app-customer-profiles-list-page',
  templateUrl: './customer-profiles-list-page.component.html',
  styleUrl: './customer-profiles-list-page.component.scss'
})
export class CustomerProfilesListPageComponent implements OnInit{
  request:ICustomerGetClusterizationProfilesRequest={
    algorithmTypeId:undefined,
    dimensionCount:undefined,
    pageParameters:{
      pageNumber:1,
      pageSize:10
    }
  }

  profiles:ISimpleClusterizationProfile[]=[];

  constructor(private profilesService:ClusterizationProfilesService,
    private toastr:MyToastrService,
    private route:ActivatedRoute){}
  ngOnInit(): void {
    this.loadFirst();
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
    this.profilesService.getCustomerCollection(this.request).subscribe(res=>{
      this.profiles=res;
      this.isLoading=false;

      if(res.length<this.request.pageParameters.pageSize)this.isLoadMoreAvailable=false;
      else this.isLoadMoreAvailable=true;

    },error=>{
      this.isLoading=false;
      this.toastr.error(error.error.Message);
    });
  }
  isLoading2:boolean;
  loadMore(){
    this.isLoading2=true;
    this.request.pageParameters.pageNumber++;
    this.profilesService.getCustomerCollection(this.request).subscribe(res=>{
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
