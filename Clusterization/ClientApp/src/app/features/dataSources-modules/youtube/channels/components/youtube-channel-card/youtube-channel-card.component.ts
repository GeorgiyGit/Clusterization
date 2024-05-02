import { Component, EventEmitter, Input, Output } from '@angular/core';

import { Clipboard } from '@angular/cdk/clipboard';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { ISimpleYoutubeChannel } from '../../models/responses/simple-youtube-channel';

@Component({
  selector: 'app-youtube-channel-card',
  templateUrl: './youtube-channel-card.component.html',
  styleUrls: ['./youtube-channel-card.component.scss']
})
export class YoutubeChannelCardComponent {
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
