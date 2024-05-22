import { Component, Input } from '@angular/core';
import { IMyMainTask } from 'src/app/features/shared-module/tasks/models/responses/my-main-task';

@Component({
  selector: 'app-moderator-main-task-list',
  templateUrl: './moderator-main-task-list.component.html',
  styleUrl: './moderator-main-task-list.component.scss'
})
export class ModeratorMainTaskListComponent {
  @Input() tasks: IMyMainTask[] = [];
}
