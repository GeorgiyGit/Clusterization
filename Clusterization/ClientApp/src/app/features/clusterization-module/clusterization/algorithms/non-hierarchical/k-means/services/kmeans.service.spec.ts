import { TestBed } from '@angular/core/testing';

import { KMeansService } from './kmeans.service';

describe('KMeansService', () => {
  let service: KMeansService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(KMeansService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
