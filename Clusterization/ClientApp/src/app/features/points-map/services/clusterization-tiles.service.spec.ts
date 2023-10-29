import { TestBed } from '@angular/core/testing';

import { ClusterizationTilesService } from './clusterization-tiles.service';

describe('ClusterizationTilesService', () => {
  let service: ClusterizationTilesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClusterizationTilesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
