import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmbeddingsLoadingService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "EmbeddingsLoading/";
  }

  loadEmbeddingsByWorkspaceDataPack(packId:number, embeddingModelId:string): Observable<any> {
    return this.http.post(this.controllerUrl + "load_by_pack",{
      packId:packId,
      embeddingModelId:embeddingModelId
    });
  }

  loadEmbeddingsByProfile(profileId:number): Observable<any> {
    return this.http.post(this.controllerUrl + "load_by_profile/"+profileId,null);
  }
}
