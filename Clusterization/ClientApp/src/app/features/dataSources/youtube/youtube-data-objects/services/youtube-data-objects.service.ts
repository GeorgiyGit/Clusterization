import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IAddCommentsToWorkspaceByChannelRequest } from '../models/requests/addCommentsToWorkspaceByChannel';
import { IAddCommentsToWorkspaceByVideosRequest } from '../models/requests/addCommentsToWorkspaceByVideos';

@Injectable({
  providedIn: 'root'
})
export class YoutubeDataObjectsService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "YoutubeDataObjects/";
  }

  addCommentsByChannel(request: IAddCommentsToWorkspaceByChannelRequest): Observable<any> {
    return this.http.post(this.controllerUrl + "add_comments_by_channel", request);
  }
  addCommentsByVideos(request: IAddCommentsToWorkspaceByVideosRequest): Observable<any> {
    return this.http.post(this.controllerUrl + "add_comments_by_videos", request);
  }
}
