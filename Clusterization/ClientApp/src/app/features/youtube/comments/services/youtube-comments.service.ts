import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IVideoLoadOptions } from '../../videos/models/video-load-options';
import { ICommentLoadOptions } from '../models/comment-load-options';

@Injectable({
  providedIn: 'root'
})
export class YoutubeCommentsService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "comments/";
  }

  loadFromVideo(options:ICommentLoadOptions): Observable<any> {
    return this.http.post(this.controllerUrl + "load_from_video",options);
  }
}
