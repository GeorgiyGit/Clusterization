import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ISimpleTelegramMessage } from '../../models/responses/simple-telegram-message';
import {Clipboard} from '@angular/cdk/clipboard';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-telegram-message-card',
  templateUrl: './telegram-message-card.component.html',
  styleUrl: './telegram-message-card.component.scss'
})
export class TelegramMessageCardComponent {
  @Input() message:ISimpleTelegramMessage;
  @Output() selectMessageEvent = new EventEmitter<ISimpleTelegramMessage>();
  @Output() unselectMessageEvent = new EventEmitter<ISimpleTelegramMessage>();
  
  @Input() isSelectOnlyLoaded=false;
  constructor(private clipboard: Clipboard,
    private toastr:ToastrService,
    private router:Router) {}

  copyToClipboard(text: string, event:MouseEvent) {
    event.stopPropagation();

    this.clipboard.copy(text);

    this.toastr.success($localize`Скопійовано!!!`);
  }

  openFull(){
    if (!this.message.isLoaded) return;

    this.router.navigateByUrl('telegram-layout/dataSources/telegram/messages/full/'+this.message.id);
  }

  toggleSelection(){
    if((this.isSelectOnlyLoaded && !this.message.isLoaded) ||
      (!this.isSelectOnlyLoaded && this.message.isLoaded))return;

    this.message.isSelected=!this.message.isSelected;
  
    if(this.message.isSelected==true){
      this.selectMessageEvent.emit(this.message);
    }
    else this.unselectMessageEvent.emit(this.message);
  }

  cutString(str: string, length: number): string {
    return str.substring(0, Math.min(length, str.length));
  }
}
