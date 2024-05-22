import { ICustomerGetSubTasksRequest } from './../../models/requests/customer-get-subtasks-request';
import { Component, Input, OnInit } from '@angular/core';
import { IMySubTask } from '../../models/responses/my-sub-task';
import { UserTasksService } from '../../services/user-tasks.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-sub-task-list',
  templateUrl: './sub-task-list.component.html',
  styleUrl: './sub-task-list.component.scss'
})
export class SubTaskListComponent implements OnInit {
  @Input() mainTaskId: string;

  subTasks: IMySubTask[] = [];

  request: ICustomerGetSubTasksRequest = {
    pageParameters: {
      pageNumber: 1,
      pageSize: 10
    },
    groupTaskId: "",
    taskStateId: undefined
  }

  constructor(private tasksService: UserTasksService,
    private toastr: MyToastrService) { }
  ngOnInit(): void {
    this.request.groupTaskId = this.mainTaskId;
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
