import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IAddOneClusterAlgorithm } from '../models/addOneClusterAlgorithm';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OneClusterAlgorithmService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "oneClusterAlgorithm/";
  }

  add(model: IAddOneClusterAlgorithm): Observable<any> {
    return this.http.post(this.controllerUrl, model);
  }
  clusterData(profileId: number): Observable<any> {
    return this.http.post(this.controllerUrl + 'cluster_data/' + profileId, null);
  }
}
