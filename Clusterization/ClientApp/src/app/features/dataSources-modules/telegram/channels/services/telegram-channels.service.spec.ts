import { TestBed } from '@angular/core/testing';

import { TelegramChannelsService } from './telegram-channels.service';

describe('TelegramChannelsService', () => {
  let service: TelegramChannelsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TelegramChannelsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
