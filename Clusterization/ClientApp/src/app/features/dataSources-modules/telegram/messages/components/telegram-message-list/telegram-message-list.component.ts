import { Component, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import { ISimpleTelegramMessage } from '../../models/responses/simple-telegram-message';

@Component({
  selector: 'app-telegram-message-list',
  templateUrl: './telegram-message-list.component.html',
  styleUrl: './telegram-message-list.component.scss'
})
export class TelegramMessageListComponent implements OnInit{
  @Input() messages:ISimpleTelegramMessage[]=[];
  @Input() isSelectOnlyLoaded:boolean;
  @Output() selectMessageEvent=new EventEmitter<ISimpleTelegramMessage>();
  @Output() unselectMessageEvent=new EventEmitter<ISimpleTelegramMessage>();

  ngOnInit(): void {
    this.phoneCalculate(window.innerWidth);
  }

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

  @HostListener('window:resize', ['$event'])
  onResize(event:any) {
    this.phoneCalculate(window.innerWidth);
  }

  isPhone:boolean;
  phoneCalculate(width: number) {
    if (width < 750) 
    {
      this.isPhone = true;
    }
    else 
    {
      this.isPhone=false;
    }
  }
}
