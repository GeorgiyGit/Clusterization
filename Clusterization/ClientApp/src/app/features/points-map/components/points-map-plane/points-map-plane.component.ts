import { AfterViewInit, Component, ElementRef, Input, OnChanges, SimpleChanges, ViewChild } from '@angular/core';
import { IDisplayedPoint } from '../../models/displayed-points';

@Component({
  selector: 'app-points-map-plane',
  templateUrl: './points-map-plane.component.html',
  styleUrls: ['./points-map-plane.component.scss']
})
export class PointsMapPlaneComponent implements AfterViewInit, OnChanges {
  @Input() displayedPoints:IDisplayedPoint[]=[];
  @ViewChild('pointCanvas') canvas: ElementRef;
  private ctx: CanvasRenderingContext2D;


  ngOnChanges(changes: SimpleChanges): void {
    if (changes['displayedPoints'] && !changes['displayedPoints'].firstChange) {
      this.drawPoints();
    }
  }

  ngAfterViewInit(): void {
    this.ctx = this.canvas.nativeElement.getContext('2d');
    
    // You can draw points on the canvas here
    this.drawPoints();
  }

  drawPoints() {
    // Iterate through the displayedPoints array and draw each point on the canvas
    console.log(this.displayedPoints);
    this.displayedPoints.forEach(point => {
      console.log(1);
      this.ctx.fillStyle = 'red'; // Point color
      this.ctx.beginPath();
      this.ctx.arc(point.x*10000, point.y*10000, 5, 0, 2 * Math.PI); // Draw a small circle for the point
      this.ctx.fill();
    });
  }
}
