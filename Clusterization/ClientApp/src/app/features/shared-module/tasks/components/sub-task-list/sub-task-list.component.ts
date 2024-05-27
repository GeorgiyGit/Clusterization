import { ICustomerGetSubTasksRequest } from './../../models/requests/customer-get-subtasks-request';
import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { IMySubTask } from '../../models/responses/my-sub-task';
import { UserTasksService } from '../../services/user-tasks.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { SignalRHubService } from '../../../signalR/services/signal-r-hub.service';
import { AccountService } from '../../../account/services/account.service';
import { SignalRDataTypes } from '../../../signalR/models/data-types';
import { IChangeTaskPercentsEvent } from '../../models/signalR/change-percents-event';
import { IChangeTaskStateEvent } from '../../models/signalR/change-task-state-event';

@Component({
  selector: 'app-sub-task-list',
  templateUrl: './sub-task-list.component.html',
  styleUrl: './sub-task-list.component.scss'
})
export class SubTaskListComponent implements OnInit, OnDestroy {
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
    private toastr: MyToastrService,
    private signalRHubService: SignalRHubService,
    private accountService:AccountService) { }
  ngOnDestroy(): void {
    if (this.signalRHubService.isConnected()) {
      this.signalRHubService.stopConnection();
    }
  }
  ngOnInit(): void {
    this.request.groupTaskId = this.mainTaskId;
    this.loadFirst();
    this.signalRHubService.startConnection(this.accountService.getUserId());
    this.signalRHubService.getNewMessage().subscribe((res) => {
      if (res.dataType == SignalRDataTypes.ChangeTaskPercents) {
        let model = res as IChangeTaskPercentsEvent;
        let task = this.subTasks.find(e => e.id == model.taskId);
        if (task != null) {
          task.percent = model.percents;
        }
      }
      else if (res.dataType == SignalRDataTypes.ChangeTaskStates) {
        let model = res as IChangeTaskStateEvent;
        let task = this.subTasks.find(e => e.id == model.taskId);
        if (task != null) {
          task.stateId = model.stateId;
          task.stateName = model.stateName;
        }
      }
    }, error => {
      this.toastr.error(error.error.Message);
    });
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
