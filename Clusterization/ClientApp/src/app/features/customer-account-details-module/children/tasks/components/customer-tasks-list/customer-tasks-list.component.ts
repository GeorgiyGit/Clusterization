import { Component, OnDestroy, OnInit } from '@angular/core';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { AccountService } from 'src/app/features/shared-module/account/services/account.service';
import { SignalRDataTypes } from 'src/app/features/shared-module/signalR/models/data-types';
import { SignalRHubService } from 'src/app/features/shared-module/signalR/services/signal-r-hub.service';
import { ICustomerGetTasksRequest } from 'src/app/features/shared-module/tasks/models/requests/customer-get-tasks-request';
import { IMyMainTask } from 'src/app/features/shared-module/tasks/models/responses/my-main-task';
import { IMyTask } from 'src/app/features/shared-module/tasks/models/responses/my-task';
import { IChangeTaskPercentsEvent } from 'src/app/features/shared-module/tasks/models/signalR/change-percents-event';
import { IChangeTaskStateEvent } from 'src/app/features/shared-module/tasks/models/signalR/change-task-state-event';
import { UserTasksService } from 'src/app/features/shared-module/tasks/services/user-tasks.service';

@Component({
  selector: 'app-customer-tasks-list',
  templateUrl: './customer-tasks-list.component.html',
  styleUrl: './customer-tasks-list.component.scss'
})
export class CustomerTasksListComponent implements OnInit, OnDestroy {
  tasks: IMyMainTask[] = [];

  request: ICustomerGetTasksRequest = {
    taskStateId: undefined,
    pageParameters: {
      pageNumber: 1,
      pageSize: 10
    }
  }
  constructor(private userTasksService: UserTasksService,
    private toastr: MyToastrService,
    private signalRHubService: SignalRHubService,
    private accountService:AccountService) { }
  ngOnDestroy(): void {
    if (this.signalRHubService.isConnected()) {
      this.signalRHubService.stopConnection();
      //this.messageIdSubscription.unsubscribe();
    }
  }
  ngOnInit(): void {
    this.loadFirst();
    this.signalRHubService.startConnection(this.accountService.getUserId());
    this.signalRHubService.getNewMessage().subscribe((res) => {
      if (res.dataType == SignalRDataTypes.ChangeTaskPercents) {
        let model = res as IChangeTaskPercentsEvent;
        let task = this.tasks.find(e => e.id == model.taskId);
        if (task != null) {
          task.percent = model.percents;
        }
      }
      else if (res.dataType == SignalRDataTypes.ChangeTaskStates) {
        let model = res as IChangeTaskStateEvent;
        let task = this.tasks.find(e => e.id == model.taskId);
        if (task != null) {
          task.stateId = model.stateId;
          task.stateName = model.stateName;
        }
        else if (model.groupTaskId != null) {
          task = this.tasks.find(e => e.id == model.groupTaskId);

          if (task != null) {
            let subTask = task.subTasks.find(e=>e.id==model.taskId);

            if(subTask!=null){
              subTask.stateId = model.stateId;
            }
          }
        }
      }
    }, error => {
      this.toastr.error(error.error.Message);
    });
  }

  isLoadMoreAvailable: boolean;
  isLoading: boolean;
  loadFirst() {
    this.request.pageParameters.pageNumber = 1;

    this.isLoading = true;
    this.userTasksService.getMainTasks(this.request).subscribe(res => {
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
    this.userTasksService.getMainTasks(this.request).subscribe(res => {
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
    this.request.taskStateId = state;
    this.loadFirst();
  }
}
