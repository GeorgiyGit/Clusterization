import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { YoutubeChannelService } from '../../../channels/services/youtube-channel.service';
import { YoutubeVideoService } from '../../services/youtube-video.service';

@Component({
  selector: 'app-youtube-load-one-video',
  templateUrl: './youtube-load-one-video.component.html',
  styleUrls: ['./youtube-load-one-video.component.scss']
})
export class YoutubeLoadOneVideoComponent {
  value: string = '';

  constructor(private toastrService: MyToastrService,
    private videoService: YoutubeVideoService,
    private router: Router) { }
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
    this.videoService.loadById(this.value).subscribe(res => {
      this.isLoading = false;
      this.toastrService.success($localize`Відео завантажено`);
      this.router.navigate([{ outlets: { overflow: null } }]);
    }, error => {
      this.isLoading = false;
      this.toastrService.error(error.error.Message);
    });
  }
}
