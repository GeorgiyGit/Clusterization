import { TestBed } from '@angular/core/testing';

import { OneClusterAlgorithmService } from './one-cluster-algorithm.service';

describe('OneClusterAlgorithmService', () => {
  let service: OneClusterAlgorithmService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OneClusterAlgorithmService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
