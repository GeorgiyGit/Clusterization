import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClusterizationProfileCardComponent } from './clusterization-profile-card.component';

describe('ClusterizationProfileCardComponent', () => {
  let component: ClusterizationProfileCardComponent;
  let fixture: ComponentFixture<ClusterizationProfileCardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ClusterizationProfileCardComponent]
    });
    fixture = TestBed.createComponent(ClusterizationProfileCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
