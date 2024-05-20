import { Component, Input } from '@angular/core';
import { ITelegramReply } from 'src/app/features/dataSources-modules/telegram/replies/models/telegram-reply';

@Component({
  selector: 'app-telegram-reply-data-object',
  templateUrl: './telegram-reply-data-object.component.html',
  styleUrl: './telegram-reply-data-object.component.scss'
})
export class TelegramReplyDataObjectComponent {
  @Input() model: ITelegramReply;
}
