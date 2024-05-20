import { Component, Input } from '@angular/core';
import { ITelegramReaction } from '../../models/telegram-reaction';

@Component({
  selector: 'app-telegram-reaction-list',
  templateUrl: './telegram-reaction-list.component.html',
  styleUrl: './telegram-reaction-list.component.scss'
})
export class TelegramReactionListComponent {
  @Input() reactions: ITelegramReaction[] = [];
}
