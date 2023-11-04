import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DimensionalityReductionTechniquesSelectComponent } from './dimensionality-reduction-techniques-select.component';

describe('DimensionalityReductionTechniquesSelectComponent', () => {
  let component: DimensionalityReductionTechniquesSelectComponent;
  let fixture: ComponentFixture<DimensionalityReductionTechniquesSelectComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DimensionalityReductionTechniquesSelectComponent]
    });
    fixture = TestBed.createComponent(DimensionalityReductionTechniquesSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
