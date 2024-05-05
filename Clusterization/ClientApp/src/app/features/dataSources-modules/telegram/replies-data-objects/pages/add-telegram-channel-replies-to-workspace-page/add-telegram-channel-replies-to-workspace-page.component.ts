import { Component, OnInit } from '@angular/core';
import { IAddTelegramRepliesToWorkspaceByChannelRequest } from '../../models/add-tg-replies-by-channel';
import { TelegramRepliesDataObjectsService } from '../../services/telegram-replies-data-objects.service';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-add-telegram-channel-replies-to-workspace-page',
  templateUrl: './add-telegram-channel-replies-to-workspace-page.component.html',
  styleUrl: './add-telegram-channel-replies-to-workspace-page.component.scss',
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
export class AddTelegramChannelRepliesToWorkspacePageComponent implements OnInit{
  animationState:string='in';
  channelId:number | undefined;

  optionsForm: FormGroup = this.fb.group({
    dateFrom: [null],
    dateTo: [null],
    maxCount:[]
  });

  get formValue() {
    return this.optionsForm.value as IAddTelegramRepliesToWorkspaceByChannelRequest;
  }

  get dateFrom() { return (this.optionsForm.get('dateFrom')!); }
  get dateTo() { return this.optionsForm.get('dateTo')!; }
  get maxCount() { return this.optionsForm.get('maxCount')!; }

  constructor(private router:Router,
    private fb: FormBuilder,
    private route:ActivatedRoute,
    private telegramRepliesDataObjectsService:TelegramRepliesDataObjectsService,
    private toaster:MyToastrService,
    private storageService:MyLocalStorageService){}
  ngOnInit(): void {
    this.animationState='in';

    this.channelId=this.route.snapshot.params['channelId'];
  }

  closeOverflow(){
    this.animationState='hidden';
    setTimeout(() => {
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, 300);
  }

  isMessageDateCount:boolean=true;

  isLoading:boolean;
  load(){
    let options = this.formValue;

    options.isMessageDateCount=true;

    if(this.channelId!=null){
      options.channelId=this.channelId;
    }

    if(options.maxCount<=0){
      this.toaster.error($localize`Максимальна кількість дорівнює нулю`);
      return;
    }

    let workspaceId =this.storageService.getSelectedWorkspace();

    if(workspaceId==null){
      this.toaster.error($localize`Робочий простір не вибрано`);
      return;
    }
    options.workspaceId=workspaceId;

    this.telegramRepliesDataObjectsService.addRepliesByChannel(options).subscribe(res=>{
      this.toaster.success($localize`Завдання створено`);
      this.isLoading=false;
      this.closeOverflow();
    },error=>{
      this.isLoading=false;
      this.toaster.error(error.error.Message);
    });
  }


  selectMessageDate(event:any){
    this.isMessageDateCount=true;
  }
  
  selectTelegramDate(event:any){
    this.isMessageDateCount=false;
  }
}
