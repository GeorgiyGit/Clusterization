import { TestBed } from '@angular/core/testing';

import { TelegramMessagesDataObjectsService } from './telegram-messages-data-objects.service';

describe('TelegramMessagesDataObjectsService', () => {
  let service: TelegramMessagesDataObjectsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TelegramMessagesDataObjectsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
