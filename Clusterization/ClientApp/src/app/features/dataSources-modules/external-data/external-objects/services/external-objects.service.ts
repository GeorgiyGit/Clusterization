import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IGetExternalObjectsRequest } from '../models/requests/get-external-objects-request';
import { ISimpleExternalObject } from '../models/responses/simple-external-object';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ExternalObjectsService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "ExternalObjects/";
  }

  getCollection(request:IGetExternalObjectsRequest): Observable<ISimpleExternalObject[]> {
    return this.http.post<ISimpleExternalObject[]>(this.controllerUrl + "get_collection",request);
  }
}
