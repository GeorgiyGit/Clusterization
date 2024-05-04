import { Component, OnInit } from '@angular/core';
import { ITelegramLoadOptions } from '../../../messages/models/telegram-load-optionts';
import { TelegramRepliesService } from '../../services/telegram-replies.service';
import { QuotasCalculationList } from 'src/app/features/shared-module/quotas/static/quotas-calculation-list';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-telegram-load-group-replies-page',
  templateUrl: './telegram-load-group-replies-page.component.html',
  styleUrl: './telegram-load-group-replies-page.component.scss'
})
export class TelegramLoadGroupRepliesPageComponent implements OnInit{
  animationState:string='in';
  messageId:number | undefined;

  optionsForm: FormGroup = this.fb.group({
    dateTo: [null],
    maxLoad:[],
  });

  get formValue() {
    return this.optionsForm.value as ITelegramLoadOptions;
  }

  get dateTo() { return (this.optionsForm.get('dateTo')!); }
  get maxLoad() { return this.optionsForm.get('maxLoad')!; }

  quotasCount:number;
  constructor(private router:Router,
    private fb: FormBuilder,
    private route:ActivatedRoute,
    private repliseService:TelegramRepliesService,
    private toaster:MyToastrService){}
  ngOnInit(): void {
    this.animationState='in';

    this.messageId=this.route.snapshot.params['messageId'];

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
    
    if(this.messageId!=null){
      options.parentId=this.messageId;
    }

    if(options.maxLoad<=0){
      this.toaster.error($localize`Кількість завантажень дорівнює нулю`);
      return;
    }

    this.repliseService.loadFromMessage(options).subscribe(res=>{
      this.toaster.success($localize`Задачу створено`);
      this.isLoading=false;
      this.closeOverflow();
    },error=>{
      this.isLoading=false;
      this.toaster.error(error.error.Message);
    });
  }
}
