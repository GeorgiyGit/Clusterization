import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { IDisplayedPoint } from '../models/displayed-points';
import { IClusterizationTile } from '../models/clusterization-tile';

@Injectable({
  providedIn: 'root'
})
export class ClusterizationTilesService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "ClusterizationTiles/";
  }

  getDisplayedPointsByTileId(tileId: string): Observable<IClusterizationTile> {
    return this.http.get<IClusterizationTile>(this.controllerUrl + "get_tile_by_id/" + tileId);
  }

  getTileDisplayedPointsByProfileId(profileId: number, x: number, y: number, z: number): Observable<IClusterizationTile> {
    return this.http.post<IClusterizationTile>(this.controllerUrl + "get_tile_by_profile/", {
      profileId: profileId,
      x: x,
      y: y,
      z: z
    });
  }
}
