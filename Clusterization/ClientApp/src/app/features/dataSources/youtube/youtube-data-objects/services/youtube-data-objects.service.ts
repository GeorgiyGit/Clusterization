import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IAddYoutubeCommentsToWorkspaceByChannelRequest } from '../models/requests/addYoutubeCommentsToWorkspaceByChannel';
import { IAddYoutubeCommentsToWorkspaceByVideosRequest } from '../models/requests/addYoutubeCommentsToWorkspaceByVideos';

@Injectable({
  providedIn: 'root'
})
export class YoutubeDataObjectsService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "YoutubeDataObjects/";
  }

  addCommentsByChannel(request: IAddYoutubeCommentsToWorkspaceByChannelRequest): Observable<any> {
    return this.http.post(this.controllerUrl + "add_comments_by_channel", request);
  }
  addCommentsByVideos(request: IAddYoutubeCommentsToWorkspaceByVideosRequest): Observable<any> {
    return this.http.post(this.controllerUrl + "add_comments_by_videos", request);
  }
}
