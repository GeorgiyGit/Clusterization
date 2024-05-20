import { Component, Input } from '@angular/core';
import { ITelegramReaction } from '../../models/telegram-reaction';

@Component({
  selector: 'app-telegram-reaction',
  templateUrl: './telegram-reaction.component.html',
  styleUrl: './telegram-reaction.component.scss'
})
export class TelegramReactionComponent {
  @Input() model: ITelegramReaction;
}
