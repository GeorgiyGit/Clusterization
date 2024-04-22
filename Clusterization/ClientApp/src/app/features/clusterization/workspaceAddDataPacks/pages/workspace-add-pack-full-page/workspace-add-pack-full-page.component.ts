import { WorkspaceDataObjectsAddPacksService } from './../../services/workspace-data-objects-add-packs.service';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { Clipboard } from '@angular/cdk/clipboard';
import { IFullWorkspaceAddDataPack } from '../../models/responses/full-workspace-add-data-pack';
import { AccountService } from 'src/app/features/account/services/account.service';

@Component({
  selector: 'app-workspace-add-pack-full-page',
  templateUrl: './workspace-add-pack-full-page.component.html',
  styleUrl: './workspace-add-pack-full-page.component.scss',
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
export class WorkspaceAddPackFullPageComponent implements OnInit {
  animationState: string = 'in';

  entitiesCountStr: string = $localize`Кількість елементів`;

  isYour: boolean = false;
  pack:IFullWorkspaceAddDataPack;

  constructor(private router: Router,
    private route: ActivatedRoute,
    private packsService: WorkspaceDataObjectsAddPacksService,
    private toaster: MyToastrService,
    private clipboard: Clipboard,
    private accountService:AccountService) { }
  ngOnInit(): void {
    this.animationState = 'in';

    let packId = this.route.snapshot.params['id'];

    this.packsService.getFullPack(packId).subscribe(res=>{
      this.pack=res;

      let id = this.accountService.getUserId();
      if (id != null && this.pack != null && id == this.pack.owner.id) {
        this.isYour = true;
      }
    },error=>{
      this.toaster.error(error.error.Message);
    })
    
  }

  closeOverflow() {
    this.animationState = 'hidden';
    setTimeout(() => {
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, 300);
  }


  copyToClipboard(msg: string, text: string, event: MouseEvent) {
    event.stopPropagation();

    this.clipboard.copy(text);

    this.toaster.success(msg + ' ' + $localize`скопійовано!!!`);
  }
}
