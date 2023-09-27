import { Component, EventEmitter, Input, Output, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { ISimpleChannel } from '../../models/simple-channel';
import { YoutubeChannelCardComponent } from '../youtube-channel-card/youtube-channel-card.component';

@Component({
  selector: 'app-youtube-channel-list',
  templateUrl: './youtube-channel-list.component.html',
  styleUrls: ['./youtube-channel-list.component.scss']
})
export class YoutubeChannelListComponent {
  @Input() channels:ISimpleChannel[]=[];
  @Output() selectChannelEvent=new EventEmitter<ISimpleChannel>();
  @Output() unselectChannelEvent=new EventEmitter<ISimpleChannel>();

  //@ViewChildren(YoutubeChannelCardComponent) childComponents: QueryList<YoutubeChannelCardComponent>;
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
