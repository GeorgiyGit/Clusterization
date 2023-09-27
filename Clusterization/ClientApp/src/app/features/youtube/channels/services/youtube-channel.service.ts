import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ISimpleChannel } from '../models/simple-channel';
import { IGetChannelsRequest } from '../models/get-channels-request';
import { IChannelsWithoutLoadingResponse } from '../models/channels-without-loading-response';

@Injectable({
  providedIn: 'root'
})
export class YoutubeChannelService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "channels/";
  }

  loadById(id:string): Observable<any> {
    return this.http.post(this.controllerUrl + "load_by_id/"+id,null);
  }

  loadManyByIds(ids:string[]): Observable<any> {
    return this.http.post(this.controllerUrl + "load_many_by_ids",{ids:ids});
  }

  getById(id:string): Observable<ISimpleChannel> {
    return this.http.get<ISimpleChannel>(this.controllerUrl + "get_by_id/"+id);
  }
  getMany(request:IGetChannelsRequest): Observable<ISimpleChannel[]> {
    return this.http.post<ISimpleChannel[]>(this.controllerUrl + "get_many/",request);
  }

  getWithoutLoading(name:string,nextPageToken:string | undefined,filterType:string): Observable<IChannelsWithoutLoadingResponse> {
    return this.http.post<IChannelsWithoutLoadingResponse>(this.controllerUrl + "get_without_loading/",{
      name:name,
      nextPageToken:nextPageToken,
      filterType:filterType
    });
  }
}
