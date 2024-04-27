import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IYoutubeVideoLoadOptions } from '../models/youtube-video-load-options';
import { IYoutubeVideosWithoutLoadingResponse } from '../models/responses/youtube-videos-without-loading-response';
import { IGetYoutubeVideosRequest } from '../models/requests/get-youtube-videos-request';
import { ISimpleYoutubeVideo } from '../models/responses/simple-youtube-video';

@Injectable({
  providedIn: 'root'
})
export class YoutubeVideoService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "YoutubeVideos/";
  }

  loadById(id:string): Observable<any> {
    return this.http.post(this.controllerUrl + "load_by_id/"+id,null);
  }

  loadByChannel(options:IYoutubeVideoLoadOptions): Observable<any> {
    return this.http.post(this.controllerUrl + "load_from_channel",options);
  }
  loadManyByIds(ids:string[]): Observable<any> {
    return this.http.post(this.controllerUrl + "load_many_by_ids",{ids:ids});
  }

  getById(id:string): Observable<ISimpleYoutubeVideo> {
    return this.http.get<ISimpleYoutubeVideo>(this.controllerUrl + "get_loaded_by_id/"+id);
  }
  getMany(request:IGetYoutubeVideosRequest): Observable<ISimpleYoutubeVideo[]> {
    return this.http.post<ISimpleYoutubeVideo[]>(this.controllerUrl + "get_loaded_collection",request);
  }

  getWithoutLoading(name:string,nextPageToken:string | undefined, channelId:string | undefined,filterType:string): Observable<IYoutubeVideosWithoutLoadingResponse> {
    return this.http.post<IYoutubeVideosWithoutLoadingResponse>(this.controllerUrl + "get_without_loading/",{
      name:name,
      nextPageToken:nextPageToken,
      channelId:channelId,
      filterType:filterType
    });
  }
}
