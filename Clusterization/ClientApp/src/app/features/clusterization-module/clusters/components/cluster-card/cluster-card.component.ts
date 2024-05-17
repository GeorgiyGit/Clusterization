import { Component, Input, OnInit } from '@angular/core';
import { ICluster } from '../../models/responses/cluster';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { ClustersService } from '../../services/clusters.service';
import { IGetClusterDataRequest } from '../../models/requests/get-cluster-data-request';
import { IClusterData } from '../../models/responses/cluster-data';

@Component({
  selector: 'app-cluster-card',
  templateUrl: './cluster-card.component.html',
  styleUrl: './cluster-card.component.scss'
})
export class ClusterCardComponent implements OnInit{
  @Input() cluster: ICluster;

  isOpen:boolean=false;
  request:IGetClusterDataRequest={
    clusterId:-1,
    pageParameters:{
      pageNumber:1,
      pageSize:50
    }
  }

  entities:IClusterData[]=[];

  constructor(private clustersService:ClustersService,
    private toastr:MyToastrService){}
  ngOnInit(): void {
    this.request.clusterId=this.cluster.id;
  }
  
  isLoading: boolean;
  loadFirst() {
    if (this.isLoading) return;

    this.request.pageParameters.pageNumber = 1;

    this.isLoading = true;
    this.clustersService.getClusterEntities(this.request).subscribe(res => {
      this.entities = res;
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
    this.clustersService.getClusterEntities(this.request).subscribe(res => {
      this.entities = this.entities.concat(res);
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

  toggleOpen(){
    this.isOpen=!this.isOpen;
    if(this.isOpen){
      this.loadFirst();
    }
  }
}
