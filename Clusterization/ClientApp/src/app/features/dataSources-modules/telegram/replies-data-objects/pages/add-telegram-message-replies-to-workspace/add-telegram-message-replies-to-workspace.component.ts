import { trigger, state, style, transition, animate } from '@angular/animations';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { ISimpleTelegramMessage } from '../../../messages/models/responses/simple-telegram-message';
import { TelegramRepliesDataObjectsService } from '../../services/telegram-replies-data-objects.service';
import { IAddTelegramRepliesToWorkspaceByMessagesRequest } from './../../models/add-tg-replies-by-messages';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-add-telegram-message-replies-to-workspace',
  templateUrl: './add-telegram-message-replies-to-workspace.component.html',
  styleUrl: './add-telegram-message-replies-to-workspace.component.scss',
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
export class AddTelegramMessageRepliesToWorkspaceComponent implements OnInit{
  animationState:string='in';
  channelId:number;

  messages:ISimpleTelegramMessage[]=[];

  optionsForm: FormGroup = this.fb.group({
    dateFrom: [null],
    dateTo: [null],
    maxCountInVideo:[]
  });

  get formValue() {
    return this.optionsForm.value as IAddTelegramRepliesToWorkspaceByMessagesRequest;
  }

  get dateFrom() { return (this.optionsForm.get('dateFrom')!); }
  get dateTo() { return this.optionsForm.get('dateTo')!; }
  get maxCountInMessage() { return this.optionsForm.get('maxCountInMessage')!; }

  constructor(private router:Router,
    private fb: FormBuilder,
    private route:ActivatedRoute,
    private repliesDataObjectsService:TelegramRepliesDataObjectsService,
    private toaster:MyToastrService,
    private storageService:MyLocalStorageService){}
  ngOnInit(): void {
    this.animationState='in';

    let channelId=this.route.snapshot.params['channelId'];

    if(channelId!=null){
      this.channelId=channelId;
    }
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

    if(options.maxCountInMessage<=0){
      this.toaster.error($localize`Максимальна кількість дорівнює нулю`);
      return;
    }

    let workspaceId =this.storageService.getSelectedWorkspace();

    if(workspaceId==null){
      this.toaster.error($localize`Робочий простір не вибрано`);
      return;
    }

    if(this.messages.length==0){
      this.toaster.error($localize`Жодного відео не вибрано`);
      return;
    }

    let ids:number[]=[];

    this.messages.forEach(message=>{
      ids.push(message.id);
    })

    options.messageIds=ids;
    options.workspaceId=workspaceId;

    this.repliesDataObjectsService.addRepliesByMessages(options).subscribe(res=>{
      this.toaster.success($localize`Завдання створено`);
      this.isLoading=false;
      this.closeOverflow();
    },error=>{
      this.isLoading=false;
      this.toaster.error(error.error.Message);
    });
  }

  selectMessage(video:ISimpleTelegramMessage){
    this.messages.push(video);
  }
  unselectMessage(video:ISimpleTelegramMessage){
    this.messages=this.messages.filter(e=>e!=video);
  }
}
