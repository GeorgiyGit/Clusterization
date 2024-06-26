import { Component, Input } from '@angular/core';
import { IMyFullTask } from '../../models/responses/my-full-task';
import { TaskStates } from '../../models/static/task-states';

@Component({
  selector: 'app-circular-progress-bar',
  templateUrl: './circular-progress-bar.component.html',
  styleUrl: './circular-progress-bar.component.scss'
})
export class CircularProgressBarComponent {
  @Input() fullTask: IMyFullTask;

  taskStates = TaskStates;
}
