import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClusterizationProfileListPageComponent } from './clusterization-profile-list-page.component';

describe('ClusterizationProfileListPageComponent', () => {
  let component: ClusterizationProfileListPageComponent;
  let fixture: ComponentFixture<ClusterizationProfileListPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ClusterizationProfileListPageComponent]
    });
    fixture = TestBed.createComponent(ClusterizationProfileListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
