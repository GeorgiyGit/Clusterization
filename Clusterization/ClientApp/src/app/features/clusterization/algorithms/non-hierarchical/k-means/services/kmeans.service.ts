import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IAddKMeansAlgorithm } from '../models/addKMeansAlgorithm';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class KMeansService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "kMeansAlgorithm/";
  }

  add(model:IAddKMeansAlgorithm): Observable<any> {
    return this.http.post(this.controllerUrl,model);
  }
}
