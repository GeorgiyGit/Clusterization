import { TestBed } from '@angular/core/testing';

import { YoutubeCommentsService } from './youtube-comments.service';

describe('YoutubeCommentsService', () => {
  let service: YoutubeCommentsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(YoutubeCommentsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
