import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IMyTask } from '../models/myTask';

@Injectable({
  providedIn: 'root'
})
export class MyTaskService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "myTask/";
  }

  getAll(): Observable<IMyTask[]> {
    return this.http.get<IMyTask[]>(this.controllerUrl + "get_all");
  }

}
