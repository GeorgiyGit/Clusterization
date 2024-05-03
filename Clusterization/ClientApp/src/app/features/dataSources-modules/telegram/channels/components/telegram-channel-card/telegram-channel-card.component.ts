import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Clipboard } from '@angular/cdk/clipboard';
import { ISimpleTelegramChannel } from '../../models/responses/simple-telegram-channel';

@Component({
    selector: 'app-telegram-channel-card',
    templateUrl: './telegram-channel-card.component.html',
    styleUrl: './telegram-channel-card.component.scss',
})
export class TelegramChannelCardComponent {
  @Input() channel: ISimpleTelegramChannel
  @Output() selectChannelEvent = new EventEmitter<ISimpleTelegramChannel>();
  @Output() unselectChannelEvent = new EventEmitter<ISimpleTelegramChannel>();

  @Input() isSelectOnlyLoaded = false;
  constructor(private clipboard: Clipboard,
    private toastr: ToastrService,
    private router: Router) { }

  copyToClipboard(text: string, event: MouseEvent) {
    event.stopPropagation();

    this.clipboard.copy(text);

    this.toastr.success($localize`Скопійовано!!!`);
  }

  openFull() {
    if (!this.channel.isLoaded) return;

    this.router.navigateByUrl('telegram-layout/dataSources/telegram/channels/full/' + this.channel.id + '/list/' + this.channel.id);
  }

  toggleSelection() {
    if ((this.isSelectOnlyLoaded && !this.channel.isLoaded) ||
      (!this.isSelectOnlyLoaded && this.channel.isLoaded)) return;

    this.channel.isSelected = !this.channel.isSelected;

    if (this.channel.isSelected == true) {
      this.selectChannelEvent.emit(this.channel);
    }
    else this.unselectChannelEvent.emit(this.channel);
  }
}
