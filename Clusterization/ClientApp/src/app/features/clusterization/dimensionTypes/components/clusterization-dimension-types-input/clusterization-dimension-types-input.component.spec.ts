import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClusterizationDimensionTypesInputComponent } from './clusterization-dimension-types-input.component';

describe('ClusterizationDimensionTypesInputComponent', () => {
  let component: ClusterizationDimensionTypesInputComponent;
  let fixture: ComponentFixture<ClusterizationDimensionTypesInputComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ClusterizationDimensionTypesInputComponent]
    });
    fixture = TestBed.createComponent(ClusterizationDimensionTypesInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
