import { Component, OnInit } from '@angular/core';
import { ISimpleChannel } from '../../models/simple-channel';
import { YoutubeChannelService } from '../../services/youtube-channel.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { Clipboard } from '@angular/cdk/clipboard';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ISelectAction } from 'src/app/core/models/select-action';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';

@Component({
  selector: 'app-youtube-full-channel-page',
  templateUrl: './youtube-full-channel-page.component.html',
  styleUrls: ['./youtube-full-channel-page.component.scss']
})
export class YoutubeFullChannelPageComponent implements OnInit {
  channel: ISimpleChannel;

  actions: ISelectAction[] = [
    {
      name: 'Завантажити багато відео',
      action: () => {
        this.router.navigate([{ outlets: { overflow: 'load-videos-by-channel/' + this.channel.id } }]);
      }
    },
    {
      name: 'Завантажити багато коментарів',
      action: () => {
        this.router.navigate([{ outlets: { overflow: 'load-comments-by-channel/' + this.channel.id } }]);
      }
    },
    {
      name: 'Додати коментарі до робочого простору',
      action: () => {
        let workspaceId = this.storageService.getSelectedWorkspace();

        if (workspaceId == null) {
          this.toastr.error('Робочий простір не вибрано');
          return;
        }
        this.router.navigateByUrl('workspaces/add-comments-by-channel/'+this.channel.id);
      }
    },
  ]


  isLoading: boolean;
  constructor(private channelService: YoutubeChannelService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: MyToastrService,
    private clipboard: Clipboard,
    private storageService: MyLocalStorageService) { }
  ngOnInit(): void {
    let id = this.route.snapshot.params['id'];

    this.isLoading = true;
    this.channelService.getById(id).subscribe(res => {
      this.channel = res;

      this.channel.channelImageUrl = this.channel.channelImageUrl.replace('s88', 's240');

      this.isLoading = false;
    }, error => {
      this.isLoading = false;
      this.toastr.error(error.error.Message);
    });
  }

  copyToClipboard(msg: string, text: string, event: MouseEvent) {
    event.stopPropagation();

    this.clipboard.copy(text);

    this.toastr.success(msg + ' ' + 'скопійовано!!!');

  }
}
