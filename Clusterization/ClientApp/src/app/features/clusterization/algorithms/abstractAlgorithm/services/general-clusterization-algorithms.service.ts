import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IAbstractAlgorithm } from '../models/abstractAlgorithm';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GeneralClusterizationAlgorithmsService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "generalClusterizationAlgorithms/";
  }

  getAlgorithms(typeId:string): Observable<IAbstractAlgorithm[]> {
    return this.http.get<IAbstractAlgorithm[]>(this.controllerUrl + "get_all/"+typeId);
  }
}
