import { Component, Input, OnInit } from '@angular/core';
import { ISimpleWorkspaceAddDataPack } from '../../models/responses/simple-workspace-add-data-pack';
import { Router } from '@angular/router';
import { ISelectAction } from 'src/app/core/models/select-action';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { Clipboard } from '@angular/cdk/clipboard';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { WorkspaceDataObjectsAddPacksService } from '../../services/workspace-data-objects-add-packs.service';
import { AccountService } from 'src/app/features/shared-module/account/services/account.service';

@Component({
  selector: 'app-simple-workspace-add-data-pack-card',
  templateUrl: './simple-workspace-add-data-pack-card.component.html',
  styleUrl: './simple-workspace-add-data-pack-card.component.scss'
})
export class SimpleWorkspaceAddDataPackCardComponent implements OnInit {
  @Input() pack: ISimpleWorkspaceAddDataPack;
  actions: ISelectAction[] = [];

  entitiesCountStr: string = $localize`Кількість елементів`;

  isYour: boolean = false;
  constructor(private router: Router,
    public myLocalStorage: MyLocalStorageService,
    private toastr: MyToastrService,
    private clipboard: Clipboard,
    public accountService: AccountService,
    private packsService:WorkspaceDataObjectsAddPacksService) { }

  ngOnInit(): void {
    let id = this.accountService.getUserId();
    if (id != null && this.pack != null && id == this.pack.owner.id) {
      this.isYour = true;
    }

    this.updateActions();
  }
  
  updateActions(){
    this.actions=[{
      name:$localize`Завантажити ембедингі`,
      action: () => {
        let userId = this.accountService.getUserId();
        if (this.pack.workspaceChangingType === 'OnlyOwner' && (userId == null || userId != this.pack.owner.id)) {
          this.toastr.error($localize`Цей пакет даних може змінювати тільки власник робочого простору`);
          return;
        }

        this.router.navigate([{ outlets: { overflow: 'clusterization/embeddings-loading/load-by-data-pack/' + this.pack.id } }]);
      },
      isForAuthorized: true,
      isOnlyForUsers:true
    }];
    if(!this.pack.isDeleted){
      this.actions.push({
        name: $localize`Видалити`,
        action: () => {
          let userId = this.accountService.getUserId();
          if (this.pack.workspaceChangingType === 'OnlyOwner' && (userId == null || userId != this.pack.owner.id)) {
            this.toastr.error($localize`Цей пакет даних може змінювати тільки власник робочого простору`);
            return;
          }
  
          this.packsService.delete(this.pack.id).subscribe(res => {
            this.toastr.success($localize`Пакет даних успішно видалено`);
            this.pack.isDeleted=true;
            this.updateActions();
          }, error => {
            this.toastr.error(error.error.Message);
          });
        },
        isForAuthorized: true,
        isOnlyForUsers:true
      });
    }
    else{
      this.actions.push(    {
        name: $localize`Відновити`,
        action: () => {
          let userId = this.accountService.getUserId();
          if (this.pack.workspaceChangingType === 'OnlyOwner' && (userId == null || userId != this.pack.owner.id)) {
            this.toastr.error($localize`Цей пакет даних може змінювати тільки власник робочого простору`);
            return;
          }
  
          this.packsService.restore(this.pack.id).subscribe(res => {
            this.toastr.success($localize`Пакет даних успішно відновлено`);
            this.pack.isDeleted=false;
            this.updateActions();
          }, error => {
            this.toastr.error(error.error.Message);
          });
        },
        isForAuthorized: true,
        isOnlyForUsers:true
      })
    }
  }

  openFull() {
    this.router.navigate([{ outlets: { overflow: 'clusterization/workspace-add-data-packs/full/' + this.pack.id } }]);
  }

  copyToClipboard(msg: string, text: string, event: MouseEvent) {
    event.stopPropagation();

    this.clipboard.copy(text);

    this.toastr.success(msg + ' ' + $localize`скопійовано!!!`);
  }
}
