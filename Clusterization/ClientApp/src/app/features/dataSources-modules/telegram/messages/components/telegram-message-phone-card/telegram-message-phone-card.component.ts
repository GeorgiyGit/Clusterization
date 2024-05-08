import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ISimpleTelegramMessage } from '../../models/responses/simple-telegram-message';
import {Clipboard} from '@angular/cdk/clipboard';

@Component({
  selector: 'app-telegram-message-phone-card',
  templateUrl: './telegram-message-phone-card.component.html',
  styleUrl: './telegram-message-phone-card.component.scss'
})
export class TelegramMessagePhoneCardComponent {
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

  select() {
    if ((this.isSelectOnlyLoaded && !this.message.isLoaded) ||
      (!this.isSelectOnlyLoaded && this.message.isLoaded)) return;

    this.message.isSelected = true;
    this.selectMessageEvent.emit(this.message);
  }
  unselect() {
    if ((this.isSelectOnlyLoaded && !this.message.isLoaded) ||
      (!this.isSelectOnlyLoaded && this.message.isLoaded)) return;

    this.message.isSelected = false;
    this.unselectMessageEvent.emit(this.message);
  }

  cutString(str: string, length: number): string {
    return str.substring(0, Math.min(length, str.length));
  }
}
