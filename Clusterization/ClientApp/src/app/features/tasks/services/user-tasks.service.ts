import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IMyTask } from '../models/myTask';
import { ICustomerGetTasksRequest } from '../models/customer-get-tasks-request';

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

  getFullTask(id:number): Observable<IMyTask> {
    return this.http.get<IMyTask>(this.controllerUrl + "get_full/"+id);
  }
}