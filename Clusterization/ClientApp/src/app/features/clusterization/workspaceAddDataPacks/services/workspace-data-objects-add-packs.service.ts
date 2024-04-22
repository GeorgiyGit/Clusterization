import { Injectable } from '@angular/core';
import { HttpClient, } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { IGetWorkspaceAddDataPacksRequest } from '../models/requests/get-workspace-add-data-packs-request';
import { ISimpleWorkspaceAddDataPack } from '../models/responses/simple-workspace-add-data-pack';
import { IGetCustomerWorkspaceAddDataPacksRequest } from '../models/requests/get-customer-workspace-add-data-packs-request';
import { IFullWorkspaceAddDataPack } from '../models/responses/full-workspace-add-data-pack';

@Injectable({
  providedIn: 'root'
})
export class WorkspaceDataObjectsAddPacksService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "WorkspaceDataObjectsAddPacks/";
  }

  getSimplePacks(request: IGetWorkspaceAddDataPacksRequest): Observable<ISimpleWorkspaceAddDataPack[]> {
    return this.http.post<ISimpleWorkspaceAddDataPack[]>(this.controllerUrl + "get_simple_list", request);
  }

  getCustomerSimplePacks(request: IGetCustomerWorkspaceAddDataPacksRequest): Observable<ISimpleWorkspaceAddDataPack[]> {
    return this.http.post<ISimpleWorkspaceAddDataPack[]>(this.controllerUrl + "get_customer_simple_list", request);
  }
  
  getFullPack(id:number): Observable<IFullWorkspaceAddDataPack> {
    return this.http.get<IFullWorkspaceAddDataPack>(this.controllerUrl + "get_full/"+id);
  }


  delete(id:number): Observable<IFullWorkspaceAddDataPack> {
    return this.http.delete<IFullWorkspaceAddDataPack>(this.controllerUrl + "delete/"+id);
  }
  restore(id:number): Observable<IFullWorkspaceAddDataPack> {
    return this.http.post<IFullWorkspaceAddDataPack>(this.controllerUrl + "restore/"+id,null);
  }
}
