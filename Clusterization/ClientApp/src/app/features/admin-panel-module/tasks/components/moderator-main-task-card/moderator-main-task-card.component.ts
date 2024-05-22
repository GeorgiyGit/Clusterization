import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { IMyMainTask } from 'src/app/features/shared-module/tasks/models/responses/my-main-task';
import { TaskStates } from 'src/app/features/shared-module/tasks/models/static/task-states';

@Component({
  selector: 'app-moderator-main-task-card',
  templateUrl: './moderator-main-task-card.component.html',
  styleUrl: './moderator-main-task-card.component.scss'
})
export class ModeratorMainTaskCardComponent {
  @Input() task: IMyMainTask;
  @Input() customerId:string | undefined;
  
  isSubTasksOpen: boolean;

  constructor(private router: Router) { }
  openFull(event: MouseEvent) {
    event.stopPropagation();
    console.log(this.task);

    this.router.navigate([{ outlets: { overflow: 'tasks/full/' + this.task.id } }]);
  }

  taskStates = TaskStates;

  toggleSubTasksOpen(event:MouseEvent) {
    event.stopPropagation();

    this.isSubTasksOpen = !this.isSubTasksOpen;
  }
}
