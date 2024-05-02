import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IDimensionalityReductionTechnique } from '../models/dimensionalityReductionTechnique';

@Injectable({
  providedIn: 'root'
})
export class DimensionalityReductionTechniquesService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "DimensionalityReductionTechniques/";
  }

  getAll(): Observable<IDimensionalityReductionTechnique[]> {
    return this.http.get<IDimensionalityReductionTechnique[]>(this.controllerUrl + "get_all");
  }
}
