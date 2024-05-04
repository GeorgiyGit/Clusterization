import { Component, OnInit } from '@angular/core';
import { ISelectAction } from 'src/app/core/models/select-action';
import { TelegramMessagesService } from '../../services/telegram-messages.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IFullTelegramMessage } from '../../models/responses/full-telegram-message';
import { Clipboard } from '@angular/cdk/clipboard';

@Component({
  selector: 'app-telegram-full-message-page',
  templateUrl: './telegram-full-message-page.component.html',
  styleUrl: './telegram-full-message-page.component.scss'
})
export class TelegramFullMessagePageComponent implements OnInit {
  message: IFullTelegramMessage;

  dateStr: string = $localize`Дату`;
  countStr: string = $localize`Кількість`;

  actions: ISelectAction[] = [
    {
      name: $localize`Завантажити відповіді`,
      action: () => {
        this.router.navigate([{ outlets: { overflow: 'dataSources/telegram/replies/load-by-message/' + this.message.id } }]);
      },
      isForAuthorized: true
    }
  ]

  isLoading: boolean;
  constructor(private messagesService: TelegramMessagesService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: MyToastrService,
    private clipboard: Clipboard) { }
  ngOnInit(): void {
    let id = this.route.snapshot.params['id'];

    this.isLoading = true;
    this.messagesService.getById(id).subscribe(res => {
      this.message = res;

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
  copyToClipboardWithoutDescription(text: string, event:MouseEvent) {
    event.stopPropagation();

    this.clipboard.copy(text);

    this.toastr.success($localize`Скопійовано!!!`);
  }

  cutString(str: string, length: number): string {
    return str.substring(0, Math.min(length, str.length));
  }
}
