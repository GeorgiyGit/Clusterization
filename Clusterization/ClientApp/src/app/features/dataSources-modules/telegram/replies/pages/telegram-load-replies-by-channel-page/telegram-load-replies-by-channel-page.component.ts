import { Component, OnInit } from '@angular/core';
import { ILoadTelegramRepliesByChannelRequest } from '../../models/load-telegram-replies-by-channel-request';
import { TelegramRepliesService } from '../../services/telegram-replies.service';
import { QuotasCalculationList } from 'src/app/features/shared-module/quotas/static/quotas-calculation-list';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-telegram-load-replies-by-channel-page',
  templateUrl: './telegram-load-replies-by-channel-page.component.html',
  styleUrl: './telegram-load-replies-by-channel-page.component.scss',
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
export class TelegramLoadRepliesByChannelPageComponent implements OnInit{
  animationState:string='in';
  channelId:number | undefined;

  optionsForm: FormGroup = this.fb.group({
    dateFrom: [null],
    dateTo: [null],
    maxLoad:[],
    maxLoadForOneMessage:[]
  });

  get formValue() {
    return this.optionsForm.value as ILoadTelegramRepliesByChannelRequest;
  }

  get dateFrom() { return (this.optionsForm.get('dateFrom')!); }
  get dateTo() { return this.optionsForm.get('dateTo')!; }
  get maxLoad() { return this.optionsForm.get('maxLoad')!; }
  get maxLoadForOneMessage() { return this.optionsForm.get('maxLoadForOneMessage')!; }

  quotasCount:number;
  constructor(private router:Router,
    private fb: FormBuilder,
    private route:ActivatedRoute,
    private repliesService:TelegramRepliesService,
    private toaster:MyToastrService){}
  ngOnInit(): void {
    this.animationState='in';

    this.channelId=this.route.snapshot.params['channelId'];

    this.quotasCount = QuotasCalculationList.telegramReply;
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

    if(options.maxLoad<=0){
      this.toaster.error($localize`Максимальна кількість дорівнює нулю`);
      return;
    }
    if(options.maxLoadForOneMessage<=0){
      this.toaster.error($localize`Максимальна кількість для одного повідомлення дорівнює нулю`);
      return;
    }

    this.repliesService.loadFromChannel(options).subscribe(res=>{
      this.toaster.success($localize`Завдання створено`);
      this.isLoading=false;
      this.closeOverflow();
    },error=>{
      this.isLoading=false;
      this.toaster.error(error.error.Message);
    });
  }
}
