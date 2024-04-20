import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { IEmbeddingModel } from '../models/embedding-model';

@Injectable({
  providedIn: 'root'
})
export class EmbeddingModelsService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "EmbeddingModels/";
  }

  getAll(): Observable<IEmbeddingModel[]> {
    return this.http.get<IEmbeddingModel[]>(this.controllerUrl + "get_all/");
  }
}
