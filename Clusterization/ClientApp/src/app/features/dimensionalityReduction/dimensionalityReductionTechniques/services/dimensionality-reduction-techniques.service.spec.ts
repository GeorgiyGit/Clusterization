import { TestBed } from '@angular/core/testing';

import { DimensionalityReductionTechniquesService } from './dimensionality-reduction-techniques.service';

describe('DimensionalityReductionTechniquesService', () => {
  let service: DimensionalityReductionTechniquesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DimensionalityReductionTechniquesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
