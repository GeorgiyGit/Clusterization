import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IFullDataObject } from '../models/full-data-object';

@Injectable({
  providedIn: 'root'
})
export class MyDataObjectsService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "MyDataObjects/";
  }

  getFullByDisplayedPointId(pointId:number): Observable<IFullDataObject> {
    return this.http.get<IFullDataObject>(this.controllerUrl + "get_full_by_point/"+pointId);
  }
}
