import { TestBed } from '@angular/core/testing';

import { TelegramMessagesService } from './telegram-messages.service';

describe('TelegramMessagesService', () => {
  let service: TelegramMessagesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TelegramMessagesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
