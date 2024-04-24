import { ICustomerGetTasksRequest } from 'src/app/features/tasks/models/requests/customer-get-tasks-request';
import { UserTasksService } from './../../../../../tasks/services/user-tasks.service';
import { Component, OnInit } from '@angular/core';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IMyTask } from 'src/app/features/tasks/models/responses/myTask';

@Component({
  selector: 'app-customer-tasks-list',
  templateUrl: './customer-tasks-list.component.html',
  styleUrl: './customer-tasks-list.component.scss'
})
export class CustomerTasksListComponent implements OnInit {
  tasks: IMyTask[] = [];

  request: ICustomerGetTasksRequest = {
    taskStateId: undefined,
    pageParameters: {
      pageNumber: 1,
      pageSize: 10
    }
  }
  constructor(private userTasksService: UserTasksService,
    private toastr: MyToastrService) { }
  ngOnInit(): void {
    this.loadFirst();
  }

  isLoadMoreAvailable: boolean;
  isLoading: boolean;
  loadFirst() {
    this.request.pageParameters.pageNumber=1;

    this.isLoading = true;
    this.userTasksService.getTasks(this.request).subscribe(res => {
      this.isLoading = false;
      this.tasks = res;

      if (res.length < this.request.pageParameters.pageSize) this.isLoadMoreAvailable = false;
      else this.isLoadMoreAvailable = true;
    }, error => {
      this.isLoading = false;
      this.toastr.error(error.error.Message);
    })
  }

  isLoading2: boolean;
  loadMore() {
    this.request.pageParameters.pageNumber++;

    this.isLoading2 = true;
    this.userTasksService.getTasks(this.request).subscribe(res => {
      this.isLoading2 = false;
      this.tasks = this.tasks.concat(res);

      if (res.length < this.request.pageParameters.pageSize) this.isLoadMoreAvailable = false;
      else this.isLoadMoreAvailable = true;
    }, error => {
      this.isLoading2 = false;
      this.toastr.error(error.error.Message);
    })
  }
  selectStatus(state: string) {
    this.request.taskStateId=state;
    this.loadFirst();
  }
}
