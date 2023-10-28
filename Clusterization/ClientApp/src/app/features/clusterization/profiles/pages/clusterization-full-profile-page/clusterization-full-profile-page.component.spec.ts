import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClusterizationFullProfilePageComponent } from './clusterization-full-profile-page.component';

describe('ClusterizationFullProfilePageComponent', () => {
  let component: ClusterizationFullProfilePageComponent;
  let fixture: ComponentFixture<ClusterizationFullProfilePageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ClusterizationFullProfilePageComponent]
    });
    fixture = TestBed.createComponent(ClusterizationFullProfilePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
