import { TestBed } from '@angular/core/testing';

import { ExternalObjectsPacksService } from './external-objects-packs.service';

describe('ExternalObjectsPacksService', () => {
  let service: ExternalObjectsPacksService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ExternalObjectsPacksService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
