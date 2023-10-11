import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClusterizationProfileSearchFilterComponent } from './clusterization-profile-search-filter.component';

describe('ClusterizationProfileSearchFilterComponent', () => {
  let component: ClusterizationProfileSearchFilterComponent;
  let fixture: ComponentFixture<ClusterizationProfileSearchFilterComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ClusterizationProfileSearchFilterComponent]
    });
    fixture = TestBed.createComponent(ClusterizationProfileSearchFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
