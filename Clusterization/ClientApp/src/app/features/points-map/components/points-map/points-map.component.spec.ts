import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PointsMapComponent } from './points-map.component';

describe('PointsMapComponent', () => {
  let component: PointsMapComponent;
  let fixture: ComponentFixture<PointsMapComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PointsMapComponent]
    });
    fixture = TestBed.createComponent(PointsMapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
