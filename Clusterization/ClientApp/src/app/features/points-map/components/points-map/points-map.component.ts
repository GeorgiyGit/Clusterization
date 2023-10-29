import { AfterViewInit, Component, ElementRef, Input, ViewChild } from '@angular/core';
import { IDisplayedPoint } from '../../models/displayed-points';

@Component({
  selector: 'app-points-map',
  templateUrl: './points-map.component.html',
  styleUrls: ['./points-map.component.scss']
})
export class PointsMapComponent implements AfterViewInit {
  @Input() displayedPoints:IDisplayedPoint[]=[];
  @ViewChild('pointCanvas') canvas: ElementRef;
  private ctx: CanvasRenderingContext2D;

  ngAfterViewInit(): void {
    this.ctx = this.canvas.nativeElement.getContext('2d');
    
    // You can draw points on the canvas here
    this.drawPoints();
  }

  drawPoints() {
    // Iterate through the displayedPoints array and draw each point on the canvas
    this.displayedPoints.forEach(point => {
      this.ctx.fillStyle = 'red'; // Point color
      this.ctx.beginPath();
      this.ctx.arc(point.x*100, point.y*100, 5, 0, 2 * Math.PI); // Draw a small circle for the point
      this.ctx.fill();
    });
  }


}
