import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClusterizationAlgorithmTypesSelectComponent } from './clusterization-algorithm-types-select.component';

describe('ClusterizationAlgorithmTypesSelectComponent', () => {
  let component: ClusterizationAlgorithmTypesSelectComponent;
  let fixture: ComponentFixture<ClusterizationAlgorithmTypesSelectComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ClusterizationAlgorithmTypesSelectComponent]
    });
    fixture = TestBed.createComponent(ClusterizationAlgorithmTypesSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
