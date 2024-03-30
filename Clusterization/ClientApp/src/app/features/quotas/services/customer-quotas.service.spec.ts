import { TestBed } from '@angular/core/testing';

import { CustomerQuotasService } from './customer-quotas.service';

describe('CustomerQuotasService', () => {
  let service: CustomerQuotasService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CustomerQuotasService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
