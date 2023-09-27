import { Component, Input } from '@angular/core';
import { IMyTask } from '../../models/myTask';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss']
})
export class TaskListComponent {
  @Input() tasks:IMyTask[]=[];
}
