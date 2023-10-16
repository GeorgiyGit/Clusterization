import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { IAddClusterizationWorkspace } from '../models/addClusterizationWorkspace';
import { IUpdateClusterizationWorkspace } from '../models/updateClusterizationWorkspace';
import { IAddCommentsToWorkspaceByChannelRequest } from '../models/requests/addCommentsToWorkspaceByChannel';
import { IAddCommentsToWorkspaceByVideosRequest } from '../models/requests/addCommentsToWorkspaceByVideos';
import { IClusterizationWorkspace } from '../models/clusterizationWorkspace';
import { IGetWorkspacesRequest } from '../models/requests/getWorkspacesRequest';
import { ISimpleClusterizationWorkspace } from '../models/simpleClusterizationWorkspace';

@Injectable({
  providedIn: 'root'
})
export class ClusterizationWorkspaceService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "clusterizationWorkspaces/";
  }

  add(model:IAddClusterizationWorkspace): Observable<any> {
    return this.http.post(this.controllerUrl + "add",model);
  }
  update(model:IUpdateClusterizationWorkspace): Observable<any> {
    return this.http.put(this.controllerUrl + "update",model);
  }

  addCommentsByChannel(request:IAddCommentsToWorkspaceByChannelRequest): Observable<any> {
    return this.http.post(this.controllerUrl + "add_comments_by_channel",request);
  }
  addCommentsByVideos(request:IAddCommentsToWorkspaceByVideosRequest): Observable<any> {
    return this.http.post(this.controllerUrl + "add_comments_by_videos",request);
  }

  getById(id:number): Observable<IClusterizationWorkspace> {
    return this.http.get<IClusterizationWorkspace>(this.controllerUrl + "get_by_id/"+id);
  }
  getWorkspaces(request:IGetWorkspacesRequest): Observable<ISimpleClusterizationWorkspace[]> {
    return this.http.post<ISimpleClusterizationWorkspace[]>(this.controllerUrl + "get_collection",request);
  }

  embeddingData(id:number): Observable<any> {
    return this.http.post(this.controllerUrl + "load_embedding_data/"+id,null);
  }
}
