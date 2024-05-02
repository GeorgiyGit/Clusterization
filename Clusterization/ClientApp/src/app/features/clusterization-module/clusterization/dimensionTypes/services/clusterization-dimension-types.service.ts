import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IClusterizationDimensionType } from '../models/clusterization-dimension-type';

@Injectable({
  providedIn: 'root'
})
export class ClusterizationDimensionTypesService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "DimensionTypes/";
  }

  getAll(): Observable<IClusterizationDimensionType[]> {
    return this.http.get<IClusterizationDimensionType[]>(this.controllerUrl + "get_all/");
  }

  getAllByEmbeddingModel(embeddingModelId:string): Observable<IClusterizationDimensionType[]> {
    return this.http.get<IClusterizationDimensionType[]>(this.controllerUrl + "get_all_in_embedding_model/"+embeddingModelId);
  }
}
