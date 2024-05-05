import { TestBed } from '@angular/core/testing';

import { TelegramRepliesDataObjectsService } from './telegram-replies-data-objects.service';

describe('TelegramRepliesDataObjectsService', () => {
  let service: TelegramRepliesDataObjectsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TelegramRepliesDataObjectsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
