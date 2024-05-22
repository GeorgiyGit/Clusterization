import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { IMySubTask } from 'src/app/features/shared-module/tasks/models/responses/my-sub-task';
import { TaskStates } from 'src/app/features/shared-module/tasks/models/static/task-states';

@Component({
  selector: 'app-moderator-sub-task-card',
  templateUrl: './moderator-sub-task-card.component.html',
  styleUrl: './moderator-sub-task-card.component.scss'
})
export class ModeratorSubTaskCardComponent {
  @Input() task: IMySubTask;

  constructor(private router: Router) { }
  openFull(event: MouseEvent) {
    event.stopPropagation();

    this.router.navigate([{ outlets: { overflow: 'tasks/full/' + this.task.id } }]);
  }

  taskStates = TaskStates;
}
