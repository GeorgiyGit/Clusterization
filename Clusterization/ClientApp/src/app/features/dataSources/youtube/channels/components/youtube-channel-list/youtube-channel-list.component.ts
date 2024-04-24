import { Component, EventEmitter, Input, Output, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { ISimpleChannel } from '../../models/responses/simple-channel';

@Component({
  selector: 'app-youtube-channel-list',
  templateUrl: './youtube-channel-list.component.html',
  styleUrls: ['./youtube-channel-list.component.scss']
})
export class YoutubeChannelListComponent {
  @Input() channels:ISimpleChannel[]=[];
  @Output() selectChannelEvent=new EventEmitter<ISimpleChannel>();
  @Output() unselectChannelEvent=new EventEmitter<ISimpleChannel>();

  toggleSelection(value:boolean){
    this.channels.forEach(elem=>{
      elem.isSelectAvailable=value;
    });
  }

  selectChannel(channel:ISimpleChannel){
    this.selectChannelEvent.emit(channel);
  }
  unselectChannel(channel:ISimpleChannel){
    this.unselectChannelEvent.emit(channel);
  }
}
