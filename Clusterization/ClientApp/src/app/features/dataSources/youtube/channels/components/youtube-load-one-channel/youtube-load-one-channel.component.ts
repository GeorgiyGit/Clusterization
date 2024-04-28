import { Component } from '@angular/core';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { YoutubeChannelService } from '../../services/youtube-channel.service';
import { Router } from '@angular/router';
import { QuotasCalculationList } from 'src/app/features/quotas/static/quotas-calculation-list';

@Component({
  selector: 'app-youtube-load-one-channel',
  templateUrl: './youtube-load-one-channel.component.html',
  styleUrls: ['./youtube-load-one-channel.component.scss']
})
export class YoutubeLoadOneChannelComponent {
  value: string = '';

  quotasCount:number;
  constructor(private toastrService: MyToastrService,
    private channelService: YoutubeChannelService,
    private router: Router) {
      this.quotasCount=QuotasCalculationList.youtubeChannel;
    }
  changeValue(event: any) {
    this.value = event.target.value;
  }

  isLoading: boolean = false;
  load() {
    if (this.value == null || this.value == '') {
      this.toastrService.error($localize`Поле для Id пусте!!!`);
      return;
    }

    this.isLoading = true;
    this.channelService.loadById(this.value).subscribe(res => {
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
