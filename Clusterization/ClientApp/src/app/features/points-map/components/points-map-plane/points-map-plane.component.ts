import { AfterViewInit, Component, EventEmitter, ElementRef, Input, OnChanges, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { IDisplayedPoint } from '../../models/displayed-points';
import { IMyPosition } from '../../models/my-position';
import { IClusterizationTile } from '../../models/clusterization-tile';
import { IClusterizationTilesLevel } from '../../models/clusterization-tiles-level';
import { ClusterizationDisplayedPointsService } from '../../services/clusterization-displayed-points.service';
import { MyToastrService } from 'src/app/core/services/my-toastr.service';

@Component({
  selector: 'app-points-map-plane',
  templateUrl: './points-map-plane.component.html',
  styleUrls: ['./points-map-plane.component.scss']
})
export class PointsMapPlaneComponent implements AfterViewInit, OnChanges, OnInit {
  @Input() displayedPoints: IDisplayedPoint[] = [];
  @Input() tiles: IClusterizationTile[][] = [];
  @Input() loadingMatrix: boolean[][] = [];

  @Input() layerValue: number;
  convertedLayerValue: number;

  @Output() addTileEvent = new EventEmitter<IMyPosition>();
  @Output() addMultipleTilesEvent = new EventEmitter<IMyPosition[]>();

  @Input() tilesLevel: IClusterizationTilesLevel;
  maxTileLength = 16;

  radius: number = 7;

  @ViewChild('pointCanvas') canvas: ElementRef;
  private ctx: CanvasRenderingContext2D;

  constructor(private el: ElementRef,
    private displayedPointsService: ClusterizationDisplayedPointsService,
    private toastr: MyToastrService) { }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['displayedPoints'] && !changes['displayedPoints'].firstChange) {
      this.drawPoints();
    }
    if (changes['layerValue'] && !changes['layerValue'].firstChange) {
      this.convertLayerValue();
      this.tilesCalculation();
      this.drawPoints();
    }
    if (changes['tilesLevel'] && !changes['tilesLevel'].firstChange) {
      this.convertLayerValue();
      this.tilesCalculation();
      this.drawPoints();
    }
  }

  ngOnInit() {
    // Add a mouse down event listener
    this.el.nativeElement.addEventListener('mousedown', this.mouseDownHandler.bind(this));

    // Add a mouse move event listener
    this.el.nativeElement.addEventListener('mousemove', this.mouseMoveHandler.bind(this));

    // Add a mouse up event listener
    this.el.nativeElement.addEventListener('mouseup', this.mouseUpHandler.bind(this));
  }

  ngOnDestroy() {
    // Remove the event listeners when the component is destroyed
    this.el.nativeElement.removeEventListener('mousedown', this.mouseDownHandler);
    this.el.nativeElement.removeEventListener('mousemove', this.mouseMoveHandler);
    this.el.nativeElement.removeEventListener('mouseup', this.mouseUpHandler);
  }

  ngAfterViewInit(): void {
    this.ctx = this.canvas.nativeElement.getContext('2d');
    // You can draw points on the canvas here
    const ratio = window.devicePixelRatio;

    this.ctx.canvas.width = window.innerWidth * ratio;
    this.ctx.canvas.height = window.innerHeight * ratio;
    this.ctx.canvas.style.width = window.innerWidth + 'px';
    this.ctx.canvas.style.height = window.innerHeight + 'px';

    this.convertLayerValue();
    this.tilesCalculation();
    this.drawPoints();
  }

  convertLayerValue() {
    if (this.tilesLevel != null) {
      console.log('layerValue', this.layerValue);
      console.log('tilesLevel', this.tilesLevel)
      this.convertedLayerValue = this.layerValue / this.tilesLevel.tileLength;
      console.log('convertedLayerValue', this.convertedLayerValue);
    }
  }

  tilesCalculation() {
    if (this.tilesLevel == undefined) return;

    let xTilesCount = Math.ceil((this.ctx.canvas.width - this.mouseChangesX) / (this.tilesLevel.tileLength * this.convertedLayerValue));
    let yTilesCount = Math.ceil((this.ctx.canvas.height - this.mouseChangesY) / (this.tilesLevel.tileLength * this.convertedLayerValue));

    if (xTilesCount > 15) xTilesCount = 15;
    if (yTilesCount > 15) yTilesCount = 15;

    let xCord = Math.floor((-this.mouseChangesX) / (this.tilesLevel.tileLength * this.convertedLayerValue));
    if (xCord < 0) xCord = 0;

    let yCord = Math.floor((-this.mouseChangesY) / (this.tilesLevel.tileLength * this.convertedLayerValue));
    if (yCord < 0) yCord = 0;

    //console.log('tiles', this.tiles);
    //console.log('points', this.displayedPoints);
    //console.log('xTilesCount', xTilesCount);
    //console.log('yTilesCount', yTilesCount);
    //console.log('layerValue', this.layerValue);

    //console.log('x',x,this.mouseChangesX);
    //console.log('y',y,this.mouseChangesY);

    //console.log(this.tilesLevel.tileLength);
    //console.log(this.tilesLevel);

    //console.log('canvas.width',this.ctx.canvas.width);
    //console.log('canvas.height',this.ctx.canvas.height);

    var tiles: IMyPosition[] = [];
    for (let y = yCord; y <= yTilesCount; y++) {
      for (let x = xCord; x <= xTilesCount; x++) {
        if (this.tiles[y] == undefined) {
          this.tiles[y] = [];
        }
        if (this.tiles[y][x] == null && this.loadingMatrix[y][x] != true) {
          this.loadingMatrix[y][x] = true;
          tiles.push({ x: x, y: y });
        }
      }
    }

    if (tiles.length > 0) {
      this.addMultipleTilesEvent.emit(tiles);
    }
  }

  //#region  mouse
  isMouseDown: boolean;

  mousePositionX: number;
  mousePositionY: number;

  private mouseDownHandler(event: MouseEvent) {
    this.isMouseDown = true;
    this.mousePositionX = event.pageX;
    this.mousePositionY = event.pageY;
  }

  private mouseMoveHandler(event: MouseEvent) {
    if (this.isMouseDown) {
      //console.log(this.mouseChangesX, this.mouseChangesY);
      //console.log('l', this.tilesLevel.tileLength * this.tilesLevel.tileCount * this.convertedLayerValue);
      //console.log(this.tiles);
      //if ((this.mouseChangesX < 100 || event.pageX - this.mousePositionX < 0) &&
      //(this.mouseChangesX > -this.tilesLevel.tileLength * this.tilesLevel.tileCount * this.convertedLayerValue+300 || event.pageX - this.mousePositionX > 0)) {

      //}
      //if ((this.mouseChangesY < 100 || event.pageY - this.mousePositionY < 0) &&
      //(this.mouseChangesY > -this.tilesLevel.tileLength * this.tilesLevel.tileCount * this.convertedLayerValue+300 || event.pageY - this.mousePositionY > 0)) {

      //}
      this.mouseChangesX += event.pageX - this.mousePositionX;
      this.mouseChangesY += event.pageY - this.mousePositionY;

      this.mousePositionX = event.pageX;
      this.mousePositionY = event.pageY;


      this.tilesCalculation();

      this.drawPoints();
    }
  }

  private mouseUpHandler(event: MouseEvent) {
    this.isMouseDown = false;
  }

  disablePageScrolling() {
    document.body.style.overflow = 'hidden';
  }

  // Event handler to enable page scrolling
  enablePageScrolling() {
    document.body.style.overflow = 'auto';
  }
  //#endregion


  mouseChangesX: number = 0;
  mouseChangesY: number = 0;
  drawPoints() {
    // Iterate through the displayedPoints array and draw each point on the canvas
    this.ctx.clearRect(0, 0, this.ctx.canvas.width, this.ctx.canvas.height);
    this.displayedPoints.forEach(point => {
      this.ctx.fillStyle = point.color; // Point color
      this.ctx.beginPath();

      let xCoordinate = Math.round(point.x * this.convertedLayerValue + this.mouseChangesX + this.convertedLayerValue * Math.abs(this.tilesLevel.minXValue));
      let yCoordinate = Math.round(point.y * this.convertedLayerValue + this.mouseChangesY + this.convertedLayerValue * Math.abs(this.tilesLevel.minYValue));

      this.ctx.arc(xCoordinate, yCoordinate, this.radius, 0, 2 * Math.PI, true); // Draw a small circle for the point
      this.ctx.fill();
    });
    this.ctx.fillStyle = "black"; // Point color
    this.ctx.beginPath();

    let rectX = this.mouseChangesX - 10;
    let rectY = this.mouseChangesY - 10;

    let rectLength = this.convertedLayerValue * this.tilesLevel.tileLength * this.tilesLevel.tileCount + 20;

    this.ctx.strokeRect(rectX, rectY, rectLength, rectLength);
  }

  click(event: MouseEvent) {
    const rect = this.ctx.canvas.getBoundingClientRect();

    let pos = this.getMousePos(event);
    //console.log('x',pos.x);
    //console.log('y',pos.y);
    this.displayedPoints.forEach(point => {
      let xCoordinate = Math.round(point.x * this.convertedLayerValue + this.mouseChangesX + this.convertedLayerValue * Math.abs(this.tilesLevel.minXValue));
      let yCoordinate = Math.round(point.y * this.convertedLayerValue + this.mouseChangesY + this.convertedLayerValue * Math.abs(this.tilesLevel.minYValue));

      //console.log(xCoordinate,yCoordinate);
      if (pos.x >= xCoordinate - this.radius && pos.x <= xCoordinate + this.radius && pos.y >= yCoordinate - this.radius && pos.y <= yCoordinate + this.radius) {
        //console.log(xCoordinate,yCoordinate);
        this.displayedPointsService.getPointValue(point.id).subscribe(res => {
          this.toastr.success(res.value);
          return;
        });
        return;
      }
    });
  }

  getMousePos(event: MouseEvent): IMyPosition {
    var rect = this.ctx.canvas.getBoundingClientRect(), // abs. size of element
      scaleX = this.ctx.canvas.width / rect.width,    // relationship bitmap vs. element for x
      scaleY = this.ctx.canvas.height / rect.height;  // relationship bitmap vs. element for y

    return {
      x: (event.clientX - rect.left) * scaleX,   // scale mouse coordinates after they have
      y: (event.clientY - rect.top) * scaleY     // been adjusted to be relative to element
    }
  }
}
