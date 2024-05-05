import { TestBed } from '@angular/core/testing';

import { WTelegramService } from './w-telegram.service';

describe('WTelegramService', () => {
  let service: WTelegramService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WTelegramService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
