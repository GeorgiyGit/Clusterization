import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IVideosWithoutLoadingResponse } from '../models/responses/videos-without-loading-response';
import { ISimpleVideo } from '../models/responses/simple-video';
import { IGetVideosRequest } from '../models/requests/get-videos-request';
import { IVideoLoadOptions } from '../models/video-load-options';

@Injectable({
  providedIn: 'root'
})
export class YoutubeVideoService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "videos/";
  }

  loadById(id:string): Observable<any> {
    return this.http.post(this.controllerUrl + "load_by_id/"+id,null);
  }

  loadByChannel(options:IVideoLoadOptions): Observable<any> {
    return this.http.post(this.controllerUrl + "load_from_channel",options);
  }
  loadManyByIds(ids:string[]): Observable<any> {
    return this.http.post(this.controllerUrl + "load_many_by_ids",{ids:ids});
  }

  getById(id:string): Observable<ISimpleVideo> {
    return this.http.get<ISimpleVideo>(this.controllerUrl + "get_loaded_by_id/"+id);
  }
  getMany(request:IGetVideosRequest): Observable<ISimpleVideo[]> {
    return this.http.post<ISimpleVideo[]>(this.controllerUrl + "get_loaded_collection",request);
  }

  getWithoutLoading(name:string,nextPageToken:string | undefined, channelId:string | undefined,filterType:string): Observable<IVideosWithoutLoadingResponse> {
    return this.http.post<IVideosWithoutLoadingResponse>(this.controllerUrl + "get_without_loading/",{
      name:name,
      nextPageToken:nextPageToken,
      channelId:channelId,
      filterType:filterType
    });
  }
}
