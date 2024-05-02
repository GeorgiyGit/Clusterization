import { TestBed } from '@angular/core/testing';

import { WorkspaceDataObjectsAddPacksService } from './workspace-data-objects-add-packs.service';

describe('WorkspaceDataObjectsAddPacksService', () => {
  let service: WorkspaceDataObjectsAddPacksService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WorkspaceDataObjectsAddPacksService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
