import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IDisplayedPoint } from '../../models/displayed-points';
import { ClusterizationTilesService } from '../../services/clusterization-tiles.service';
import { IClusterizationTile } from '../../models/clusterization-tile';
import { IMyPosition } from '../../models/my-position';
import { IClusterizationTilesLevel } from '../../models/clusterization-tiles-level';
import { PointsMapPlaneComponent } from '../../components/points-map-plane/points-map-plane.component';

@Component({
  selector: 'app-points-map-page',
  templateUrl: './points-map-page.component.html',
  styleUrls: ['./points-map-page.component.scss']
})
export class PointsMapPageComponent implements OnInit {
  tiles: IClusterizationTile[][] = [];

  loadingMatrix: boolean[][] = [];

  layerValue: number = 200;

  tilesLevel: IClusterizationTilesLevel;

  level: number = 0;

  profileId: number;

  allowedClusterIds: number[] = [];

  constructor(private tilesService: ClusterizationTilesService,
    private route: ActivatedRoute,
    private toastr: MyToastrService) { }

  ngOnInit(): void {
    this.profileId = this.route.snapshot.params['profileId'];

    this.loadTilesLevel(this.level);
  }

  addLayerValue() {
    this.layerValue+=1;
  }
  reduceLayerValue() {
    if(this.layerValue<10)return;
    this.layerValue-=1;
  }
  centralize() {
    this.layerValue = 50;
  }

  addTile(position: IMyPosition) {
    if (this.tiles[position.y] == undefined) this.tiles[position.y] = [];
    if (this.tiles[position.y][position.x] != undefined) return;

    if (this.loadingMatrix[position.y] == undefined) this.loadingMatrix[position.y] = [];
    if (this.loadingMatrix[position.y][position.x] == true) return;

    this.loadingMatrix[position.y][position.x] == true;

    this.tilesService.getOneTileByProfile(this.profileId, position.x, position.y, this.level, this.allowedClusterIds).subscribe(res => {
      if (this.tiles[position.y] == undefined) this.tiles[position.y] = [];
      this.tiles[position.y][position.x] = res;
      this.filterChild.calculateDisplayedPoints();
      this.filterChild.drawPoints();
    }, error => {
      this.toastr.error(error.error.Message);
    })
  }

  addManyTiles(points: IMyPosition[]) {
    points.forEach(point => {
      if (this.tiles[point.y] == undefined) this.tiles[point.y] = [];
      if (this.tiles[point.y][point.x] != undefined) return;
    });

    points.forEach(point => {
      if (this.loadingMatrix[point.y][point.x] == true) return;
      else this.loadingMatrix[point.y][point.x] == true;
    });

    this.tilesService.getTileCollection(this.profileId, this.level, points, this.allowedClusterIds).subscribe(res => {
      let newPoints: IDisplayedPoint[] = [];
      res.forEach(tile => {
        this.tiles[tile.y][tile.x] = tile;
        newPoints = newPoints.concat(tile.points);
      });
      this.filterChild.calculateDisplayedPoints();
      this.filterChild.drawPoints();
    }, error => {
      this.toastr.error(error.error.Message);
    })
  }

  loadTilesLevel(z: number) {
    this.tilesService.getTilesLevelByProfile(this.profileId, z).subscribe(res => {
      for (let i = 0; i < res.tileCount; i++) {
        this.loadingMatrix[i] = [];
        this.tiles[i] = [];
      }
      this.tilesLevel = res;
    }, error => {
      this.toastr.error(error.error.Message);
    })
  }

  handleMouseWheel(event: WheelEvent) {
    event.preventDefault();

    if (-event.deltaY > 0) {
      this.layerValue += -event.deltaY / 100;
    }
    else {
      if(this.layerValue<10)return;
      this.layerValue += -event.deltaY / 100;
    }
  }

  @ViewChild(PointsMapPlaneComponent) filterChild: PointsMapPlaneComponent;
  downloadImage() {
    this.filterChild.saveCanvas();
  }

  addAllowedClusterId(id: number) {
    this.allowedClusterIds.push(id);
    this.loadTilesLevel(this.level);
  }
  removeAllowedClusterId(id: number) {
    this.allowedClusterIds = this.allowedClusterIds.filter(e => e != id);
    this.loadTilesLevel(this.level);
  }
}
