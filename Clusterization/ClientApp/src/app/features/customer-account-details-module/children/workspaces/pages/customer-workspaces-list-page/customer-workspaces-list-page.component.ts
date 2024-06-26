import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IGetWorkspacesRequest } from 'src/app/features/clusterization-module/clusterization/workspace/models/requests/getWorkspacesRequest';
import { ISimpleClusterizationWorkspace } from 'src/app/features/clusterization-module/clusterization/workspace/models/responses/simpleClusterizationWorkspace';
import { IWorkspaceFilter } from 'src/app/features/clusterization-module/clusterization/workspace/models/workspaceFilter';
import { ClusterizationWorkspaceService } from 'src/app/features/clusterization-module/clusterization/workspace/service/clusterization-workspace.service';

@Component({
  selector: 'app-customer-workspaces-list-page',
  templateUrl: './customer-workspaces-list-page.component.html',
  styleUrl: './customer-workspaces-list-page.component.scss'
})
export class CustomerWorkspacesListPageComponent implements OnInit{
  request:IGetWorkspacesRequest={
    filterStr:'',
    typeId:undefined,
    pageParameters:{
      pageNumber:1,
      pageSize:10
    }
  }

  workspaces:ISimpleClusterizationWorkspace[]=[];

  constructor(private workspacesService:ClusterizationWorkspaceService,
    private toastr:MyToastrService,
    private router:Router){}
  ngOnInit(): void {
    this.loadFirst();
  }


  changeFilter(filter:IWorkspaceFilter){
    this.request.filterStr=filter.filterStr;
    this.request.typeId=filter.typeId;

    this.loadFirst();
  }

  isLoading:boolean;
  loadFirst(){
    if(this.isLoading)return;

    this.request.pageParameters.pageNumber=1;

    this.isLoading=true;
    this.workspacesService.getCustomerWorkspaces(this.request).subscribe(res=>{
      this.workspaces=res;
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
    if(this.isLoading2)return;
    this.isLoading2=true;
    this.workspacesService.getCustomerWorkspaces(this.request).subscribe(res=>{
      this.workspaces=this.workspaces.concat(res);
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
      this.request.pageParameters.pageNumber++;
      this.loadMore();
    }
  }
}
