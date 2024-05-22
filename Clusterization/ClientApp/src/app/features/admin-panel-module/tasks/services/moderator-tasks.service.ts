import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IModeratorGetTasksRequest } from '../models/request/moderator-get-tasks-request';
import { IModeratorGetSubTasksRequest } from '../models/request/moderator-get-subtasks-request';
import { IMyFullTask } from 'src/app/features/shared-module/tasks/models/responses/my-full-task';
import { IMyMainTask } from 'src/app/features/shared-module/tasks/models/responses/my-main-task';
import { IMySubTask } from 'src/app/features/shared-module/tasks/models/responses/my-sub-task';

@Injectable({
  providedIn: 'root'
})
export class ModeratorTasksService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "ModeratorTasks/";
  }

  getMainTasks(request: IModeratorGetTasksRequest): Observable<IMyMainTask[]> {
    return this.http.post<IMyMainTask[]>(this.controllerUrl + "get_main_tasks", request);
  }
  getSubTasks(request: IModeratorGetSubTasksRequest): Observable<IMySubTask[]> {
    return this.http.post<IMySubTask[]>(this.controllerUrl + "get_sub_tasks", request);
  }

  getFullTask(id: string): Observable<IMyFullTask> {
    return this.http.get<IMyFullTask>(this.controllerUrl + "get_full/" + id);
  }
}
