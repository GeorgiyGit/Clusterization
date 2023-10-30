import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IDisplayedPoint } from '../../models/displayed-points';
import { ClusterizationTilesService } from '../../services/clusterization-tiles.service';
import { IClusterizationTile } from '../../models/clusterization-tile';
import { IMyPosition } from '../../models/my-position';

@Component({
  selector: 'app-points-map-page',
  templateUrl: './points-map-page.component.html',
  styleUrls: ['./points-map-page.component.scss']
})
export class PointsMapPageComponent implements OnInit {
  tiles: IClusterizationTile[][] = [];
  points: IDisplayedPoint[] = [];
  layerValue: number = 5000;

  tileLength:number;

  level: number = 0;

  profileId: number;

  constructor(private tilesService: ClusterizationTilesService,
    private route: ActivatedRoute,
    private toastr: MyToastrService) { }

  ngOnInit(): void {
    this.profileId = this.route.snapshot.params['profileId'];
  }

  addLayerValue() {
    this.layerValue += 100;
  }
  reduceLayerValue() {
    this.layerValue -= 100;
  }

  addTile(position: IMyPosition) {
    if(this.tiles[position.y] == undefined)this.tiles[position.y]=[];

    if (this.tiles[position.y][position.x] != undefined) return;

    this.tilesService.getTileDisplayedPointsByProfileId(this.profileId, position.x, position.y, this.level).subscribe(res => {
      if (this.tiles[position.y] == undefined) this.tiles[position.y] = [];
      this.tiles[position.y][position.x] = res;

      this.points = this.points.concat(res.points);

      this.tileLength=res.length;
    }, error => {
      this.toastr.error(error.error.Message);
    })
  }
}
