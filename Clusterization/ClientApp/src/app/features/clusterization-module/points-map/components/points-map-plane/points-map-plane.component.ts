import { AfterViewInit, Component, EventEmitter, ElementRef, Input, OnChanges, OnInit, Output, SimpleChanges, ViewChild, Renderer2 } from '@angular/core';
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
  visibleAreaMatrix: boolean[][] = [];
  displayedPoints: IDisplayedPoint[] = [];

  @Input() tiles: IClusterizationTile[][] = [];
  @Input() loadingMatrix: boolean[][] = [];

  @Input() layerValue: number;
  convertedLayerValue: number;

  @Output() addTileEvent = new EventEmitter<IMyPosition>();
  @Output() addMultipleTilesEvent = new EventEmitter<IMyPosition[]>();

  @Input() tilesLevel: IClusterizationTilesLevel;
  maxTileLength = 16;

  radius: number = 7;
  @ViewChild('pointCanvas', { static: true }) canvas: ElementRef;
  private ctx: CanvasRenderingContext2D;

  constructor(private el: ElementRef,
    private displayedPointsService: ClusterizationDisplayedPointsService,
    private toastr: MyToastrService,
    private renderer: Renderer2) { }
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
      for (let y = 0; y < this.tilesLevel.tileCount; y++) {
        this.visibleAreaMatrix[y] = [];
        for (let x = 0; x < this.tilesLevel.tileCount; x++) {
          this.visibleAreaMatrix[y][x] = false;
        }
      }
      this.convertLayerValue();
      this.tilesCalculation();
      this.drawPoints();
    }
  }

  ngOnInit() {
    this.el.nativeElement.addEventListener('mousedown', this.mouseDownHandler.bind(this));

    this.el.nativeElement.addEventListener('mousemove', this.mouseMoveHandler.bind(this));

    this.el.nativeElement.addEventListener('mouseup', this.mouseUpHandler.bind(this));

    this.el.nativeElement.addEventListener('touchstart', this.touchStartHandler.bind(this));
    this.el.nativeElement.addEventListener('touchmove', this.touchMoveHandler.bind(this));
    this.el.nativeElement.addEventListener('touchend', this.touchEndHandler.bind(this));
  }

  ngOnDestroy() {
    this.el.nativeElement.removeEventListener('mousedown', this.mouseDownHandler);
    this.el.nativeElement.removeEventListener('mousemove', this.mouseMoveHandler);
    this.el.nativeElement.removeEventListener('mouseup', this.mouseUpHandler);

    this.el.nativeElement.removeEventListener('touchstart', this.touchStartHandler.bind(this));
    this.el.nativeElement.removeEventListener('touchmove', this.touchMoveHandler.bind(this));
    this.el.nativeElement.removeEventListener('touchend', this.touchEndHandler.bind(this));
  }

  ngAfterViewInit(): void {
    this.ctx = this.canvas.nativeElement.getContext('2d');

    const ratio = window.devicePixelRatio;

    const canvasSize = Math.min(window.innerWidth, window.innerHeight);

    this.ctx.canvas.width = canvasSize * ratio;
    this.ctx.canvas.height = canvasSize * ratio;

    this.ctx.canvas.style.width = canvasSize + 'px';
    this.ctx.canvas.style.height = canvasSize + 'px';

    this.convertLayerValue();
    this.tilesCalculation();
    this.drawPoints();
  }

  convertLayerValue() {
    if (this.tilesLevel != null) {
      this.convertedLayerValue = this.layerValue / this.tilesLevel.tileLength;
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

    let tiles: IMyPosition[] = [];
    let newVisibleAreaMatrix: boolean[][] = [];

    for (let y = 0; y < this.tilesLevel.tileCount; y++) {
      newVisibleAreaMatrix[y] = [];
      for (let x = 0; x < this.tilesLevel.tileCount; x++) {
        newVisibleAreaMatrix[y][x] = false;
      }
    }

    for (let y = yCord; y <= yTilesCount; y++) {
      for (let x = xCord; x <= xTilesCount; x++) {
        if (this.tiles[y] == undefined) {
          this.tiles[y] = [];
        }
        if (this.tiles[y][x] == null && this.loadingMatrix[y][x] != true) {
          this.loadingMatrix[y][x] = true;
          tiles.push({ x: x, y: y });
        }

        newVisibleAreaMatrix[y][x] = true;
      }
    }
    let flag=false;
    for (let y = 0; y < this.tilesLevel.tileCount; y++) {
      for (let x = 0; x < this.tilesLevel.tileCount; x++) {
        if (this.visibleAreaMatrix[y][x] != newVisibleAreaMatrix[y][x]) {
          this.visibleAreaMatrix = newVisibleAreaMatrix;
          this.calculateDisplayedPoints();
          flag=true;
          break;
        }
        if(flag)break;
      }
    }

    if (tiles.length > 0) {
      this.addMultipleTilesEvent.emit(tiles);
    }
  }

  calculateDisplayedPoints() {
    let newDisplayedPoints: IDisplayedPoint[] = [];
    for (let y = 0; y < this.tilesLevel.tileCount; y++) {
      for (let x = 0; x < this.tilesLevel.tileCount; x++) {
        if (this.visibleAreaMatrix[y][x] && this.tiles[y] != null && this.tiles[y][x] != null) {
          newDisplayedPoints.push(...this.tiles[y][x].points);
        }
      }
    }
    this.displayedPoints = newDisplayedPoints;
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


  private touchStartHandler(event: TouchEvent) {
    this.isMouseDown = true;
    this.mouseChangesX = event.touches[0].pageX;
    this.mousePositionY = event.touches[0].pageY;
  }

  private touchMoveHandler(event: TouchEvent) {
    if (this.isMouseDown) {
      const touchPositionX = event.touches[0].pageX;
      const touchPositionY = event.touches[0].pageY;

      this.mouseChangesX += touchPositionX - this.mouseChangesX;
      this.mouseChangesY += touchPositionY - this.mousePositionY;

      this.mouseChangesX = touchPositionX;
      this.mousePositionY = touchPositionY;

      this.tilesCalculation();

      this.drawPoints();
    }
  }

  private touchEndHandler(event: TouchEvent) {
    this.isMouseDown = false;
  }

  disablePageScrolling() {
    document.body.style.overflow = 'hidden';
  }

  enablePageScrolling() {
    document.body.style.overflow = 'auto';
  }
  //#endregion


  mouseChangesX: number = 0;
  mouseChangesY: number = 0;
  drawPoints() {
    if (this.tilesLevel == null) return;
    this.ctx.clearRect(0, 0, this.ctx.canvas.width, this.ctx.canvas.height);
    this.displayedPoints.forEach(point => {
      this.ctx.fillStyle = point.color;

      this.ctx.beginPath();
      let xCoordinate = Math.round(point.x * this.convertedLayerValue + this.mouseChangesX + this.convertedLayerValue * Math.abs(this.tilesLevel.minXValue));
      let yCoordinate = Math.round(point.y * this.convertedLayerValue + this.mouseChangesY + this.convertedLayerValue * Math.abs(this.tilesLevel.minYValue));
      this.ctx.arc(xCoordinate, yCoordinate, this.radius, 0, 2 * Math.PI, true);
      this.ctx.fill();
    });

    this.ctx.fillStyle = "black";
    this.ctx.beginPath();


    let rectX = this.mouseChangesX - 10;
    let rectY = this.mouseChangesY - 10;

    let rectLength = this.convertedLayerValue * this.tilesLevel.tileLength * this.tilesLevel.tileCount + 20;

    this.ctx.strokeRect(rectX, rectY, rectLength, rectLength);
  }

  click(event: MouseEvent) {
    const rect = this.ctx.canvas.getBoundingClientRect();

    let pos = this.getMousePos(event);

    this.displayedPoints.forEach(point => {
      let xCoordinate = Math.round(point.x * this.convertedLayerValue + this.mouseChangesX + this.convertedLayerValue * Math.abs(this.tilesLevel.minXValue));
      let yCoordinate = Math.round(point.y * this.convertedLayerValue + this.mouseChangesY + this.convertedLayerValue * Math.abs(this.tilesLevel.minYValue));

      if (pos.x >= xCoordinate - this.radius && pos.x <= xCoordinate + this.radius && pos.y >= yCoordinate - this.radius && pos.y <= yCoordinate + this.radius) {
        this.displayedPointsService.getPointValue(point.id).subscribe(res => {
          this.toastr.success(res.value);
          return;
        });
        return;
      }
    });
  }

  getMousePos(event: MouseEvent): IMyPosition {
    const canvasRect = this.canvas.nativeElement.getBoundingClientRect();
    const scaleX = this.ctx.canvas.width / this.canvas.nativeElement.clientWidth;
    const scaleY = this.ctx.canvas.height / this.canvas.nativeElement.clientHeight;

    const mouseX = (event.clientX - canvasRect.left) * scaleX;
    const mouseY = (event.clientY - canvasRect.top) * scaleY;

    return {
      x: mouseX,
      y: mouseY
    }
  }

  saveCanvas() {
    const canvas: HTMLCanvasElement = this.canvas.nativeElement;

    let rectX = this.mouseChangesX - 10;
    let rectY = this.mouseChangesY - 10;

    let rectLength = this.convertedLayerValue * this.tilesLevel.tileLength * this.tilesLevel.tileCount + 20;

    const croppedCanvas = document.createElement('canvas');
    croppedCanvas.width = rectLength;
    croppedCanvas.height = rectLength;
    const context = croppedCanvas.getContext('2d', { alpha: false });

    if (context == null) return;
    context.drawImage(canvas, rectX, rectY, rectLength, rectLength, 0, 0, rectLength, rectLength);

    const dataUrl: string = croppedCanvas.toDataURL('image/png');

    const link = document.createElement('a');
    link.href = dataUrl;
    link.download = 'cropped_canvas_image' + this.tilesLevel.id + '.png';

    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }
}
