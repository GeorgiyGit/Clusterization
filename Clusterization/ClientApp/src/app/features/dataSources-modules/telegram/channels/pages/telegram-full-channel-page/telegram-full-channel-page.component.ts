import { TelegramChannelsService } from './../../services/telegram-channels.service';
import { Component, OnInit } from '@angular/core';
import { IFullTelegramChannel } from '../../models/responses/full-telegram-channel';
import { ISelectAction } from 'src/app/core/models/select-action';
import { ActivatedRoute, Router } from '@angular/router';
import { MyLocalStorageService } from 'src/app/core/services/my-local-storage.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { AccountService } from 'src/app/features/shared-module/account/services/account.service';
import { Clipboard } from '@angular/cdk/clipboard';

@Component({
  selector: 'app-telegram-full-channel-page',
  templateUrl: './telegram-full-channel-page.component.html',
  styleUrl: './telegram-full-channel-page.component.scss'
})
export class TelegramFullChannelPageComponent implements OnInit {
  channel: IFullTelegramChannel;

  dateStr:string=$localize`Дату`;
  countStr:string=$localize`Кількість`;

  actions: ISelectAction[] = [
    {
      name: $localize`Завантажити багато повідомлень`,
      action: () => {
        this.router.navigate([{ outlets: { overflow: 'dataSources/telegram/messages/load-many-by-channel/' + this.channel.id } }]);
      },
      isForAuthorized: true
    },
    {
      name: $localize`Завантажити багато відповідей`,
      action: () => {
        this.router.navigate([{ outlets: { overflow: 'dataSources/telegram/replies/load-by-channel/' + this.channel.id } }]);
      },
      isForAuthorized: true
    },
    {
      name: $localize`Додати відповіді до робочого простору`,
      action: () => {
        let workspaceId = this.storageService.getSelectedWorkspace();

        if (workspaceId == null) {
          this.toastr.error($localize`Робочий простір не вибрано`);
          return;
        }
        this.router.navigate([{ outlets: { overflow: 'dataSources/telegram/add-data-objects/add-comments-by-channel/' + this.channel.id } }]);
      },
      isForAuthorized: true
    },
    {
      name: $localize`Додати відповіді у повідомленях до робочого простору`,
      action: () => {
        let workspaceId = this.storageService.getSelectedWorkspace();

        if (workspaceId == null) {
          this.toastr.error($localize`Робочий простір не вибрано`);
          return;
        }
        this.router.navigate([{ outlets: { overflow: 'dataSources/telegram/add-data-objects/add-comments-by-videos/' + this.channel.id } }]);
      },
      isForAuthorized: true
    }
  ]


  isLoading: boolean;
  constructor(private channelService: TelegramChannelsService,
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

  openFindNewVideos(event: MouseEvent) {
    if (!this.accountService.isAuthenticated()) {
      this.toastr.error($localize`Ви не авторизовані!`);
    }
  }
}
