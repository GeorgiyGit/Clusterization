import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IAbstractAlgorithm } from '../models/abstractAlgorithm';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IPageParameters } from 'src/app/core/models/page-parameters';

@Injectable({
  providedIn: 'root'
})
export class GeneralClusterizationAlgorithmsService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "generalClusterizationAlgorithms/";
  }

  getAlgorithms(typeId: string, pageParameters:IPageParameters): Observable<IAbstractAlgorithm[]> {
    return this.http.post<IAbstractAlgorithm[]>(this.controllerUrl + "get_collection/",{
      typeId:typeId,
      pageParameters:pageParameters
    });
  }
  getAllAlgorithms(typeId: string): Observable<IAbstractAlgorithm[]> {
    return this.http.get<IAbstractAlgorithm[]>(this.controllerUrl + "get_all/" + typeId);
  }
  calculateQuotasCount(algorithmTypeId: string, entitiesCount: number, dimensionCount: number): Observable<number> {
    return this.http.post<number>(this.controllerUrl + "calculate_quotas_count/", {
      algorithmTypeId: algorithmTypeId,
      entitiesCount: entitiesCount,
      dimensionCount: dimensionCount
    });
  }
  calculateQuotasCountByWorkspace(algorithmTypeId: string, workspaceId: number, dimensionCount: number): Observable<number> {
    return this.http.post<number>(this.controllerUrl + "calculate_quotas_count_by_workspace/", {
      algorithmTypeId: algorithmTypeId,
      workspaceId: workspaceId,
      dimensionCount: dimensionCount
    });
  }
}
