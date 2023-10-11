import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IAddClusterizationWorkspace } from '../../workspace/models/addClusterizationWorkspace';
import { IAddClusterizationProfile } from '../models/add-clusterization-profile';
import { IClusterizationProfile } from '../models/clusterization-profile';
import { IGetClusterizationProfilesRequest } from '../models/get-clusterization-profiles-request';
import { ISimpleClusterizationProfile } from '../models/simple-clusterization-profile';

@Injectable({
  providedIn: 'root'
})
export class ClusterizationProfilesService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "clusterizationProfiles/";
  }

  add(model:IAddClusterizationProfile): Observable<any> {
    return this.http.post(this.controllerUrl + "add",model);
  }

  getById(id:number): Observable<IClusterizationProfile> {
    return this.http.get<IClusterizationProfile>(this.controllerUrl + "get_by_id/"+id);
  }

  getCollection(request:IGetClusterizationProfilesRequest): Observable<ISimpleClusterizationProfile[]> {
    return this.http.post<ISimpleClusterizationProfile[]>(this.controllerUrl + "get_collection",request);
  }
}
