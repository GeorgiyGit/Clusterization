import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddGaussianMixtureAlgorithmComponent } from './add-gaussian-mixture-algorithm.component';

describe('AddGaussianMixtureAlgorithmComponent', () => {
  let component: AddGaussianMixtureAlgorithmComponent;
  let fixture: ComponentFixture<AddGaussianMixtureAlgorithmComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddGaussianMixtureAlgorithmComponent]
    });
    fixture = TestBed.createComponent(AddGaussianMixtureAlgorithmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
