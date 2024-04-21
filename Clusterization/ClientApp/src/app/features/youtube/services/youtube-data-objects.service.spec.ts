import { TestBed } from '@angular/core/testing';

import { YoutubeDataObjectsService } from './youtube-data-objects.service';

describe('YoutubeDataObjectsService', () => {
  let service: YoutubeDataObjectsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(YoutubeDataObjectsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
