import { TestBed } from '@angular/core/testing';

import { EntitiesTasksService } from './entities-tasks.service';

describe('EntitiesTasksService', () => {
  let service: EntitiesTasksService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EntitiesTasksService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
