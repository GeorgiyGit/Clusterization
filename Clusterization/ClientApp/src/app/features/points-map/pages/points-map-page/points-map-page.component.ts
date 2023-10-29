import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';
import { IDisplayedPoint } from '../../models/displayed-points';
import { ClusterizationTilesService } from '../../services/clusterization-tiles.service';

@Component({
  selector: 'app-points-map-page',
  templateUrl: './points-map-page.component.html',
  styleUrls: ['./points-map-page.component.scss']
})
export class PointsMapPageComponent implements OnInit{
  points:IDisplayedPoint[]=[];

  layerValue:number=10000;

  constructor(private tilesService:ClusterizationTilesService,
    private route:ActivatedRoute,
    private toastr:MyToastrService){}
  
  ngOnInit(): void {
    let profileId= this.route.snapshot.params['profileId'];
    
    this.tilesService.getTileDisplayedPointsByProfileId(profileId,0,0,0).subscribe(res=>{
      this.points=res;
      console.log(this.points);

      this.tilesService.getTileDisplayedPointsByProfileId(profileId,1,0,0).subscribe(res=>{
        this.points=this.points.concat(res);
      });
    },error=>{
      this.toastr.error(error.error.Message);
    })
  }

  addLayerValue(){
    this.layerValue+=100;
  }
  reduceLayerValue(){
    this.layerValue-=100;
  }
}
