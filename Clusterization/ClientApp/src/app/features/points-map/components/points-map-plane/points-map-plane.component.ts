import { AfterViewInit, Component, EventEmitter, ElementRef, Input, OnChanges, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { IDisplayedPoint } from '../../models/displayed-points';
import { IMyPosition } from '../../models/my-position';
import { IClusterizationTile } from '../../models/clusterization-tile';

@Component({
  selector: 'app-points-map-plane',
  templateUrl: './points-map-plane.component.html',
  styleUrls: ['./points-map-plane.component.scss']
})
export class PointsMapPlaneComponent implements AfterViewInit, OnChanges, OnInit {
  @Input() displayedPoints: IDisplayedPoint[] = [];
  @Input() tiles: IClusterizationTile[][] = [];
  @Input() layerValue: number;
  @Output() addTileEvent = new EventEmitter<IMyPosition>();

  @Input() tileLength: number;
  maxTileLength = 16;

  @ViewChild('pointCanvas') canvas: ElementRef;
  private ctx: CanvasRenderingContext2D;

  constructor(private el: ElementRef) { }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['displayedPoints'] && !changes['displayedPoints'].firstChange) {
      this.tilesCalculation();
      this.drawPoints();
    }
    if (changes['layerValue'] && !changes['layerValue'].firstChange) {
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

    this.tilesCalculation();
    this.drawPoints();
  }

  tilesCalculation() {
    if (this.tileLength == undefined) {
      this.addTileEvent.emit({
        x: 0,
        y: 0
      });
    }
    let xTilesCount = Math.ceil((this.ctx.canvas.width - this.mouseChangesX) / (this.tileLength * this.layerValue));
    let yTilesCount = Math.ceil((this.ctx.canvas.height - this.mouseChangesY) / (this.tileLength * this.layerValue));

    if (xTilesCount > 15) xTilesCount = 15;
    if (yTilesCount > 15) yTilesCount = 15;

    let y = Math.floor(this.mouseChangesX / (this.tileLength * this.layerValue));
    if (y < 0) y = 0;

    let x = Math.floor(this.mouseChangesY / (this.tileLength * this.layerValue));
    if (x < 0) x = 0;

    console.log('tiles', this.tiles);

    console.log(x);
    console.log(y);

    for (; y <= yTilesCount; y++) {
      for (; x <= xTilesCount; x++) {
        if (this.tiles[y] == undefined) {
          this.tiles[y] = [];
        }
        if (this.tiles[y][x] == undefined) {
          this.addTileEvent.emit({
            x: x,
            y: y
          });
        }
      }
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
  //#endregion


  mouseChangesX: number = 0;
  mouseChangesY: number = 0;
  drawPoints() {
    // Iterate through the displayedPoints array and draw each point on the canvas
    this.ctx.clearRect(0, 0, this.ctx.canvas.width, this.ctx.canvas.height);
    this.displayedPoints.forEach(point => {
      this.ctx.fillStyle = point.color; // Point color
      this.ctx.beginPath();
      this.ctx.arc(point.x * this.layerValue + this.mouseChangesX, point.y * this.layerValue + this.mouseChangesY, 2, 0, 2 * Math.PI); // Draw a small circle for the point
      this.ctx.fill();
    });
  }
}
