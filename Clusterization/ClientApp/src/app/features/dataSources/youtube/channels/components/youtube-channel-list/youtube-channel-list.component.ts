import { Component, EventEmitter, Input, Output} from '@angular/core';
import { ISimpleYoutubeChannel } from '../../models/responses/simple-youtube-channel';

@Component({
  selector: 'app-youtube-channel-list',
  templateUrl: './youtube-channel-list.component.html',
  styleUrls: ['./youtube-channel-list.component.scss']
})
export class YoutubeChannelListComponent {
  @Input() channels:ISimpleYoutubeChannel[]=[];
  @Output() selectChannelEvent=new EventEmitter<ISimpleYoutubeChannel>();
  @Output() unselectChannelEvent=new EventEmitter<ISimpleYoutubeChannel>();

  toggleSelection(value:boolean){
    this.channels.forEach(elem=>{
      elem.isSelectAvailable=value;
    });
  }

  selectChannel(channel:ISimpleYoutubeChannel){
    this.selectChannelEvent.emit(channel);
  }
  unselectChannel(channel:ISimpleYoutubeChannel){
    this.unselectChannelEvent.emit(channel);
  }
}
