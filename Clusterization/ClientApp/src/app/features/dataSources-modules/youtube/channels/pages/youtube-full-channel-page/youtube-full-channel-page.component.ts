import { Component, OnInit } from '@angular/core';
import { YoutubeChannelService } from '../../services/youtube-channel.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { Clipboard } from '@angular/cdk/clipboard';
import { ISelectAction } from 'src/app/core/models/select-action';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { ISimpleYoutubeChannel } from '../../models/responses/simple-youtube-channel';
import { AccountService } from 'src/app/features/shared-module/account/services/account.service';

@Component({
  selector: 'app-youtube-full-channel-page',
  templateUrl: './youtube-full-channel-page.component.html',
  styleUrls: ['./youtube-full-channel-page.component.scss']
})
export class YoutubeFullChannelPageComponent implements OnInit {
  channel: ISimpleYoutubeChannel;

  dateStr:string=$localize`Дату`;
  countStr:string=$localize`Кількість`;

  actions: ISelectAction[] = [
    {
      name: $localize`Завантажити багато відео`,
      action: () => {
        this.router.navigate([{ outlets: { overflow: 'dataSources/youtube/videos/load-many-by-channel/' + this.channel.id } }]);
      },
      isForAuthorized: true,
      isOnlyForUsers:true
    },
    {
      name: $localize`Завантажити багато коментарів`,
      action: () => {
        this.router.navigate([{ outlets: { overflow: 'dataSources/youtube/comments/load-by-channel/' + this.channel.id } }]);
      },
      isForAuthorized: true,
      isOnlyForUsers:true
    },
    {
      name: $localize`Додати коментарі до робочого простору`,
      action: () => {
        let workspaceId = this.storageService.getSelectedWorkspace();

        if (workspaceId == null) {
          this.toastr.error($localize`Робочий простір не вибрано`);
          return;
        }
        this.router.navigate([{ outlets: { overflow: 'dataSources/youtube/add-data-objects/add-comments-by-channel/' + this.channel.id } }]);
      },
      isForAuthorized: true,
      isOnlyForUsers:true
    },
    {
      name: $localize`Додати коментарі у відео до робочого простору`,
      action: () => {
        let workspaceId = this.storageService.getSelectedWorkspace();

        if (workspaceId == null) {
          this.toastr.error($localize`Робочий простір не вибрано`);
          return;
        }
        this.router.navigate([{ outlets: { overflow: 'dataSources/youtube/add-data-objects/add-comments-by-videos/' + this.channel.id } }]);
      },
      isForAuthorized: true,
      isOnlyForUsers:true
    }
  ]


  isLoading: boolean;
  constructor(private channelService: YoutubeChannelService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: MyToastrService,
    private clipboard: Clipboard,
    private storageService: MyLocalStorageService,
    private accountService: AccountService) { }
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

    this.toastr.success(msg + ' ' + $localize`скопійовано!!!`);
  }

  visitorError = $localize`Недостатній рівень доступу. Для цієї дії необхідно підтвердити email!`;
  openFindNewVideos(event: MouseEvent) {
    if (!this.accountService.isAuthenticated()) {
      this.toastr.error($localize`Ви не авторизовані!`);
      return;
    }
    if (!this.accountService.isUserUser()) {
      this.toastr.error(this.visitorError);
      return;
    }
  }
}
