import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ISimpleTelegramMessage } from '../../models/responses/simple-telegram-message';

@Component({
  selector: 'app-telegram-message-list',
  templateUrl: './telegram-message-list.component.html',
  styleUrl: './telegram-message-list.component.scss'
})
export class TelegramMessageListComponent {
  @Input() messages:ISimpleTelegramMessage[]=[];
  @Input() isSelectOnlyLoaded:boolean;
  @Output() selectMessageEvent=new EventEmitter<ISimpleTelegramMessage>();
  @Output() unselectMessageEvent=new EventEmitter<ISimpleTelegramMessage>();

  toggleSelection(value:boolean){
    this.messages.forEach(elem=>{
      elem.isSelectAvailable=value;
    });
  }

  selectMessage(message:ISimpleTelegramMessage){
    this.selectMessageEvent.emit(message);
  }
  unselectMessage(message:ISimpleTelegramMessage){
    this.unselectMessageEvent.emit(message);
  }
}
