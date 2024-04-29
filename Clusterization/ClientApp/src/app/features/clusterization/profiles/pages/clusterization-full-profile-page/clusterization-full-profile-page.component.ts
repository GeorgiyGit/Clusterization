import { Component, OnInit } from '@angular/core';
import { IClusterizationProfile } from '../../models/responses/clusterization-profile';
import { ClusterizationProfilesService } from '../../services/clusterization-profiles.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ISelectAction } from 'src/app/core/models/select-action';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { OneClusterAlgorithmService } from '../../../algorithms/non-hierarchical/oneCluster/services/one-cluster-algorithm.service';
import { Clipboard } from '@angular/cdk/clipboard';
import { KMeansService } from '../../../algorithms/non-hierarchical/k-means/services/kmeans.service';
import { DbscanService } from '../../../algorithms/non-hierarchical/dbscan/services/dbscan.service';
import { SpectralClusteringService } from '../../../algorithms/non-hierarchical/spectral-clustering/services/spectral-clustering.service';
import { GaussianMixtureService } from '../../../algorithms/non-hierarchical/gaussian-mixture/services/gaussian-mixture.service';
import { AccountService } from 'src/app/features/account/services/account.service';
import { EmbeddingsLoadingService } from 'src/app/features/embeddings/services/embeddings-loading.service';
import { GeneralClusterizationAlgorithmsService } from '../../../algorithms/abstractAlgorithm/services/general-clusterization-algorithms.service';

@Component({
  selector: 'app-clusterization-full-profile-page',
  templateUrl: './clusterization-full-profile-page.component.html',
  styleUrls: ['./clusterization-full-profile-page.component.scss']
})
export class ClusterizationFullProfilePageComponent implements OnInit {
  profile: IClusterizationProfile;

  algorithmTypeStr: string = $localize`Тип алгоритм`;
  dimensionCountStr: string = $localize`Кількість вимірів`;
  clustersCountStr: string = $localize`Кількість кластерів`;

  actions: ISelectAction[] = [
    {
      name: $localize`Завантажити ембедингі`,
      action: () => {
        let userId = this.accountService.getUserId();
        if (this.profile.changingType === 'OnlyOwner' && (userId == null || userId != this.profile.ownerId)) {
          this.toastr.error($localize`Цей профіль може змінювати тільки власник!!!`);
          return;
        }

        this.isEmbeddingConfirmOpen=true;
      },
      isForAuthorized: true
    },
    {
      name: $localize`Кластеризувати`,
      action: () => {
        let userId = this.accountService.getUserId();
        if (this.profile.changingType === 'OnlyOwner' && (userId == null || userId != this.profile.ownerId)) {
          this.toastr.error($localize`Цей профіль може змінювати тільки власник!!!`);
          return;
        }

        if(!this.profile.isAllEmbeddingsLoaded){
          this.toastr.error($localize`Для цього профіля не завантажено всі ембедингі!!!`);
          return;
        }

        this.isClusterizationConfirmOpen=true;
      },
      isForAuthorized: true
    }
  ]


  quotasCountMainText = $localize`Кількість квот`;
  embeddingsQuotasCountText = $localize`Розрахована кількість квот ембедингів:`;
  clusterizationQuotasCountText = $localize`Розрахована кількість квот кластеризації:`;

  isEmbeddingConfirmOpen: boolean;
  isClusterizationConfirmOpen: boolean;

  isLoading: boolean;
  constructor(private profilesService: ClusterizationProfilesService,
    private route: ActivatedRoute,
    private oneClusterService: OneClusterAlgorithmService,
    private dbScanService: DbscanService,
    private spectralClusteringService: SpectralClusteringService,
    private gaussianMixtureService: GaussianMixtureService,
    private router: Router,
    private toastr: MyToastrService,
    private clipboard: Clipboard,
    private accountService: AccountService,
    private kMeansService: KMeansService,
    private embeddingsLoadingService: EmbeddingsLoadingService,
    private generalAlgorithmsService: GeneralClusterizationAlgorithmsService) { }
  ngOnInit(): void {
    let id = this.route.snapshot.params['id'];

    this.isLoading = true;
    this.profilesService.getFullById(id).subscribe(res => {
      this.profile = res;
      this.isLoading = false;

      this.calculateEmbeddingQuotasCount();
      this.calculateClusterizationQuotasCount();
    }, error => {
      this.isLoading = false;
      this.toastr.error(error.error.Message);
    });
  }

  embeddingsConfirm(result: boolean) {
    this.isEmbeddingConfirmOpen=false;
    if (result == false) {
      return;
    }

    this.embeddingsLoadingService.loadEmbeddingsByProfile(this.profile.id).subscribe(res => {
      this.toastr.success($localize`Завдання успішно створено`);
    }, error => {
      this.toastr.error(error.error.Message);
    })
  }

  clusterizationConfirm(result: boolean) {
    this.isClusterizationConfirmOpen=false;
    if (result == false) {
      return;
    }


    switch (this.profile.algorithmType.id) {
      case 'KMeans':
        this.kMeansService.clusterData(this.profile.id).subscribe(res => {
        }, error => {
          this.toastr.error(error.error.Message);
        });
        break;
      case 'OneCluster':
        this.oneClusterService.clusterData(this.profile.id).subscribe(res => {
        }, error => {
          this.toastr.error(error.error.Message);
        });
        break;
      case 'DBSCAN':
        this.dbScanService.clusterData(this.profile.id).subscribe(res => {
        }, error => {
          this.toastr.error(error.error.Message);
        });
        break;
      case 'SpectralClustering':
        this.spectralClusteringService.clusterData(this.profile.id).subscribe(res => {
        }, error => {
          this.toastr.error(error.error.Message);
        });
        break;
      case 'GaussianMixture':
        this.gaussianMixtureService.clusterData(this.profile.id).subscribe(res => {
        }, error => {
          this.toastr.error(error.error.Message);
        });
        break;
    }
  }

  embeddingQuotasCount: number;
  calculateEmbeddingQuotasCount() {
    this.profilesService.calculateQuotasCount(this.profile.id).subscribe(res => {
      this.embeddingQuotasCount = res;
    })
  }

  clusterizationQuotasCount: number;
  calculateClusterizationQuotasCount() {
    this.generalAlgorithmsService.calculateQuotasCountByWorkspace(this.profile.algorithmType.id, this.profile.workspaceId, this.profile.dimensionCount).subscribe(res => {
      this.clusterizationQuotasCount = res;
    });
  }

  copyToClipboard(msg: string, text: string, event: MouseEvent) {
    event.stopPropagation();

    this.clipboard.copy(text);

    this.toastr.success(msg + ' ' + $localize`скопійовано!!!`);
  }
  openMap(event: MouseEvent) {
    if (!this.profile.isCalculated) {
      this.toastr.error($localize`Цей профіль не був кластеризований!`);
      return;
    }

    this.router.navigateByUrl('profiles/full/' + this.profile.id + '/profile-points-map/' + this.profile.id);
  }
}
