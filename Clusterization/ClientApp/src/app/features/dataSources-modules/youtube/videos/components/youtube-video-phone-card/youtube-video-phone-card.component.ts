import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ISimpleYoutubeVideo } from '../../models/responses/simple-youtube-video';
import {Clipboard} from '@angular/cdk/clipboard';

@Component({
  selector: 'app-youtube-video-phone-card',
  templateUrl: './youtube-video-phone-card.component.html',
  styleUrl: './youtube-video-phone-card.component.scss'
})
export class YoutubeVideoPhoneCardComponent {
  @Input() video:ISimpleYoutubeVideo;
  @Output() selectVideoEvent = new EventEmitter<ISimpleYoutubeVideo>();
  @Output() unselectVideoEvent = new EventEmitter<ISimpleYoutubeVideo>();
  
  @Input() isSelectOnlyLoaded=false;
  constructor(private clipboard: Clipboard,
    private toastr:ToastrService,
    private router:Router) {}

  copyToClipboard(text: string, event:MouseEvent) {
    event.stopPropagation();

    this.clipboard.copy(text);

    this.toastr.success($localize`Скопійовано!!!`);
  }

  openFull(){
    if (!this.video.isLoaded) return;

    this.router.navigateByUrl('youtube-layout/dataSources/youtube/videos/full/'+this.video.id);
  }

  select() {
    if ((this.isSelectOnlyLoaded && !this.video.isLoaded) ||
      (!this.isSelectOnlyLoaded && this.video.isLoaded)) return;

    this.video.isSelected = true;
    this.selectVideoEvent.emit(this.video);
  }
  unselect() {
    if ((this.isSelectOnlyLoaded && !this.video.isLoaded) ||
      (!this.isSelectOnlyLoaded && this.video.isLoaded)) return;

    this.video.isSelected = false;
    this.unselectVideoEvent.emit(this.video);
  }
}
