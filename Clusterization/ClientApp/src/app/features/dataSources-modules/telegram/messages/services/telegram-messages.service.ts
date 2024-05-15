import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ISimpleTelegramMessage } from '../models/responses/simple-telegram-message';
import { IFullTelegramMessage } from '../models/responses/full-telegram-message';
import { IGetTelegramMessagesRequest } from '../models/requests/get-telegram-messages-request';
import { ITelegramMessageLoadByIdRequest } from '../models/requests/telegram-message-load-by-id-request';
import { ITelegramLoadOptions } from '../models/telegram-load-optionts';
import { ITelegramMessageLoadManyByIdsRequest } from '../models/requests/telegram-message-load-many-by-ids-request';

@Injectable({
  providedIn: 'root'
})
export class TelegramMessagesService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "TelegramMessages/";
  }

  loadById(requet:ITelegramMessageLoadByIdRequest): Observable<any> {
    return this.http.post(this.controllerUrl + "load_by_id",requet);
  }

  loadByChannel(options:ITelegramLoadOptions): Observable<any> {
    return this.http.post(this.controllerUrl + "load_from_channel",options);
  }
  loadManyByIds(request: ITelegramMessageLoadManyByIdsRequest): Observable<any> {
    return this.http.post(this.controllerUrl + "load_many_by_ids",request);
  }

  getById(id:string): Observable<IFullTelegramMessage> {
    return this.http.get<IFullTelegramMessage>(this.controllerUrl + "get_loaded_by_id/"+id);
  }
  getMany(request:IGetTelegramMessagesRequest): Observable<ISimpleTelegramMessage[]> {
    return this.http.post<ISimpleTelegramMessage[]>(this.controllerUrl + "get_loaded_collection",request);
  }
  getCustomerMany(request:IGetTelegramMessagesRequest): Observable<ISimpleTelegramMessage[]> {
    return this.http.post<ISimpleTelegramMessage[]>(this.controllerUrl + "get_customer_loaded_collection",request);
  }

  getWithoutLoading(request:IGetTelegramMessagesRequest): Observable<ISimpleTelegramMessage[]> {
    return this.http.post<ISimpleTelegramMessage[]>(this.controllerUrl + "get_without_loading/",request);
  }
}
