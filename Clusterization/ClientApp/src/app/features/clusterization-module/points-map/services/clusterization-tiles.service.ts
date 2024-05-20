import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { IDisplayedPoint } from '../models/displayed-points';
import { IClusterizationTile } from '../models/clusterization-tile';
import { IClusterizationTilesLevel } from '../models/clusterization-tiles-level';
import { IMyPosition } from '../models/my-position';

@Injectable({
  providedIn: 'root'
})
export class ClusterizationTilesService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "ClusterizationTiles/";
  }

  getOneTileById(tileId: string): Observable<IClusterizationTile> {
    return this.http.get<IClusterizationTile>(this.controllerUrl + "get_tile_by_id/" + tileId);
  }

  getOneTileByProfile(profileId: number, x: number, y: number, z: number, allowedClusterIds: number[]): Observable<IClusterizationTile> {
    return this.http.post<IClusterizationTile>(this.controllerUrl + "get_tile_by_profile/", {
      profileId: profileId,
      x: x,
      y: y,
      z: z,
      allowedClusterIds: allowedClusterIds
    });
  }
  getTileCollection(profileId: number, z: number, points: IMyPosition[], allowedClusterIds: number[]): Observable<IClusterizationTile[]> {
    return this.http.post<IClusterizationTile[]>(this.controllerUrl + "get_tile_collection/", {
      profileId: profileId,
      z: z,
      points: points,
      allowedClusterIds: allowedClusterIds
    });
  }

  getTilesLevelByProfile(profileId: number, x: number): Observable<IClusterizationTilesLevel> {
    return this.http.post<IClusterizationTilesLevel>(this.controllerUrl + "get_tiles_level_by_profile/", {
      profileId: profileId,
      x: x
    });
  }
}
