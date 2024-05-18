import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { ClustersService } from '../../services/clusters.service';
import { IGetClustersRequest } from '../../models/requests/get-clusters-request';
import { ICluster } from '../../models/responses/cluster';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-clusters-list',
  templateUrl: './clusters-list.component.html',
  styleUrl: './clusters-list.component.scss'
})
export class ClustersListComponent implements OnInit, OnChanges {
  @Input() profileId:number=-1;
  clusters:ICluster[]=[];

  @Output() selectClusterEvent=new EventEmitter<number>();
  @Output() unSelectClusterEvent=new EventEmitter<number>();
  
  request:IGetClustersRequest={
    profileId:this.profileId,
    pageParameters:{
      pageNumber:1,
      pageSize:50
    }
  }

  constructor(private clustersService:ClustersService,
    private toastr:MyToastrService){}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['profileId'] && !changes['profileId'].firstChange) {
      if(this.profileId!=-1){
        this.request.profileId=this.profileId;
        this.loadFirst();
      }
    }
  }
  ngOnInit(): void {
    if(this.profileId!=-1){
      this.request.profileId=this.profileId;
      this.loadFirst();
    }
  }

  isLoading: boolean;
  loadFirst() {
    if (this.isLoading) return;

    this.request.pageParameters.pageNumber = 1;

    this.isLoading = true;
    this.clustersService.getClusters(this.request).subscribe(res => {
      this.clusters = res;
      this.isLoading = false;

      if (res.length < this.request.pageParameters.pageSize) this.isLoadMoreAvailable = false;
      else this.isLoadMoreAvailable = true;
    }, error => {
      this.isLoading = false;
      this.toastr.error(error.error.Message);
    });
  }

  isLoading2: boolean;
  loadMore() {
    if (this.isLoading2) return;
    this.isLoading2 = true;
    this.clustersService.getClusters(this.request).subscribe(res => {
      this.clusters = this.clusters.concat(res);
      this.isLoading2 = false;

      if (res.length < this.request.pageParameters.pageSize) this.isLoadMoreAvailable = false;
      else this.isLoadMoreAvailable = true;
    }, error => {
      this.isLoading = false;
      this.toastr.error(error.error.Message);
    });
  }

  isLoadMoreAvailable: boolean;
  addMore() {
    if (this.isLoadMoreAvailable) {
      this.request.pageParameters.pageNumber++;
      this.loadMore();
    }
  }

  selectEvent(id:number){
    this.selectClusterEvent.emit(id);
  }
  unselectEvent(id:number){
    this.unSelectClusterEvent.emit(id);
  }
}
