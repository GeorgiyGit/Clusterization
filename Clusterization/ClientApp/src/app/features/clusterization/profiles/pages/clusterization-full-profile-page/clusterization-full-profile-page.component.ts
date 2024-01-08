import { Component, OnInit } from '@angular/core';
import { IClusterizationProfile } from '../../models/clusterization-profile';
import { ClusterizationProfilesService } from '../../services/clusterization-profiles.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ISelectAction } from 'src/app/core/models/select-action';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { OneClusterAlgorithmService } from '../../../algorithms/non-hierarchical/oneCluster/services/one-cluster-algorithm.service';
import { Clipboard } from '@angular/cdk/clipboard';
import { KMeansService } from '../../../algorithms/non-hierarchical/k-means/services/kmeans.service';
import { DbscanService } from '../../../algorithms/non-hierarchical/dbscan/services/dbscan.service';
import { SpectralClusteringService } from '../../../algorithms/non-hierarchical/spectral-clustering/services/spectral-clustering.service';
import { GaussianMixtureService } from '../../../algorithms/non-hierarchical/gaussian-mixture/services/gaussian-mixture.service';

@Component({
  selector: 'app-clusterization-full-profile-page',
  templateUrl: './clusterization-full-profile-page.component.html',
  styleUrls: ['./clusterization-full-profile-page.component.scss']
})
export class ClusterizationFullProfilePageComponent implements OnInit {
  profile: IClusterizationProfile;

  actions:ISelectAction[]=[
    {
      name:'Кластеризувати',
      action:()=>{
        switch(this.profile.algorithmType.id){
          case 'KMeans':
            this.kMeansService.clusterData(this.profile.id).subscribe(res=>{
            },error=>{
              this.toastr.error(error.error.Message);
            });
          break;
          case 'OneCluster':
            this.oneClusterService.clusterData(this.profile.id).subscribe(res=>{
            },error=>{
              this.toastr.error(error.error.Message);
            });
          break;
          case 'DBScan':
            this.dbScanService.clusterData(this.profile.id).subscribe(res=>{
            },error=>{
              this.toastr.error(error.error.Message);
            });
          break;
          case 'SpectralClustering':
            this.spectralClusteringService.clusterData(this.profile.id).subscribe(res=>{
            },error=>{
              this.toastr.error(error.error.Message);
            });
          break;
          case 'GaussianMixture':
            this.gaussianMixtureService.clusterData(this.profile.id).subscribe(res=>{
            },error=>{
              this.toastr.error(error.error.Message);
            });
          break;
        }
      }
    }
  ]

  isLoading: boolean;
  constructor(private profilesService: ClusterizationProfilesService,
    private route: ActivatedRoute,
    private oneClusterService:OneClusterAlgorithmService,
    private dbScanService:DbscanService,
    private spectralClusteringService:SpectralClusteringService,
    private gaussianMixtureService:GaussianMixtureService,
    private router:Router,
    private toastr: MyToastrService,
    private clipboard: Clipboard,
    private myLocalStorage:MyLocalStorageService,
    private kMeansService:KMeansService) { }
  ngOnInit(): void {
    let id = this.route.snapshot.params['id'];

    this.isLoading = true;
    this.profilesService.getFullById(id).subscribe(res => {
      this.profile = res;
      
      this.isLoading = false;
    }, error => {
      this.isLoading = false;
      this.toastr.error(error.error.Message);
    });
  }

  copyToClipboard(msg: string, text: string, event: MouseEvent) {
    event.stopPropagation();

    this.clipboard.copy(text);

    this.toastr.success(msg + ' ' + 'скопійовано!!!');
  }
}
