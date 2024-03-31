import { TestBed } from '@angular/core/testing';

import { QuotasTypesService } from './quotas-types.service';

describe('QuotasTypesService', () => {
  let service: QuotasTypesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QuotasTypesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
