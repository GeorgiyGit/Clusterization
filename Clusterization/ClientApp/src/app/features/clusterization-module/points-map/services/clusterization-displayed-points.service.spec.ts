import { TestBed } from '@angular/core/testing';

import { ClusterizationDisplayedPointsService } from './clusterization-displayed-points.service';

describe('ClusterizationDisplayedPointsService', () => {
  let service: ClusterizationDisplayedPointsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClusterizationDisplayedPointsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
