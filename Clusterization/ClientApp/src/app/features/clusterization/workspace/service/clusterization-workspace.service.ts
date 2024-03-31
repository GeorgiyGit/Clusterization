import { Injectable } from '@angular/core';
import { HttpClient, } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { IAddClusterizationWorkspace } from '../models/addClusterizationWorkspace';
import { IUpdateClusterizationWorkspace } from '../models/updateClusterizationWorkspace';
import { IAddCommentsToWorkspaceByChannelRequest } from '../models/requests/addCommentsToWorkspaceByChannel';
import { IAddCommentsToWorkspaceByVideosRequest } from '../models/requests/addCommentsToWorkspaceByVideos';
import { IClusterizationWorkspace } from '../models/clusterizationWorkspace';
import { IGetWorkspacesRequest } from '../models/requests/getWorkspacesRequest';
import { ISimpleClusterizationWorkspace } from '../models/simpleClusterizationWorkspace';
import { IAddExternalData } from '../models/external-data/add-external-data';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Injectable({
  providedIn: 'root'
})
export class ClusterizationWorkspaceService {
  controllerUrl: string;

  constructor(private http: HttpClient,
    private toastr:MyToastrService) {
    this.controllerUrl = environment.apiUrl + "clusterizationWorkspaces/";
  }

  add(model: IAddClusterizationWorkspace): Observable<any> {
    return this.http.post(this.controllerUrl + "add", model);
  }
  update(model: IUpdateClusterizationWorkspace): Observable<any> {
    return this.http.put(this.controllerUrl + "update", model);
  }

  addCommentsByChannel(request: IAddCommentsToWorkspaceByChannelRequest): Observable<any> {
    return this.http.post(this.controllerUrl + "add_comments_by_channel", request);
  }
  addCommentsByVideos(request: IAddCommentsToWorkspaceByVideosRequest): Observable<any> {
    return this.http.post(this.controllerUrl + "add_comments_by_videos", request);
  }
  addExternalData(data: IAddExternalData): Observable<any> {
    let formData = new FormData();

    formData.append("file", data.file);
    formData.append("workspaceId", data.workspaceId + "");

    return this.http.post(this.controllerUrl + "load_external_data", formData);
  }

  getFullById(id: number): Observable<IClusterizationWorkspace> {
    return this.http.get<IClusterizationWorkspace>(this.controllerUrl + "get_full_by_id/" + id);
  }

  getSimpleById(id: number): Observable<ISimpleClusterizationWorkspace> {
    return this.http.get<ISimpleClusterizationWorkspace>(this.controllerUrl + "get_simple_by_id/" + id);
  }
  getWorkspaces(request: IGetWorkspacesRequest): Observable<ISimpleClusterizationWorkspace[]> {
    return this.http.post<ISimpleClusterizationWorkspace[]>(this.controllerUrl + "get_collection", request);
  }
  getCustomerWorkspaces(request: IGetWorkspacesRequest): Observable<ISimpleClusterizationWorkspace[]> {
    return this.http.post<ISimpleClusterizationWorkspace[]>(this.controllerUrl + "get_customer_collection", request);
  }

  downloadEntitiesFile(id:number): Observable<any> {
    return this.http.get(this.controllerUrl + "get_entities/"+id, {
      reportProgress: true,
      observe: 'events',
      responseType: 'blob'
    });
  }

  embeddingData(id: number): Observable<any> {
    return this.http.post(this.controllerUrl + "load_embedding_data/" + id, null);
  }
}
