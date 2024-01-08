import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IAddSpectralClusteringAlgorithm } from '../models/add-spectral-clustering-algorithm';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SpectralClusteringService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "SpectralClusteringAlgorithm/";
  }

  add(model: IAddSpectralClusteringAlgorithm): Observable<any> {
    return this.http.post(this.controllerUrl, model);
  }
  clusterData(profileId: number): Observable<any> {
    return this.http.post(this.controllerUrl + 'cluster_data/' + profileId, null);
  }
}
