import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IFastClusteringInitialRequest } from '../models/requests/fast-clustering-initial-request';
import { IFastClusteringProcessRequest } from '../models/requests/fast-clustering-process-request';
import { IPageParameters } from 'src/app/core/models/page-parameters';
import { ISimpleClusterizationWorkspace } from '../../workspace/models/responses/simpleClusterizationWorkspace';
import { IQuotasCalculation } from 'src/app/features/shared-module/quotas/models/responses/quotas-calculation';
import { IFullFastClustering } from '../models/requests/full-fast-clustering-request';

@Injectable({
  providedIn: 'root'
})
export class FastClusteringService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "FastClustering/";
  }

  createWorkflow(): Observable<any> {
    return this.http.post(this.controllerUrl + "create_workflow/",null);
  }

  initializeWorkspace(request:IFastClusteringInitialRequest): Observable<number> {
    return this.http.post<number>(this.controllerUrl + "initialize_workspace/",request);
  }

  initializeProfile(request:IFastClusteringProcessRequest): Observable<number> {
    return this.http.post<number>(this.controllerUrl + "initialize_profile/",request);
  }

  fullFastClustering(request:IFullFastClustering): Observable<number> {
    return this.http.post<number>(this.controllerUrl + "full/",request);
  }

  getWorkspaces(pageParameters:IPageParameters): Observable<ISimpleClusterizationWorkspace[]> {
    return this.http.post<ISimpleClusterizationWorkspace[]>(this.controllerUrl + "get_workspaces/",pageParameters);
  }

  getWorkflowId(): Observable<number> {
    return this.http.get<number>(this.controllerUrl + "get_workflow_id/");
  }

  calculateInitialProfileQuotas(request:IFastClusteringProcessRequest): Observable<IQuotasCalculation[]> {
    return this.http.post<IQuotasCalculation[]>(this.controllerUrl + "calculate_profile_initialize_quotas/",request);
  }

  calculateFullFastClusteringQuotas(request:IFullFastClustering): Observable<IQuotasCalculation[]> {
    return this.http.post<IQuotasCalculation[]>(this.controllerUrl + "calculate_full_fast_clustering_quotas/",request);
  }
}
