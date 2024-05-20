import { Component, Input } from '@angular/core';
import { IFullTelegramMessage } from 'src/app/features/dataSources-modules/telegram/messages/models/responses/full-telegram-message';

@Component({
  selector: 'app-telegram-message-data-object',
  templateUrl: './telegram-message-data-object.component.html',
  styleUrl: './telegram-message-data-object.component.scss'
})
export class TelegramMessageDataObjectComponent {
  @Input() model: IFullTelegramMessage;
}
