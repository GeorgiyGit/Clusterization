import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddOneClusterAlgorithmComponent } from './add-one-cluster-algorithm.component';

describe('AddOneClusterAlgorithmComponent', () => {
  let component: AddOneClusterAlgorithmComponent;
  let fixture: ComponentFixture<AddOneClusterAlgorithmComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddOneClusterAlgorithmComponent]
    });
    fixture = TestBed.createComponent(AddOneClusterAlgorithmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
