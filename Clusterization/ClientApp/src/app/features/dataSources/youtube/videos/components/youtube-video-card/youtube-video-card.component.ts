import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import {Clipboard} from '@angular/cdk/clipboard';
import { ISimpleYoutubeVideo } from '../../models/responses/simple-youtube-video';

@Component({
  selector: 'app-youtube-video-card',
  templateUrl: './youtube-video-card.component.html',
  styleUrls: ['./youtube-video-card.component.scss']
})
export class YoutubeVideoCardComponent {
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

    this.router.navigateByUrl('video-full-info/'+this.video.id);
  }

  toggleSelection(){
    if((this.isSelectOnlyLoaded && !this.video.isLoaded) ||
      (!this.isSelectOnlyLoaded && this.video.isLoaded))return;

    this.video.isSelected=!this.video.isSelected;
  
    if(this.video.isSelected==true){
      this.selectVideoEvent.emit(this.video);
    }
    else this.unselectVideoEvent.emit(this.video);
  }
}
