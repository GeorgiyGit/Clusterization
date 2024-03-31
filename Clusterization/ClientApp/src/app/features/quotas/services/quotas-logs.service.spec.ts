import { TestBed } from '@angular/core/testing';

import { QuotasLogsService } from './quotas-logs.service';

describe('QuotasLogsService', () => {
  let service: QuotasLogsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QuotasLogsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
