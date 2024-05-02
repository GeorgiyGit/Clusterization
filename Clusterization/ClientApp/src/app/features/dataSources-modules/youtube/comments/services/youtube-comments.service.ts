import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IYoutubeCommentLoadOptions } from '../models/youtube-comment-load-options';

@Injectable({
  providedIn: 'root'
})
export class YoutubeCommentsService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "YoutubeComments/";
  }

  loadFromVideo(options:IYoutubeCommentLoadOptions): Observable<any> {
    return this.http.post(this.controllerUrl + "load_from_video",options);
  }
  loadFromChannel(options:IYoutubeCommentLoadOptions): Observable<any> {
    return this.http.post(this.controllerUrl + "load_from_channel",options);
  }
}
