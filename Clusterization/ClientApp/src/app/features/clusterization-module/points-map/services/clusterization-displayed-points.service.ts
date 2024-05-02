import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { IDisplayedPointValue } from '../models/displayed-point-value';

@Injectable({
  providedIn: 'root'
})
export class ClusterizationDisplayedPointsService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "ClusterizationDisplayedPoints/";
  }

  getPointValue(pointId: number): Observable<IDisplayedPointValue> {
    return this.http.get<IDisplayedPointValue>(this.controllerUrl + "get_point_value/" + pointId);
  }
}
