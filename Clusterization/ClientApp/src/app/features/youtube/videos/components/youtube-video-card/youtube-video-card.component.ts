import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ISimpleChannel } from '../../../channels/models/simple-channel';
import { ISimpleVideo } from '../../models/simple-video';

import {Clipboard} from '@angular/cdk/clipboard';

@Component({
  selector: 'app-youtube-video-card',
  templateUrl: './youtube-video-card.component.html',
  styleUrls: ['./youtube-video-card.component.scss']
})
export class YoutubeVideoCardComponent {
  @Input() video:ISimpleVideo;
  @Output() selectVideoEvent = new EventEmitter<ISimpleVideo>();
  @Output() unselectVideoEvent = new EventEmitter<ISimpleVideo>();
  
  @Input() isSelectOnlyLoaded=false;
  constructor(private clipboard: Clipboard,
    private toastr:ToastrService,
    private router:Router) {}

  copyToClipboard(text: string, event:MouseEvent) {
    event.stopPropagation();

    this.clipboard.copy(text);

    this.toastr.success('Скопійовано!!!');
  }

  openFull(){
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
