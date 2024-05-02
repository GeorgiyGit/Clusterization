import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClusterizationProfileListComponent } from './clusterization-profile-list.component';

describe('ClusterizationProfileListComponent', () => {
  let component: ClusterizationProfileListComponent;
  let fixture: ComponentFixture<ClusterizationProfileListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ClusterizationProfileListComponent]
    });
    fixture = TestBed.createComponent(ClusterizationProfileListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
