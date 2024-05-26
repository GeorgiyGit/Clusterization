import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IGetEntityTasksRequest } from '../models/requests/get-entity-tasks-request';
import { IMyMainTask } from '../models/responses/my-main-task';

@Injectable({
  providedIn: 'root'
})
export class EntitiesTasksService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "EntitiesTasks/";
  }

  getWorkspaceTasks(request: IGetEntityTasksRequest<number>): Observable<IMyMainTask[]> {
    return this.http.post<IMyMainTask[]>(this.controllerUrl + "get_workspace_tasks", request);
  }
  getProfileTasks(request: IGetEntityTasksRequest<number>): Observable<IMyMainTask[]> {
    return this.http.post<IMyMainTask[]>(this.controllerUrl + "get_profile_tasks", request);
  }

  getYoutubeChannelTasks(request: IGetEntityTasksRequest<string>): Observable<IMyMainTask[]> {
    return this.http.post<IMyMainTask[]>(this.controllerUrl + "get_youtube_channel_tasks", request);
  }
  getYoutubeVideoTasks(request: IGetEntityTasksRequest<string>): Observable<IMyMainTask[]> {
    return this.http.post<IMyMainTask[]>(this.controllerUrl + "get_youtube_video_tasks", request);
  }

  getTelegramChannelTasks(request: IGetEntityTasksRequest<number>): Observable<IMyMainTask[]> {
    return this.http.post<IMyMainTask[]>(this.controllerUrl + "get_telegram_channel_tasks", request);
  }
  getTelegramMessageTasks(request: IGetEntityTasksRequest<number>): Observable<IMyMainTask[]> {
    return this.http.post<IMyMainTask[]>(this.controllerUrl + "get_telegram_message_tasks", request);
  }

  getExternalObjectsPackTasks(request: IGetEntityTasksRequest<number>): Observable<IMyMainTask[]> {
    return this.http.post<IMyMainTask[]>(this.controllerUrl + "get_external_pack_tasks", request);
  }
}
