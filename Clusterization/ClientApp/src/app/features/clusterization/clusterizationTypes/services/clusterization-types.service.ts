import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IClusterizationType } from '../models/clusterization-type';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ClusterizationTypesService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "clusterizationTypes/";
  }

  getAll(): Observable<IClusterizationType[]> {
    return this.http.get<IClusterizationType[]>(this.controllerUrl + "get_all/");
  }
}
