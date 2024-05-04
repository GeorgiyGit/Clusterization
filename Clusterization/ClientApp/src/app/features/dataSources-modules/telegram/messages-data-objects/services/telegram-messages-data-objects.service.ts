import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IAddTelegramMessagesToWorkspaceByChannelRequest } from '../models/AddTGMsgToWorkspaceByChannelRequest';

@Injectable({
  providedIn: 'root'
})
export class TelegramMessagesDataObjectsService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "TelegramMessagesDataObjects/";
  }

  addMessagesByChannel(request: IAddTelegramMessagesToWorkspaceByChannelRequest): Observable<any> {
    return this.http.post(this.controllerUrl + "add_messages_by_channel", request);
  }
}
