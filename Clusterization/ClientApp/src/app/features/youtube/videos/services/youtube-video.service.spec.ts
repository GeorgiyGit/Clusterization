import { TestBed } from '@angular/core/testing';

import { YoutubeVideoService } from './youtube-video.service';

describe('YoutubeVideoService', () => {
  let service: YoutubeVideoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(YoutubeVideoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
