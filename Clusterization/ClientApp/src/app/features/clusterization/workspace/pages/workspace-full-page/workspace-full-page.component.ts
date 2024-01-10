import { Component, OnInit } from '@angular/core';
import { IClusterizationWorkspace } from '../../models/clusterizationWorkspace';
import { ClusterizationWorkspaceService } from '../../service/clusterization-workspace.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { Clipboard } from '@angular/cdk/clipboard';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { ISelectAction } from 'src/app/core/models/select-action';

@Component({
  selector: 'app-workspace-full-page',
  templateUrl: './workspace-full-page.component.html',
  styleUrls: ['./workspace-full-page.component.scss']
})
export class WorkspaceFullPageComponent implements OnInit {
  workspace: IClusterizationWorkspace;

  actions: ISelectAction[] = [];

  isLoading: boolean;
  constructor(private workspaceService: ClusterizationWorkspaceService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: MyToastrService,
    private clipboard: Clipboard,
    private myLocalStorage: MyLocalStorageService) { }
  ngOnInit(): void {
    let id = this.route.snapshot.params['id'];

    this.isLoading = true;
    this.workspaceService.getFullById(id).subscribe(res => {
      this.workspace = res;

      this.actions = [
        {
          name: 'Додати профіль',
          action: () => {
            this.router.navigate([{ outlets: { overflow: 'profiles/add/' + this.workspace.id } }]);
          }
        },
        {
          name: 'Завантажити ембедінги',
          action: () => {
            this.workspaceService.embeddingData(this.workspace.id).subscribe(res => {
            }, error => {
              this.toastr.error(error.error.Message);
            });
          }
        }
      ];

      this.isLoading = false;

      if (this.workspace.id != this.myLocalStorage.getSelectedWorkspace()) {
        this.actions.push({
          name: 'Встановити активним',
          action: () => {
            this.myLocalStorage.setSelectedWorkspace(this.workspace.id);
          }
        });
      }
      if (this.workspace.typeId == 'External') {
        this.actions.push({
          name: 'Завантажити зовнішні дані',
          action: () => {
            this.router.navigate([{ outlets: { overflow: 'workspace/add-external-data/' + this.workspace.id } }]);
          }
        });
      }

      this.router.navigateByUrl('workspaces/full/' + this.workspace.id + '/profiles-list/' + this.workspace.id);
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
