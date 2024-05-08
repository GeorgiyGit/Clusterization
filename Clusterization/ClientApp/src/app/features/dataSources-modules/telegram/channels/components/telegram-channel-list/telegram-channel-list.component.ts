import { Component, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import { ISimpleTelegramChannel } from '../../models/responses/simple-telegram-channel';

@Component({
  selector: 'app-telegram-channel-list',
  templateUrl: './telegram-channel-list.component.html',
  styleUrl: './telegram-channel-list.component.scss'
})
export class TelegramChannelListComponent implements OnInit{
  @Input() channels:ISimpleTelegramChannel[]=[];
  @Output() selectChannelEvent=new EventEmitter<ISimpleTelegramChannel>();
  @Output() unselectChannelEvent=new EventEmitter<ISimpleTelegramChannel>();


  ngOnInit(): void {
    this.phoneCalculate(window.innerWidth);
  }

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
