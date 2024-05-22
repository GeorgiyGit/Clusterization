import { Component, OnInit } from '@angular/core';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IMyMainTask } from 'src/app/features/shared-module/tasks/models/responses/my-main-task';
import { IModeratorGetTasksRequest } from '../../models/request/moderator-get-tasks-request';
import { ActivatedRoute } from '@angular/router';
import { ModeratorTasksService } from '../../services/moderator-tasks.service';

@Component({
  selector: 'app-moderator-task-list-page',
  templateUrl: './moderator-task-list-page.component.html',
  styleUrl: './moderator-task-list-page.component.scss'
})
export class ModeratorTaskListPageComponent implements OnInit {
  tasks: IMyMainTask[] = [];
  customerId:string | undefined;

  request: IModeratorGetTasksRequest = {
    taskStateId: undefined,
    pageParameters: {
      pageNumber: 1,
      pageSize: 10
    },
    customerId:undefined
  }
  constructor(private tasksService: ModeratorTasksService,
    private toastr: MyToastrService,
    private route:ActivatedRoute) { }
  ngOnInit(): void {
    this.customerId=this.route.snapshot.params['customerId'];
    this.request.customerId=this.customerId;
    this.loadFirst();
  }

  isLoadMoreAvailable: boolean;
  isLoading: boolean;
  loadFirst() {
    this.request.pageParameters.pageNumber=1;

    this.isLoading = true;
    this.tasksService.getMainTasks(this.request).subscribe(res => {
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
    this.tasksService.getMainTasks(this.request).subscribe(res => {
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
