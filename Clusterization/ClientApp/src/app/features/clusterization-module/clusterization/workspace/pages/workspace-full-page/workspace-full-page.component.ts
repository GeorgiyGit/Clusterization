import { Component, OnInit } from '@angular/core';
import { IClusterizationWorkspace } from '../../models/responses/clusterizationWorkspace';
import { ClusterizationWorkspaceService } from '../../service/clusterization-workspace.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { Clipboard } from '@angular/cdk/clipboard';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { ISelectAction } from 'src/app/core/models/select-action';
import { HttpEventType, HttpResponse } from '@angular/common/http';
import { AccountService } from 'src/app/features/shared-module/account/services/account.service';

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

            this.router.navigate([{ outlets: { overflow: 'clusterization/profiles/add/' + this.workspace.id } }]);
          },
          isForAuthorized: true,
          isOnlyForUsers:true
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
          isForAuthorized: true,
          isOnlyForUsers:true
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
          isForAuthorized: true,
          isOnlyForUsers:true
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
            this.router.navigate([{ outlets: { overflow: 'dataSources/externalData/load-and-add-data-objects/' + this.workspace.id } }]);
          },
          isForAuthorized: true,
          isOnlyForUsers:true
        });
      }
      if(this.accountService.getUserId()==this.workspace.ownerId){
        this.actions.push({
          name: $localize`Оновити`,
          action: () => {
            let userId = this.accountService.getUserId();
            if (this.workspace.ownerId!=userId) {
              this.toastr.error($localize`Цей робочий простір може оновлювати тільки власник`);
              return;
            }

            this.router.navigate([{ outlets: { overflow: 'clusterization/workspaces/update/' + this.workspace.id } }]);
          },
          isForAuthorized: true,
          isOnlyForUsers:true
        });
      }
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
