import { TestBed } from '@angular/core/testing';

import { FastClusteringService } from './fast-clustering.service';

describe('FastClusteringService', () => {
  let service: FastClusteringService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FastClusteringService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
