import { TestBed } from '@angular/core/testing';

import { ClusterizationTypesService } from './clusterization-types.service';

describe('ClusterizationTypesService', () => {
  let service: ClusterizationTypesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClusterizationTypesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
