import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IAddTelegramRepliesToWorkspaceByChannelRequest } from '../models/add-tg-replies-by-channel';
import { IAddTelegramRepliesToWorkspaceByMessagesRequest } from '../models/add-tg-replies-by-messages';

@Injectable({
  providedIn: 'root'
})
export class TelegramRepliesDataObjectsService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "TelegramRepliesDataObjects/";
  }

  addRepliesByChannel(request: IAddTelegramRepliesToWorkspaceByChannelRequest): Observable<any> {
    return this.http.post(this.controllerUrl + "add_replies_by_channel", request);
  }
  addRepliesByMessages(request: IAddTelegramRepliesToWorkspaceByMessagesRequest): Observable<any> {
    return this.http.post(this.controllerUrl + "add_replies_by_messages", request);
  }
}
