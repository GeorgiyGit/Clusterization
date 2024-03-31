import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IAddClusterizationWorkspace } from '../../workspace/models/addClusterizationWorkspace';
import { IAddClusterizationProfile } from '../models/add-clusterization-profile';
import { IClusterizationProfile } from '../models/clusterization-profile';
import { IGetClusterizationProfilesRequest } from '../models/get-clusterization-profiles-request';
import { ISimpleClusterizationProfile } from '../models/simple-clusterization-profile';
import { ICustomerGetClusterizationProfilesRequest } from '../models/customer-get-profiles-request';

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

  getFullById(id:number): Observable<IClusterizationProfile> {
    return this.http.get<IClusterizationProfile>(this.controllerUrl + "get_full_by_id/"+id);
  }
  getSimpleById(id:number): Observable<ISimpleClusterizationProfile> {
    return this.http.get<ISimpleClusterizationProfile>(this.controllerUrl + "get_simple_by_id/"+id);
  }

  getCollection(request:IGetClusterizationProfilesRequest): Observable<ISimpleClusterizationProfile[]> {
    return this.http.post<ISimpleClusterizationProfile[]>(this.controllerUrl + "get_collection",request);
  }
  getCustomerCollection(request:ICustomerGetClusterizationProfilesRequest): Observable<ISimpleClusterizationProfile[]> {
    return this.http.post<ISimpleClusterizationProfile[]>(this.controllerUrl + "get_customer_collection",request);
  }

  elect(id:number): Observable<any> {
    return this.http.post(this.controllerUrl + "elect/"+id,null);
  }
  unElect(id:number): Observable<any> {
    return this.http.post(this.controllerUrl + "unelect/"+id,null);
  }
}
