import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICustomerGetTasksRequest } from '../models/requests/customer-get-tasks-request';
import { IMyFullTask } from '../models/responses/my-full-task';
import { IMyMainTask } from '../models/responses/my-main-task';
import { ICustomerGetSubTasksRequest } from '../models/requests/customer-get-subtasks-request';
import { IMySubTask } from '../models/responses/my-sub-task';

@Injectable({
  providedIn: 'root'
})
export class UserTasksService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "UserTasks/";
  }

  getMainTasks(request: ICustomerGetTasksRequest): Observable<IMyMainTask[]> {
    return this.http.post<IMyMainTask[]>(this.controllerUrl + "get_main_tasks", request);
  }
  getSubTasks(request: ICustomerGetSubTasksRequest): Observable<IMySubTask[]> {
    return this.http.post<IMySubTask[]>(this.controllerUrl + "get_sub_tasks", request);
  }

  getFullTask(id: string): Observable<IMyFullTask> {
    return this.http.get<IMyFullTask>(this.controllerUrl + "get_full/" + id);
  }
}