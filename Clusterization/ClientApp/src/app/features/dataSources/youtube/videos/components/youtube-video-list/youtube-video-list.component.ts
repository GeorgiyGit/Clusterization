import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ISimpleYoutubeVideo } from '../../models/responses/simple-youtube-video';

@Component({
  selector: 'app-youtube-video-list',
  templateUrl: './youtube-video-list.component.html',
  styleUrls: ['./youtube-video-list.component.scss']
})
export class YoutubeVideoListComponent {
  @Input() videos:ISimpleYoutubeVideo[]=[];
  @Input() isSelectOnlyLoaded:boolean;
  @Output() selectVideoEvent=new EventEmitter<ISimpleYoutubeVideo>();
  @Output() unselectVideoEvent=new EventEmitter<ISimpleYoutubeVideo>();

  toggleSelection(value:boolean){
    this.videos.forEach(elem=>{
      elem.isSelectAvailable=value;
    });
  }

  selectVideo(video:ISimpleYoutubeVideo){
    this.selectVideoEvent.emit(video);
  }
  unselectVideo(video:ISimpleYoutubeVideo){
    this.unselectVideoEvent.emit(video);
  }
}
