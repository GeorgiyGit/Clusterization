import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IGetTelegramChannelsRequest } from '../models/requests/get-telegram-channels-request';
import { ISimpleTelegramChannel } from '../models/responses/simple-telegram-channel';
import { IFullTelegramChannel } from '../models/responses/full-telegram-channel';

@Injectable({
  providedIn: 'root'
})
export class TelegramChannelsService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "TelegramChannels/";
  }

  loadByUsername(username: string): Observable<any> {
    return this.http.post(this.controllerUrl + "load_by_username/" + username, null);
  }

  loadManyByUsernames(usernames: string[]): Observable<any> {
    return this.http.post(this.controllerUrl + "load_many_by_usernames", { ids: usernames });
  }

  getById(id: number): Observable<IFullTelegramChannel> {
    return this.http.get<IFullTelegramChannel>(this.controllerUrl + "get_loaded_by_id/" + id);
  }
  getMany(request: IGetTelegramChannelsRequest): Observable<ISimpleTelegramChannel[]> {
    return this.http.post<ISimpleTelegramChannel[]>(this.controllerUrl + "get_loaded_collection/", request);
  }
  getCustomerMany(request: IGetTelegramChannelsRequest): Observable<ISimpleTelegramChannel[]> {
    return this.http.post<ISimpleTelegramChannel[]>(this.controllerUrl + "get_customer_loaded_collection/", request);
  }

  getWithoutLoading(name: string): Observable<ISimpleTelegramChannel[]> {
    return this.http.get<ISimpleTelegramChannel[]>(this.controllerUrl + "get_without_loading/"+name);
  }
}