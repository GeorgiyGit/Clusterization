import { Component, Input, OnInit } from '@angular/core';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { ICustomerGetSubTasksRequest } from 'src/app/features/shared-module/tasks/models/requests/customer-get-subtasks-request';
import { IMySubTask } from 'src/app/features/shared-module/tasks/models/responses/my-sub-task';
import { UserTasksService } from 'src/app/features/shared-module/tasks/services/user-tasks.service';
import { IModeratorGetSubTasksRequest } from '../../models/request/moderator-get-subtasks-request';
import { ModeratorTasksService } from '../../services/moderator-tasks.service';

@Component({
  selector: 'app-moderator-sub-task-list',
  templateUrl: './moderator-sub-task-list.component.html',
  styleUrl: './moderator-sub-task-list.component.scss'
})
export class ModeratorSubTaskListComponent implements OnInit {
  @Input() mainTaskId: string;
  @Input() customerId:string | undefined;

  subTasks: IMySubTask[] = [];

  request: IModeratorGetSubTasksRequest = {
    pageParameters: {
      pageNumber: 1,
      pageSize: 10
    },
    groupTaskId: "",
    taskStateId: undefined,
    customerId:undefined
  }

  constructor(private tasksService: ModeratorTasksService,
    private toastr: MyToastrService) { }
  ngOnInit(): void {
    this.request.groupTaskId = this.mainTaskId;
    this.request.customerId=this.customerId;
    this.loadFirst();
  }

  isLoading: boolean;
  loadFirst() {
    if (this.isLoading) return;

    this.request.pageParameters.pageNumber = 1;

    this.isLoading = true;
    this.tasksService.getSubTasks(this.request).subscribe(res => {
      this.subTasks = res;
      this.isLoading = false;

      if (res.length < this.request.pageParameters.pageSize) this.isLoadMoreAvailable = false;
      else this.isLoadMoreAvailable = true;
    }, error => {
      this.isLoading = false;
      this.toastr.error(error.error.Message);
    });
  }

  isLoading2: boolean;
  loadMore() {
    if (this.isLoading2) return;
    this.isLoading2 = true;
    this.tasksService.getSubTasks(this.request).subscribe(res => {
      this.subTasks = this.subTasks.concat(res);
      this.isLoading2 = false;

      if (res.length < this.request.pageParameters.pageSize) this.isLoadMoreAvailable = false;
      else this.isLoadMoreAvailable = true;
    }, error => {
      this.isLoading = false;
      this.toastr.error(error.error.Message);
    });
  }

  isLoadMoreAvailable: boolean;
  addMore() {
    if (this.isLoadMoreAvailable) {
      this.request.pageParameters.pageNumber++;
      this.loadMore();
    }
  }
}
