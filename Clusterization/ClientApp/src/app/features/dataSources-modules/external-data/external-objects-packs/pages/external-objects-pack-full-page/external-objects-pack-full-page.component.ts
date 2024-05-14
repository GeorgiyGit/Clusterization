import { Component, OnInit } from '@angular/core';
import { IFullExternalObjectsPack } from '../../models/responses/full-external-objects-pack';
import { ISelectAction } from 'src/app/core/models/select-action';
import { ExternalObjectsPacksService } from '../../services/external-objects-packs.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { Clipboard } from '@angular/cdk/clipboard';
import { AccountService } from 'src/app/features/shared-module/account/services/account.service';

@Component({
  selector: 'app-external-objects-pack-full-page',
  templateUrl: './external-objects-pack-full-page.component.html',
  styleUrl: './external-objects-pack-full-page.component.scss'
})
export class ExternalObjectsPackFullPageComponent implements OnInit {
  pack: IFullExternalObjectsPack;

  dateStr:string=$localize`Дату`;
  countStr:string=$localize`Кількість`;
  entitiesCountStr: string = $localize`Кількість елементів`;
  actions: ISelectAction[] = [
    {
      name: $localize`Додати дані до робочого простору`,
      action: () => {
        let workspaceId = this.storageService.getSelectedWorkspace();

        if (workspaceId == null) {
          this.toastr.error($localize`Робочий простір не вибрано`);
          return;
        }
        this.packsService.add({
          workspaceId:workspaceId,
          packId:this.pack.id
        }).subscribe(res=>{
          this.toastr.success($localize`Завдання створено`);
        },error=>{
          this.toastr.error(error.error.Message);
        })
      },
      isForAuthorized: true,
      isOnlyForUsers:true
    }
  ]


  isLoading: boolean;
  constructor(private packsService: ExternalObjectsPacksService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: MyToastrService,
    private clipboard: Clipboard,
    private storageService: MyLocalStorageService,
    private accountService:AccountService) { }
  ngOnInit(): void {
    let id = this.route.snapshot.params['id'];

    this.isLoading = true;
    this.packsService.getFull(id).subscribe(res => {
      this.pack = res;

      this.isLoading = false;

      if(this.accountService.getUserId()==this.pack.ownerId){
        this.actions.push({
          name: $localize`Оновити`,
          action: () => {
            let userId = this.accountService.getUserId();
            if (this.pack.ownerId!=userId) {
              this.toastr.error($localize`Цей пакет зовнішніх даних може оновлювати тільки власник`);
              return;
            }

            this.router.navigate([{ outlets: { overflow: 'dataSources/externalData/packs/update/' + this.pack.id } }]);
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
