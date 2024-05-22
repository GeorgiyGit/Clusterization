import { TestBed } from '@angular/core/testing';

import { ModeratorTasksService } from './moderator-tasks.service';

describe('ModeratorTasksService', () => {
  let service: ModeratorTasksService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ModeratorTasksService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
