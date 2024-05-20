import { Component, OnInit } from '@angular/core';
import { ClustersService } from '../../services/clusters.service';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { HttpEventType, HttpResponse } from '@angular/common/http';
import { IGetClustersFileRequest } from '../../models/requests/get-clusters-file-request';

@Component({
  selector: 'app-load-clusters-file-page',
  templateUrl: './load-clusters-file-page.component.html',
  styleUrl: './load-clusters-file-page.component.scss',
  animations: [
    trigger('popUpAnimation', [
      state('in', style({ transform: 'translateY(0)' })),
      state('hidden', style({ transform: 'translateY(100%)' })),
      transition('void => in', [
        style({ transform: 'translateY(100%)' }),
        animate('300ms cubic-bezier(0.4, 0, 0.2, 1)')
      ]),
      transition('in => hidden', animate('300ms cubic-bezier(0.4, 0, 0.2, 1)'))
    ])
  ]
})
export class LoadClustersFilePageComponent implements OnInit {
  animationState: string = 'in';
  profileId: number;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private clustersService: ClustersService,
    private toaster: MyToastrService) { }
  ngOnInit(): void {
    this.profileId = parseInt(this.route.snapshot.params['profileId']);
    this.animationState = 'in';
  }

  private downloadFile = (data: HttpResponse<Blob>) => {
    const downloadedFile = new Blob([data.body as BlobPart], { type: data.body?.type });
    const a = document.createElement('a');
    a.setAttribute('style', 'display:none;');
    document.body.appendChild(a);
    a.download = 'clusters.txt';
    a.href = URL.createObjectURL(downloadedFile);
    a.target = '_blank';
    a.click();
    document.body.removeChild(a);
    this.closeOverflow();
  }

  closeOverflow() {
    this.animationState = 'hidden';
    setTimeout(() => {
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, 300);
  }

  isSelectAllClusters: boolean = true;
  clusterIds: number[] = [];

  isLoading: boolean;
  load() {
    if (!this.isSelectAllClusters && (this.clusterIds == null || this.clusterIds.length == 0)) {
      this.toaster.error($localize`Не вибрано жодного кластера`);
      return;
    }

    let request: IGetClustersFileRequest;
    if (this.isSelectAllClusters) {
      request = {
        profileId: this.profileId,
        clusterIds: []
      }
    }
    else {
      request = {
        profileId: this.profileId,
        clusterIds: this.clusterIds
      }
    }

    this.isLoading = true;
    this.clustersService.getClustersFile(request).subscribe((event) => {
      if (event.type === HttpEventType.Response) {
        this.isLoading = false;
        this.downloadFile(event);
      }
    }, error => {
      this.isLoading = false;
      this.toaster.error(error.error.Message);
    });
  }


  selectisSelectAllClusters(value: boolean, event: any) {
    this.isSelectAllClusters = value;
  }

  selectClusterId(id: number) {
    this.clusterIds.push(id);
  }
  unselectClusterId(id: number) {
    this.clusterIds = this.clusterIds.filter(e => e != id);
  }
}
