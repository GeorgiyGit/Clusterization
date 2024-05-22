import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { IMySubTask } from '../../models/responses/my-sub-task';
import { TaskStates } from '../../models/static/task-states';

@Component({
  selector: 'app-sub-task-card',
  templateUrl: './sub-task-card.component.html',
  styleUrl: './sub-task-card.component.scss'
})
export class SubTaskCardComponent {
  @Input() task: IMySubTask;

  constructor(private router: Router) { }
  openFull(event: MouseEvent) {
    event.stopPropagation();

    this.router.navigate([{ outlets: { overflow: 'tasks/full/' + this.task.id } }]);
  }

  taskStates = TaskStates;
}
