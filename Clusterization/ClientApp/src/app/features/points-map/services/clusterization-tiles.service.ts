import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { IDisplayedPoint } from '../models/displayed-points';

@Injectable({
  providedIn: 'root'
})
export class ClusterizationTilesService {
  controllerUrl: string;

  constructor(private http: HttpClient) {
    this.controllerUrl = environment.apiUrl + "ClusterizationTiles/";
  }

  getDisplayedPointsByTileId(tileId: string): Observable<IDisplayedPoint[]> {
    return this.http.get<IDisplayedPoint[]>(this.controllerUrl + "get_displayed_points_by_tileId/" + tileId);
  }

  getTileDisplayedPointsByProfileId(profileId: number, x: number, y: number, z: number): Observable<IDisplayedPoint[]> {
    return this.http.post<IDisplayedPoint[]>(this.controllerUrl + "get_tile_displayed_points_by_profileid/", {
      profileId: profileId,
      x: x,
      y: y,
      z: z
    });
  }
}
