import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IQuotasType } from '../models/responses/quotas-type';

@Injectable({
  providedIn: 'root'
})
export class QuotasTypesService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "QuotasTypes/";
  }

  getAllTypes(): Observable<IQuotasType[]> {
    return this.http.get<IQuotasType[]>(this.controllerUrl + "get_all");
  }
}
