import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IGetEntityTasksRequest } from 'src/app/features/shared-module/tasks/models/requests/get-entity-tasks-request';
import { IMyMainTask } from 'src/app/features/shared-module/tasks/models/responses/my-main-task';
import { EntitiesTasksService } from 'src/app/features/shared-module/tasks/services/entities-tasks.service';

@Component({
  selector: 'app-external-packs-tasks-page',
  templateUrl: './external-packs-tasks-page.component.html',
  styleUrl: './external-packs-tasks-page.component.scss'
})
export class ExternalPacksTasksPageComponent implements OnInit {
  tasks: IMyMainTask[] = [];

  request: IGetEntityTasksRequest<number> = {
    id: -1,
    taskStateId: undefined,
    pageParameters: {
      pageNumber: 1,
      pageSize: 10
    }
  }
  constructor(private entitiesTasksService: EntitiesTasksService,
    private toastr: MyToastrService,
    private route: ActivatedRoute) { }
  ngOnInit(): void {
    this.request.id = this.route.snapshot.params['packId'];
    this.loadFirst();
  }

  isLoadMoreAvailable: boolean;
  isLoading: boolean;
  loadFirst() {
    this.request.pageParameters.pageNumber = 1;

    this.isLoading = true;
    this.entitiesTasksService.getExternalObjectsPackTasks(this.request).subscribe(res => {
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
    this.entitiesTasksService.getExternalObjectsPackTasks(this.request).subscribe(res => {
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
