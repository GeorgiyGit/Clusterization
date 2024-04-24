import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ISimpleVideo } from '../../models/responses/simple-video';

@Component({
  selector: 'app-youtube-video-list',
  templateUrl: './youtube-video-list.component.html',
  styleUrls: ['./youtube-video-list.component.scss']
})
export class YoutubeVideoListComponent {
  @Input() videos:ISimpleVideo[]=[];
  @Input() isSelectOnlyLoaded:boolean;
  @Output() selectVideoEvent=new EventEmitter<ISimpleVideo>();
  @Output() unselectVideoEvent=new EventEmitter<ISimpleVideo>();

  toggleSelection(value:boolean){
    this.videos.forEach(elem=>{
      elem.isSelectAvailable=value;
    });
  }

  selectVideo(video:ISimpleVideo){
    this.selectVideoEvent.emit(video);
  }
  unselectVideo(video:ISimpleVideo){
    this.unselectVideoEvent.emit(video);
  }
}
