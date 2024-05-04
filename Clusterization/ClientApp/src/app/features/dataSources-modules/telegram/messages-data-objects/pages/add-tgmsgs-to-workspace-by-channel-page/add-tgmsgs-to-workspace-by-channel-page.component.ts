import { Component, OnInit } from '@angular/core';
import { IAddTelegramMessagesToWorkspaceByChannelRequest } from '../../models/AddTGMsgToWorkspaceByChannelRequest';
import { TelegramMessagesDataObjectsService } from '../../services/telegram-messages-data-objects.service';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-add-tgmsgs-to-workspace-by-channel-page',
  templateUrl: './add-tgmsgs-to-workspace-by-channel-page.component.html',
  styleUrl: './add-tgmsgs-to-workspace-by-channel-page.component.scss',
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
export class AddTGMsgsToWorkspaceByChannelPageComponent implements OnInit{
  animationState:string='in';
  channelId:number | undefined;

  optionsForm: FormGroup = this.fb.group({
    dateFrom: [null],
    dateTo: [null],
    maxCount:[]
  });

  get formValue() {
    return this.optionsForm.value as IAddTelegramMessagesToWorkspaceByChannelRequest;
  }

  get dateFrom() { return (this.optionsForm.get('dateFrom')!); }
  get dateTo() { return this.optionsForm.get('dateTo')!; }
  get maxCount() { return this.optionsForm.get('maxCount')!; }

  constructor(private router:Router,
    private fb: FormBuilder,
    private route:ActivatedRoute,
    private telegramMessagesDataObjectsService:TelegramMessagesDataObjectsService,
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

  isLoading:boolean;
  load(){
    let options = this.formValue;


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

    this.telegramMessagesDataObjectsService.addMessagesByChannel(options).subscribe(res=>{
      this.toaster.success($localize`Завдання створено`);
      this.isLoading=false;
      this.closeOverflow();
    },error=>{
      this.isLoading=false;
      this.toaster.error(error.error.Message);
    });
  }
}
