import { TestBed } from '@angular/core/testing';

import { ClusterizationProfilesService } from './clusterization-profiles.service';

describe('ClusterizationProfilesService', () => {
  let service: ClusterizationProfilesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClusterizationProfilesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
