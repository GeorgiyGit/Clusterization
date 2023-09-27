import { Component, Input } from '@angular/core';
import { IMyTask } from '../../models/myTask';

@Component({
  selector: 'app-task-card',
  templateUrl: './task-card.component.html',
  styleUrls: ['./task-card.component.scss']
})
export class TaskCardComponent {
  @Input() task:IMyTask;
}
