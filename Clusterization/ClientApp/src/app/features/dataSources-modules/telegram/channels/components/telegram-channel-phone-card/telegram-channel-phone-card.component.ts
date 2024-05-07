import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ISimpleTelegramChannel } from '../../models/responses/simple-telegram-channel';
import { Clipboard } from '@angular/cdk/clipboard';

@Component({
  selector: 'app-telegram-channel-phone-card',
  templateUrl: './telegram-channel-phone-card.component.html',
  styleUrl: './telegram-channel-phone-card.component.scss'
})
export class TelegramChannelPhoneCardComponent {
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

  select() {
    if ((this.isSelectOnlyLoaded && !this.channel.isLoaded) ||
      (!this.isSelectOnlyLoaded && this.channel.isLoaded)) return;

    this.channel.isSelected = true;
    this.selectChannelEvent.emit(this.channel);
  }
  unselect() {
    if ((this.isSelectOnlyLoaded && !this.channel.isLoaded) ||
      (!this.isSelectOnlyLoaded && this.channel.isLoaded)) return;

    this.channel.isSelected = false;
    this.unselectChannelEvent.emit(this.channel);
  }
}
