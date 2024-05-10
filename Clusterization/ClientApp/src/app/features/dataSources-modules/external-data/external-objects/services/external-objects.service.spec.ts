import { TestBed } from '@angular/core/testing';

import { ExternalObjectsService } from './external-objects.service';

describe('ExternalObjectsService', () => {
  let service: ExternalObjectsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ExternalObjectsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
