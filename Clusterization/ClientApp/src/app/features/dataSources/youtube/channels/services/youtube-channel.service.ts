import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ISimpleYoutubeChannel } from '../models/responses/simple-youtube-channel';
import { IGetYoutubeChannelsRequest } from '../models/requests/get-youtube-channels-request';
import { IYoutubeChannelsWithoutLoadingResponse } from '../models/responses/youtube-channels-without-loading-response';

@Injectable({
  providedIn: 'root'
})
export class YoutubeChannelService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "YoutubeChannels/";
  }

  loadById(id:string): Observable<any> {
    return this.http.post(this.controllerUrl + "load_by_id/"+id,null);
  }

  loadManyByIds(ids:string[]): Observable<any> {
    return this.http.post(this.controllerUrl + "load_many_by_ids",{ids:ids});
  }

  getById(id:string): Observable<ISimpleYoutubeChannel> {
    return this.http.get<ISimpleYoutubeChannel>(this.controllerUrl + "get_loaded_by_id/"+id);
  }
  getMany(request:IGetYoutubeChannelsRequest): Observable<ISimpleYoutubeChannel[]> {
    return this.http.post<ISimpleYoutubeChannel[]>(this.controllerUrl + "get_loaded_collection/",request);
  }

  getWithoutLoading(name:string,nextPageToken:string | undefined,filterType:string): Observable<IYoutubeChannelsWithoutLoadingResponse> {
    return this.http.post<IYoutubeChannelsWithoutLoadingResponse>(this.controllerUrl + "get_without_loading/",{
      name:name,
      nextPageToken:nextPageToken,
      filterType:filterType
    });
  }
}
