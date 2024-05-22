import { Component, Input } from '@angular/core';
import { IMyMainTask } from '../../models/responses/my-main-task';

@Component({
  selector: 'app-main-task-list',
  templateUrl: './main-task-list.component.html',
  styleUrl: './main-task-list.component.scss'
})
export class MainTaskListComponent {
  @Input() tasks: IMyMainTask[] = [];
}
