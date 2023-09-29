import { TestBed } from '@angular/core/testing';

import { ClusterizationWorkspaceService } from './clusterization-workspace.service';

describe('ClusterizationWorkspaceService', () => {
  let service: ClusterizationWorkspaceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ClusterizationWorkspaceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
