import { TestBed } from '@angular/core/testing';

import { YoutubeChannelService } from './youtube-channel.service';

describe('YoutubeChannelService', () => {
  let service: YoutubeChannelService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(YoutubeChannelService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
