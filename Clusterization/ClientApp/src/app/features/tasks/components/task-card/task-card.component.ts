import { Component, Input } from '@angular/core';
import { IMyTask } from '../../models/responses/myTask';
import { Router } from '@angular/router';

@Component({
  selector: 'app-task-card',
  templateUrl: './task-card.component.html',
  styleUrls: ['./task-card.component.scss']
})
export class TaskCardComponent {
  @Input() task: IMyTask;

  constructor(private router: Router) { }
  openFull(event:MouseEvent) {
    event.stopPropagation();

    this.router.navigate([{ outlets: { overflow: 'tasks-details/'+this.task.id } }]);
  }
}
