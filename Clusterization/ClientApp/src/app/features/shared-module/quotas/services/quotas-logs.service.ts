import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IQuotasLogs } from '../models/responses/quotas-logs';
import { IGetQuotasLogsRequest } from '../models/requests/get-quotas-logs-request';
import { IGetQuotasPackLogsRequest } from '../models/requests/get-quotas-pack-logs-request';
import { IQuotasPackLogs } from '../models/responses/quotas-pack-logs';

@Injectable({
  providedIn: 'root'
})
export class QuotasLogsService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "QuotasLogs/";
  }

  getQuotasLogs(request:IGetQuotasLogsRequest): Observable<IQuotasLogs[]> {
    return this.http.post<IQuotasLogs[]>(this.controllerUrl + "get_quotas_logs",request);
  }

  getQuotasPackLogs(request:IGetQuotasPackLogsRequest): Observable<IQuotasPackLogs[]> {
    return this.http.post<IQuotasPackLogs[]>(this.controllerUrl + "get_quotas_pack_logs",request);
  }
}
