import { Component, EventEmitter, HostListener, Input, OnInit, Output} from '@angular/core';
import { ISimpleYoutubeChannel } from '../../models/responses/simple-youtube-channel';

@Component({
  selector: 'app-youtube-channel-list',
  templateUrl: './youtube-channel-list.component.html',
  styleUrls: ['./youtube-channel-list.component.scss']
})
export class YoutubeChannelListComponent implements OnInit{
  @Input() channels:ISimpleYoutubeChannel[]=[];
  @Output() selectChannelEvent=new EventEmitter<ISimpleYoutubeChannel>();
  @Output() unselectChannelEvent=new EventEmitter<ISimpleYoutubeChannel>();

  ngOnInit(): void {
    this.phoneCalculate(window.innerWidth);
  }

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
