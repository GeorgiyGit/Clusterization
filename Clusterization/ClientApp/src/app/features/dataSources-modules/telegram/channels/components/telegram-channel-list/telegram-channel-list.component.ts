import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ISimpleTelegramChannel } from '../../models/responses/simple-telegram-channel';

@Component({
  selector: 'app-telegram-channel-list',
  templateUrl: './telegram-channel-list.component.html',
  styleUrl: './telegram-channel-list.component.scss'
})
export class TelegramChannelListComponent {
  @Input() channels:ISimpleTelegramChannel[]=[];
  @Output() selectChannelEvent=new EventEmitter<ISimpleTelegramChannel>();
  @Output() unselectChannelEvent=new EventEmitter<ISimpleTelegramChannel>();

  toggleSelection(value:boolean){
    this.channels.forEach(elem=>{
      elem.isSelectAvailable=value;
    });
  }

  selectChannel(channel:ISimpleTelegramChannel){
    this.selectChannelEvent.emit(channel);
  }
  unselectChannel(channel:ISimpleTelegramChannel){
    this.unselectChannelEvent.emit(channel);
  }
}
