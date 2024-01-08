import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddSpectralClusteringAlgorithmComponent } from './add-spectral-clustering-algorithm.component';

describe('AddSpectralClusteringAlgorithmComponent', () => {
  let component: AddSpectralClusteringAlgorithmComponent;
  let fixture: ComponentFixture<AddSpectralClusteringAlgorithmComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddSpectralClusteringAlgorithmComponent]
    });
    fixture = TestBed.createComponent(AddSpectralClusteringAlgorithmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
