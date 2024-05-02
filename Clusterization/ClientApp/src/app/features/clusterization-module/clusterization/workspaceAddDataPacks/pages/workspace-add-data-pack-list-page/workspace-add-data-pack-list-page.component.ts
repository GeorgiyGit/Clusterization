import { Component, OnInit } from '@angular/core';
import { IGetWorkspaceAddDataPacksRequest } from '../../models/requests/get-workspace-add-data-packs-request';
import { ISimpleWorkspaceAddDataPack } from '../../models/responses/simple-workspace-add-data-pack';
import { WorkspaceDataObjectsAddPacksService } from '../../services/workspace-data-objects-add-packs.service';
import { ActivatedRoute } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-workspace-add-data-pack-list-page',
  templateUrl: './workspace-add-data-pack-list-page.component.html',
  styleUrl: './workspace-add-data-pack-list-page.component.scss'
})
export class WorkspaceAddDataPackListPageComponent implements OnInit{
  request:IGetWorkspaceAddDataPacksRequest={
    workspaceId:-1,
    pageParameters:{
      pageNumber:1,
      pageSize:10
    }
  }
  packs:ISimpleWorkspaceAddDataPack[]=[];


  constructor(private toastr:MyToastrService,
    private route:ActivatedRoute,
    private packsService:WorkspaceDataObjectsAddPacksService){}
  ngOnInit(): void {
    let id = this.route.snapshot.params['workspaceId'];
    this.request.workspaceId=id;

    this.loadFirst();
  }

  isLoading:boolean;
  loadFirst(){
    this.request.pageParameters.pageNumber=1;

    this.isLoading=true;
    this.packsService.getSimplePacks(this.request).subscribe(res=>{
      this.packs=res;
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
    this.packsService.getCustomerSimplePacks(this.request).subscribe(res=>{
      this.packs=this.packs.concat(res);
      this.isLoading2=false;

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
}
