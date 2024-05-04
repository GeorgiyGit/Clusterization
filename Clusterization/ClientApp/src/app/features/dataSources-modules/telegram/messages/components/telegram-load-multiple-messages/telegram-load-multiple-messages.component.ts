import { Component, OnInit, ViewChild } from '@angular/core';
import { ISimpleTelegramMessage } from '../../models/responses/simple-telegram-message';
import { TelegramMessagesService } from '../../services/telegram-messages.service';
import { TelegramMessageListComponent } from '../telegram-message-list/telegram-message-list.component';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { Router, ActivatedRoute } from '@angular/router';
import { QuotasCalculationList } from 'src/app/features/shared-module/quotas/static/quotas-calculation-list';
import { IGetTelegramMessagesRequest } from '../../models/requests/get-telegram-messages-request';
import { IPageParameters } from 'src/app/core/models/page-parameters';

@Component({
  selector: 'app-telegram-load-multiple-messages',
  templateUrl: './telegram-load-multiple-messages.component.html',
  styleUrl: './telegram-load-multiple-messages.component.scss'
})
export class TelegramLoadMultipleMessagesComponent implements OnInit {
  value: string = '';

  channelId: number;

  messages: ISimpleTelegramMessage[] = [];
  selectedMessages: ISimpleTelegramMessage[] = [];

  pageParameters:IPageParameters={
    pageNumber:1,
    pageSize:20
  }

  quotasCount: number;
  constructor(private toastrService: MyToastrService,
    private messagesService: TelegramMessagesService,
    private router: Router,
    private route: ActivatedRoute) { }
  ngOnInit(): void {
    this.channelId = this.route.snapshot.params['id'];

    this.quotasCount = QuotasCalculationList.telegramMessage;

    if (this.channelId != null) this.loadFirst();
  }


  @ViewChild(TelegramMessageListComponent) filterChild: TelegramMessageListComponent;
  changeValue(event: any) {
    this.value = event.target.value;
  }

  isLoading: boolean = false;
  loadFirst() {
    if (this.isLoading) return;

    this.pageParameters.pageNumber=1;
    this.isLoading = true;

    let request:IGetTelegramMessagesRequest={
      filterStr:'',
      channelId:this.channelId,
      filterType:'',
      pageParameters:this.pageParameters
    }
    this.messagesService.getWithoutLoading(request).subscribe(res => {
      this.selectedMessages = [];

      res.forEach(elem => {
        elem.isSelectAvailable = true;
      });

      this.messages = res;
      this.isLoading = false;
    },
      error => {
        this.isLoading = false;
        this.toastrService.error(error.error.Message);
      });
  }

  isLoading2: boolean;
  loadMore() {
    if (this.isLoading2) return;

    this.pageParameters.pageNumber++;
    this.isLoading2 = true;

    let request:IGetTelegramMessagesRequest={
      filterStr:'',
      channelId:this.channelId,
      filterType:'',
      pageParameters:this.pageParameters
    }
    this.messagesService.getWithoutLoading(request).subscribe(res => {
      res.forEach(elem => {
        elem.isSelectAvailable = true;
      });

      this.messages = this.messages.concat(res);
      this.isLoading2 = false;
    },
      error => {
        this.isLoading2 = false;
        this.toastrService.error(error.error.Message);
      });
  }

  selectMessage(msg: ISimpleTelegramMessage) {
    if (this.selectedMessages.find(e => e == msg) == null) this.selectedMessages.push(msg);
  }
  unselectMessage(msg: ISimpleTelegramMessage) {
    this.selectedMessages = this.selectedMessages.filter(e => e != msg);
  }

  load() {
    if (this.selectedMessages == null || this.selectedMessages.length == 0) {
      this.toastrService.error($localize`Жодного повідомлення не вибрано`);
      return;
    }

    let ids: number[] = [];
    this.selectedMessages.forEach(elem => {
      ids.push(elem.id);
    });

    this.isLoading = true;
    this.messagesService.loadManyByIds({
      ids:ids,
      channelId:this.channelId
    }).subscribe(res => {
      this.messages = [];
      this.selectedMessages = [];
      this.value = '';

      this.toastrService.success($localize`Всі повідомлення завантажено`);
      this.isLoading = false;
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, error => {
      this.isLoading = false;
      this.toastrService.error(error.error.Message);
    });
  }
}
