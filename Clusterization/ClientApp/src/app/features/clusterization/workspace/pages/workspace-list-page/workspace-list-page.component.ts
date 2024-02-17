import { Component, OnInit } from '@angular/core';
import { IGetWorkspacesRequest } from '../../models/requests/getWorkspacesRequest';
import { ISimpleClusterizationWorkspace } from '../../models/simpleClusterizationWorkspace';
import { ClusterizationWorkspaceService } from '../../service/clusterization-workspace.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IWorkspaceFilter } from '../../models/workspaceFilter';
import { Router } from '@angular/router';

@Component({
  selector: 'app-workspace-list-page',
  templateUrl: './workspace-list-page.component.html',
  styleUrls: ['./workspace-list-page.component.scss']
})
export class WorkspaceListPageComponent implements OnInit{
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
    this.request.pageParameters.pageNumber=0;

    this.isLoading=true;
    this.workspacesService.getWorkspaces(this.request).subscribe(res=>{
      this.workspaces=res;
      this.isLoading=false;

      if(res.length<this.request.pageParameters.pageSize)this.isLoadMoreAvailable=false;
      else this.isLoadMoreAvailable=true;
    },error=>{
      this.isLoading=false;
      this.toastr.error(error.error.Message);
    });
  }
  loadMore(){
    this.isLoading=true;
    this.workspacesService.getWorkspaces(this.request).subscribe(res=>{
      this.workspaces=this.workspaces.concat(res);
      this.isLoading=false;

      if(res.length<this.request.pageParameters.pageSize)this.isLoadMoreAvailable=false;
      else this.isLoadMoreAvailable=true;
    },error=>{
      this.isLoading=false;
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

  openAddWorkspace(event:MouseEvent){
    this.router.navigate([{outlets: {overflow: 'workspaces_add'}}]);
  }
}
