import { Component, OnInit } from '@angular/core';
import { ITelegramMessagesLoadOptions } from '../../models/telegram-messages-load-options';
import { FormBuilder, FormGroup } from '@angular/forms';
import { TelegramMessagesService } from '../../services/telegram-messages.service';
import { QuotasCalculationList } from 'src/app/features/shared-module/quotas/static/quotas-calculation-list';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { Router, ActivatedRoute } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { ITelegramLoadOptions } from '../../models/telegram-load-optionts';

@Component({
  selector: 'app-telegram-load-group-messages-page',
  templateUrl: './telegram-load-group-messages-page.component.html',
  styleUrl: './telegram-load-group-messages-page.component.scss',
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
export class TelegramLoadGroupMessagesPageComponent implements OnInit {
  animationState: string = 'in';
  channelId: number | undefined;

  optionsForm: FormGroup = this.fb.group({
    dateFrom: [null],
    dateTo: [null],
    maxLoad: [],
    minCommentCount: [],
    minViewCount: []
  });

  get formValue() {
    return this.optionsForm.value as ITelegramLoadOptions;
  }

  get dateTo() { return this.optionsForm.get('dateTo')!; }
  get maxLoad() { return this.optionsForm.get('maxLoad')!; }

  quotasCount: number;
  constructor(private router: Router,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private messagesService: TelegramMessagesService,
    private toaster: MyToastrService) { }
  ngOnInit(): void {
    this.animationState = 'in';

    this.channelId = this.route.snapshot.params['channelId'];
    this.quotasCount = QuotasCalculationList.telegramMessage;
  }

  closeOverflow() {
    this.animationState = 'hidden';
    setTimeout(() => {
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, 300);
  }

  isLoading: boolean;
  load() {
    let options = this.formValue;

    if (this.channelId != null) {
      options.parentId = this.channelId;
    }

    if (options.maxLoad <= 0) {
      this.toaster.error($localize`Кількість завантажень дорівнює нулю`);
      return;
    }

    this.messagesService.loadByChannel(options).subscribe(res => {
      this.toaster.success($localize`Задачу створено`);
      this.isLoading = false;
      this.closeOverflow();
    }, error => {
      this.isLoading = false;
      this.toaster.error(error.error.Message);
    });
  }
}
