import { Injectable } from '@angular/core';
import { ISimpleAlgorithmType } from '../models/simpleAlgorithmType';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ClusterizationAlgorithmTypesService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "clusterizationAlgorithmTypes/";
  }

  getAll(): Observable<ISimpleAlgorithmType[]> {
    return this.http.get<ISimpleAlgorithmType[]>(this.controllerUrl + "get_all/");
  }
}
