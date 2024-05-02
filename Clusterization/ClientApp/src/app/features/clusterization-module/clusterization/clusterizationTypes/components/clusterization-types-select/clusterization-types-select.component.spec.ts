import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClusterizationTypesSelectComponent } from './clusterization-types-select.component';

describe('ClusterizationTypesSelectComponent', () => {
  let component: ClusterizationTypesSelectComponent;
  let fixture: ComponentFixture<ClusterizationTypesSelectComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ClusterizationTypesSelectComponent]
    });
    fixture = TestBed.createComponent(ClusterizationTypesSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
