import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ISimpleYoutubeChannel } from '../../models/responses/simple-youtube-channel';
import { Clipboard } from '@angular/cdk/clipboard';

@Component({
  selector: 'app-youtube-channel-phone-card',
  templateUrl: './youtube-channel-phone-card.component.html',
  styleUrl: './youtube-channel-phone-card.component.scss'
})
export class YoutubeChannelPhoneCardComponent {
  @Input() channel: ISimpleYoutubeChannel
  @Output() selectChannelEvent = new EventEmitter<ISimpleYoutubeChannel>();
  @Output() unselectChannelEvent = new EventEmitter<ISimpleYoutubeChannel>();

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

    this.router.navigateByUrl('youtube-layout/dataSources/youtube/channels/full/' + this.channel.id + '/list/' + this.channel.id);
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
