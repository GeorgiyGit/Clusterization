import { Component, OnInit } from '@angular/core';
import { IClusterizationWorkspace } from '../../models/clusterizationWorkspace';
import { ClusterizationWorkspaceService } from '../../service/clusterization-workspace.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { Clipboard } from '@angular/cdk/clipboard';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { ISelectAction } from 'src/app/core/models/select-action';
import { AccountService } from 'src/app/features/account/services/account.service';
import { HttpEventType, HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-workspace-full-page',
  templateUrl: './workspace-full-page.component.html',
  styleUrls: ['./workspace-full-page.component.scss']
})
export class WorkspaceFullPageComponent implements OnInit {
  workspace: IClusterizationWorkspace;

  actions: ISelectAction[] = [];

  workspaceTypeNameStr: string = $localize`Тип`
  dateStr: string = $localize`Дату`;
  entitiesCountStr: string = $localize`Кількість елементів`;
  profilesCountStr: string = $localize`Кількість профілів`;

  isLoading: boolean;

  private downloadFile = (data: HttpResponse<Blob>) => {
    const downloadedFile = new Blob([data.body as BlobPart], { type: data.body?.type });
    const a = document.createElement('a');
    a.setAttribute('style', 'display:none;');
    document.body.appendChild(a);
    a.download = 'entities.txt';
    a.href = URL.createObjectURL(downloadedFile);
    a.target = '_blank';
    a.click();
    document.body.removeChild(a);
  }


  constructor(private workspaceService: ClusterizationWorkspaceService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: MyToastrService,
    private clipboard: Clipboard,
    private myLocalStorage: MyLocalStorageService,
    private accountService: AccountService) { }
  ngOnInit(): void {
    let id = this.route.snapshot.params['id'];

    this.isLoading = true;
    this.workspaceService.getFullById(id).subscribe(res => {
      this.workspace = res;

      this.actions = [
        {
          name: $localize`Додати профіль`,
          action: () => {
            let userId = this.accountService.getUserId();
            if (this.workspace.changingType === 'OnlyOwner' && (userId == null || userId != this.workspace.ownerId)) {
              this.toastr.error($localize`Цей робочий простір може змінювати тільки власник`);
              return;
            }

            this.router.navigate([{ outlets: { overflow: 'profiles/add/' + this.workspace.id } }]);
          },
          isForAuthorized: true
        },
        {
          name: $localize`Завантажити ембедінги`,
          action: () => {
            let userId = this.accountService.getUserId();
            if (this.workspace.changingType === 'OnlyOwner' && (userId == null || userId != this.workspace.ownerId)) {
              this.toastr.error($localize`Цей робочий простір може змінювати тільки власник`);
              return;
            }

            this.workspaceService.embeddingData(this.workspace.id).subscribe(res => {
            }, error => {
              this.toastr.error(error.error.Message);
            });
          },
          isForAuthorized: true
        },
        {
          name: $localize`Завантажити файл з текстовими даними`,
          action: () => {
            let userId = this.accountService.getUserId();
            if (this.workspace.changingType === 'OnlyOwner' && (userId == null || userId != this.workspace.ownerId)) {
              this.toastr.error($localize`Цей робочий простір може змінювати тільки власник`);
              return;
            }

            this.workspaceService.downloadEntitiesFile(this.workspace.id).subscribe((event) => {
              if (event.type === HttpEventType.Response) {
                this.downloadFile(event);
              }
            });
          },
          isForAuthorized: true
        }
      ];

      this.isLoading = false;

      if (this.workspace.id != this.myLocalStorage.getSelectedWorkspace()) {
        this.actions.push({
          name: $localize`Встановити активним`,
          action: () => {
            let userId = this.accountService.getUserId();
            if (this.workspace.changingType === 'OnlyOwner' && (userId == null || userId != this.workspace.ownerId)) {
              this.toastr.error($localize`Цей робочий простір може змінювати тільки власник`);
              return;
            }
            this.myLocalStorage.setSelectedWorkspace(this.workspace.id);
          },
          isForAuthorized: true
        });
      }
      if (this.workspace.typeId == 'External') {
        this.actions.push({
          name: $localize`Завантажити зовнішні дані`,
          action: () => {
            let userId = this.accountService.getUserId();
            if (this.workspace.changingType === 'OnlyOwner' && (userId == null || userId != this.workspace.ownerId)) {
              this.toastr.error($localize`Цей робочий простір може змінювати тільки власник`);
              return;
            }
            this.router.navigate([{ outlets: { overflow: 'workspace/add-external-data/' + this.workspace.id } }]);
          },
          isForAuthorized: true
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

    this.toastr.success(msg + ' ' + $localize`скопійовано!!!`);
  }
}
