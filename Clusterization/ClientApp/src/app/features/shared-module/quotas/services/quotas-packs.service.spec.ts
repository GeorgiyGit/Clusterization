import { TestBed } from '@angular/core/testing';

import { QuotasPacksService } from './quotas-packs.service';

describe('QuotasPacksService', () => {
  let service: QuotasPacksService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QuotasPacksService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
