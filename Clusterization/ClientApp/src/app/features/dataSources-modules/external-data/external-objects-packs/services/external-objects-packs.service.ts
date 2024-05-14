import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { ILoadExternalObjectsRequest } from '../models/requests/load-external-objects-request';
import { IAddExternalObjectsToWorkspaceRequest } from '../models/requests/add-external-objects-to-workspace-request';
import { IGetExternalObjectsPacksRequest } from '../models/requests/get-external-objects-packs-request';
import { IUpdateExternalObjectsPackRequest } from '../models/requests/update-external-objects-pack-request';

@Injectable({
  providedIn: 'root'
})
export class ExternalObjectsPacksService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "ExternalObjectsPacks/";
  }

  load(request:ILoadExternalObjectsRequest): Observable<any> {
    const formData = new FormData();
    
    // Append each property to the FormData object
    formData.append('file', request.file);
    formData.append('workspaceId', String(request.workspaceId));
    formData.append('visibleType', request.visibleType);
    formData.append('changingType', request.changingType);
    formData.append('title', request.title);
    formData.append('description', request.description);

    return this.http.post(this.controllerUrl + "load",formData);
  }
  add(request:IAddExternalObjectsToWorkspaceRequest): Observable<any> {
    return this.http.post(this.controllerUrl + "add",request);
  }
  update(request:IUpdateExternalObjectsPackRequest): Observable<any> {
    return this.http.put(this.controllerUrl + "update",request);
  }
  loadAndAdd(request:ILoadExternalObjectsRequest): Observable<any> {
    const formData = new FormData();
    
    // Append each property to the FormData object
    formData.append('file', request.file);

    if(request.workspaceId!=undefined){
      formData.append('workspaceId', String(request.workspaceId));
    }
    formData.append('visibleType', request.visibleType);
    formData.append('changingType', request.changingType);
    formData.append('title', request.title);
    formData.append('description', request.description);

    return this.http.post(this.controllerUrl + "load_and_add",formData);
  }
  getCollection(request:IGetExternalObjectsPacksRequest): Observable<any> {
    return this.http.post(this.controllerUrl + "get_collection",request);
  }
  getFull(id:number): Observable<any> {
    return this.http.get(this.controllerUrl + "get_full/"+id);
  }
}
