import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IAddDBSCANAlgorithm } from '../models/add-dbscan-algorithm';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DbscanService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "DBSCANAlgorithm/";
  }

  add(model: IAddDBSCANAlgorithm): Observable<any> {
    return this.http.post(this.controllerUrl, model);
  }
  clusterData(profileId: number): Observable<any> {
    return this.http.post(this.controllerUrl + 'cluster_data/' + profileId, null);
  }
}
