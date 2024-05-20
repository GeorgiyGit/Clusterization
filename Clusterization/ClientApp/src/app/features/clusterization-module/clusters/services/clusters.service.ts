import { IGetClustersRequest } from './../models/requests/get-clusters-request';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { ICluster } from '../models/responses/cluster';
import { IGetClusterDataRequest } from '../models/requests/get-cluster-data-request';
import { IClusterDataObject } from '../models/responses/cluster-data-object';
import { IClusterData } from '../models/responses/cluster-data';
import { IGetClustersFileRequest } from '../models/requests/get-clusters-file-request';

@Injectable({
  providedIn: 'root'
})
export class ClustersService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "Clusters/";
  }

  getClusters(request: IGetClustersRequest): Observable<ICluster[]> {
    return this.http.post<ICluster[]>(this.controllerUrl + "get_cluster/", request);
  }
  getClusterEntities(request: IGetClusterDataRequest): Observable<IClusterData[]> {
    return this.http.post<IClusterData[]>(this.controllerUrl + "get_cluster_entities/", request);
  }

  getClustersFile(request:IGetClustersFileRequest): Observable<any> {
    return this.http.post(this.controllerUrl + "get_clusters_file/",request, {
      reportProgress: true,
      observe: 'events',
      responseType: 'blob'
    });
  }
}
