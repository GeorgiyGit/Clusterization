import { TestBed } from '@angular/core/testing';

import { TelegramRepliesService } from './telegram-replies.service';

describe('TelegramRepliesService', () => {
  let service: TelegramRepliesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TelegramRepliesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
