import { TestBed } from '@angular/core/testing';

import { ClusterizationAlgorithmTypesService } from './clusterization-algorithm-types.service';

describe('ClusterizationAlgorithmTypesService', () => {
  let service: ClusterizationAlgorithmTypesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClusterizationAlgorithmTypesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
