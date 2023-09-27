import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IChannelsWithoutLoadingResponse } from '../../channels/models/channels-without-loading-response';
import { IGetChannelsRequest } from '../../channels/models/get-channels-request';
import { ISimpleChannel } from '../../channels/models/simple-channel';
import { IVideosWithoutLoadingResponse } from '../models/videos-without-loading-response';
import { ISimpleVideo } from '../models/simple-video';
import { IGetVideosRequest } from '../models/get-videos-request';
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
    return this.http.post(this.controllerUrl + "load_all_by_channel",options);
  }
  loadManyByIds(ids:string[]): Observable<any> {
    return this.http.post(this.controllerUrl + "load_many_by_ids",{ids:ids});
  }

  getById(id:string): Observable<ISimpleVideo> {
    return this.http.get<ISimpleVideo>(this.controllerUrl + "get_by_id/"+id);
  }
  getMany(request:IGetVideosRequest): Observable<ISimpleVideo[]> {
    return this.http.post<ISimpleVideo[]>(this.controllerUrl + "get_many",request);
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
