import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddKMeansAlgorithmComponent } from './add-k-means-algorithm.component';

describe('AddKMeansAlgorithmComponent', () => {
  let component: AddKMeansAlgorithmComponent;
  let fixture: ComponentFixture<AddKMeansAlgorithmComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddKMeansAlgorithmComponent]
    });
    fixture = TestBed.createComponent(AddKMeansAlgorithmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
