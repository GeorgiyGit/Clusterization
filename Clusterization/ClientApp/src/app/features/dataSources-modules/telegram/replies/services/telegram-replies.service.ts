import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ITelegramLoadOptions } from '../../messages/models/telegram-load-optionts';
import { ILoadTelegramRepliesByChannelRequest } from '../models/load-telegram-replies-by-channel-request';

@Injectable({
  providedIn: 'root'
})
export class TelegramRepliesService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "TelegramReplies/";
  }

  loadFromMessage(options:ITelegramLoadOptions): Observable<any> {
    return this.http.post(this.controllerUrl + "load_from_message",options);
  }
  loadFromChannel(options:ILoadTelegramRepliesByChannelRequest): Observable<any> {
    return this.http.post(this.controllerUrl + "load_from_channel",options);
  }
}
