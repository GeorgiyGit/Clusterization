import { TaskStates } from './../../models/static/task-states';
import { Component, Input } from '@angular/core';
import { IMyMainTask } from '../../models/responses/my-main-task';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main-task-card',
  templateUrl: './main-task-card.component.html',
  styleUrl: './main-task-card.component.scss'
})
export class MainTaskCardComponent {
  @Input() task: IMyMainTask;

  isSubTasksOpen: boolean;

  constructor(private router: Router) { }
  openFull(event: MouseEvent) {
    event.stopPropagation();

    this.router.navigate([{ outlets: { overflow: 'tasks/full/' + this.task.id } }]);
  }

  taskStates = TaskStates;

  toggleSubTasksOpen(event:MouseEvent) {
    event.stopPropagation();

    this.isSubTasksOpen = !this.isSubTasksOpen;
  }
}
