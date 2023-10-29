import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PointsMapPageComponent } from './points-map-page.component';

describe('PointsMapPageComponent', () => {
  let component: PointsMapPageComponent;
  let fixture: ComponentFixture<PointsMapPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PointsMapPageComponent]
    });
    fixture = TestBed.createComponent(PointsMapPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
