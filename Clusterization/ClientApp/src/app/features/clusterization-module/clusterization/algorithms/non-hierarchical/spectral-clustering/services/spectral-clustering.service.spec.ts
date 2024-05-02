import { TestBed } from '@angular/core/testing';

import { SpectralClusteringService } from './spectral-clustering.service';

describe('SpectralClusteringService', () => {
  let service: SpectralClusteringService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SpectralClusteringService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
