import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IMyTask } from '../models/responses/myTask';
import { ICustomerGetTasksRequest } from '../models/requests/customer-get-tasks-request';
import { IMyFullTask } from '../models/responses/my-full-task';

@Injectable({
  providedIn: 'root'
})
export class UserTasksService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "UserTasks/";
  }

  getTasks(request:ICustomerGetTasksRequest): Observable<IMyTask[]> {
    return this.http.post<IMyTask[]>(this.controllerUrl + "get_collection",request);
  }

  getFullTask(id:number): Observable<IMyFullTask> {
    return this.http.get<IMyFullTask>(this.controllerUrl + "get_full/"+id);
  }
}