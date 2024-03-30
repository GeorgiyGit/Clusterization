import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IAddQuotasPack } from '../models/requests/add-quotas-pack';
import { IQuotasPack } from '../models/responses/quotas-pack';

@Injectable({
  providedIn: 'root'
})
export class QuotasPacksService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "QuotasPacks/";
  }

  addPack(pack: IAddQuotasPack): Observable<any> {
    return this.http.post(this.controllerUrl + "add_pack/", pack);
  }
  getAllPacks(): Observable<IQuotasPack[]> {
    return this.http.get<IQuotasPack[]>(this.controllerUrl + "get_all_packs");
  }
}
