import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClusterizationProfileAddPageComponent } from './clusterization-profile-add-page.component';

describe('ClusterizationProfileAddPageComponent', () => {
  let component: ClusterizationProfileAddPageComponent;
  let fixture: ComponentFixture<ClusterizationProfileAddPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ClusterizationProfileAddPageComponent]
    });
    fixture = TestBed.createComponent(ClusterizationProfileAddPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
