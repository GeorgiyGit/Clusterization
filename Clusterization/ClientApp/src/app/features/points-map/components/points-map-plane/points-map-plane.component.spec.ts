import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PointsMapPlaneComponent } from './points-map-plane.component';

describe('PointsMapPlaneComponent', () => {
  let component: PointsMapPlaneComponent;
  let fixture: ComponentFixture<PointsMapPlaneComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PointsMapPlaneComponent]
    });
    fixture = TestBed.createComponent(PointsMapPlaneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
