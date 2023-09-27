import { Component, EventEmitter, Input, Output } from '@angular/core';

import {Clipboard} from '@angular/cdk/clipboard';
import { ToastrService } from 'ngx-toastr';
import { ISimpleChannel } from '../../models/simple-channel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-youtube-channel-card',
  templateUrl: './youtube-channel-card.component.html',
  styleUrls: ['./youtube-channel-card.component.scss']
})
export class YoutubeChannelCardComponent {
  @Input() channel:ISimpleChannel
  @Output() selectChannelEvent = new EventEmitter<ISimpleChannel>();
  @Output() unselectChannelEvent = new EventEmitter<ISimpleChannel>();
  
  @Input() isSelectOnlyLoaded=false;
  constructor(private clipboard: Clipboard,
    private toastr:ToastrService,
    private router:Router) {}

  copyToClipboard(text: string, event:MouseEvent) {
    event.stopPropagation();

    this.clipboard.copy(text);

    this.toastr.success('Скопійовано!!!');
  }

  openFull(){
    this.router.navigateByUrl('channel-full-info/'+this.channel.id+'/list/'+this.channel.id);
  }

  toggleSelection(){
    if((this.isSelectOnlyLoaded && !this.channel.isLoaded) ||
      (!this.isSelectOnlyLoaded && this.channel.isLoaded))return;

    this.channel.isSelected=!this.channel.isSelected;
  
    if(this.channel.isSelected==true){
      this.selectChannelEvent.emit(this.channel);
    }
    else this.unselectChannelEvent.emit(this.channel);
  }
}
