import { Component } from '@angular/core';
import { TelegramChannelsService } from '../../services/telegram-channels.service';
import { Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { QuotasCalculationList } from 'src/app/features/shared-module/quotas/static/quotas-calculation-list';

@Component({
  selector: 'app-telegram-load-one-channel',
  templateUrl: './telegram-load-one-channel.component.html',
  styleUrl: './telegram-load-one-channel.component.scss'
})
export class TelegramLoadOneChannelComponent {
  value: string = '';

  quotasCount: number;
  constructor(private toastrService: MyToastrService,
    private channelService: TelegramChannelsService,
    private router: Router) {
    this.quotasCount = QuotasCalculationList.telegramChannel;
  }
  changeValue(event: any) {
    this.value = event.target.value;
  }

  isLoading: boolean = false;
  load() {
    if (this.value == null || this.value == '') {
      this.toastrService.error($localize`Поле для Username пусте!!!`);
      return;
    }
    this.value = this.value.replace('@','');

    this.isLoading = true;
    this.channelService.loadByUsername(this.value).subscribe(res => {
      this.isLoading = false;
      this.toastrService.success($localize`Канал завантажено`);
      this.router.navigate([{ outlets: { overflow: null } }]);
    },
      error => {
        this.isLoading = false;
        this.toastrService.error(error.error.Message);
      });
  }
}
