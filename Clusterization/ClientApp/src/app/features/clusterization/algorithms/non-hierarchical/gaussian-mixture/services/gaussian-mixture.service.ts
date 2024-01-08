import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

import { Observable } from 'rxjs';
import { IAddGaussianMixtureAlgorithm } from '../models/add-gaussian-mixture-algorithm';

@Injectable({
  providedIn: 'root'
})
export class GaussianMixtureService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "GaussianMixtureAlgorithm/";
  }

  add(model: IAddGaussianMixtureAlgorithm): Observable<any> {
    return this.http.post(this.controllerUrl, model);
  }
  clusterData(profileId: number): Observable<any> {
    return this.http.post(this.controllerUrl + 'cluster_data/' + profileId, null);
  }
}
