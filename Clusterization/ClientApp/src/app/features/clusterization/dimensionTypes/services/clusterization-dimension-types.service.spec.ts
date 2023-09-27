import { TestBed } from '@angular/core/testing';

import { ClusterizationDimensionTypesService } from './clusterization-dimension-types.service';

describe('ClusterizationDimensionTypesService', () => {
  let service: ClusterizationDimensionTypesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClusterizationDimensionTypesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
