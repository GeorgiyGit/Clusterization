import { Component, ViewChild } from '@angular/core';
import { ISimpleTelegramChannel } from '../../models/responses/simple-telegram-channel';
import { TelegramChannelListComponent } from '../telegram-channel-list/telegram-channel-list.component';
import { IOptionForSelectInput } from 'src/app/core/models/option-for-select-input';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { TelegramChannelsService } from '../../services/telegram-channels.service';
import { Router } from '@angular/router';
import { QuotasCalculationList } from 'src/app/features/shared-module/quotas/static/quotas-calculation-list';

@Component({
  selector: 'app-telegram-load-multiple-channels',
  templateUrl: './telegram-load-multiple-channels.component.html',
  styleUrl: './telegram-load-multiple-channels.component.scss'
})
export class TelegramLoadMultipleChannelsComponent {
  value: string = '';

  channels: ISimpleTelegramChannel[] = [];
  selectedChannels: ISimpleTelegramChannel[] = [];

  quotasCount:number;

  constructor(private toastrService: MyToastrService,
    private channelService: TelegramChannelsService,
    private router: Router) { 
      this.quotasCount=QuotasCalculationList.telegramChannel;
    }


  @ViewChild(TelegramChannelListComponent) filterChild: TelegramChannelListComponent;
  changeValue(event: any) {
    this.value = event.target.value;
  }

  isLoading: boolean = false;
  loadFirst() {
    if (this.value == null || this.value == '') {
      this.toastrService.error($localize`Поле для назви пусте!!!`);
      return;
    }

    this.isLoading = true;
    this.channelService.getWithoutLoading(this.value).subscribe(res => {
      this.selectedChannels=[];
      
      res.forEach(elem=>{
        elem.isSelectAvailable=true;
      });

      this.channels = res;
      this.isLoading = false;
    },
      error => {
        this.isLoading = false;
        this.toastrService.error(error.error.Message);
      });
  }
  
  selectChannel(channel: ISimpleTelegramChannel) {
    if (this.selectedChannels.find(c => c == channel) == null) this.selectedChannels.push(channel);
  }
  unselectChannel(channel: ISimpleTelegramChannel) {
    this.selectedChannels = this.selectedChannels.filter(c => c != channel);
  }

  load() {
    if (this.selectedChannels == null || this.selectedChannels.length == 0) {
      this.toastrService.error($localize`Каналів не вибрано`);
      return;
    }

    let usernames: string[] = [];
    this.selectedChannels.forEach(elem => {
      usernames.push(elem.username);
    });

    this.isLoading = true;
    this.channelService.loadManyByUsernames(usernames).subscribe(res => {
      this.channels = [];
      this.selectedChannels = [];
      this.value = '';

      this.toastrService.success($localize`Канали завантажено`);
      this.isLoading = false;
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, error => {
      this.isLoading = false;
      this.toastrService.error(error.error.Message);
    });
  }
}
