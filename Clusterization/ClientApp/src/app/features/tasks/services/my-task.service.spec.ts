import { TestBed } from '@angular/core/testing';

import { MyTaskService } from './my-task.service';

describe('MyTaskService', () => {
  let service: MyTaskService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MyTaskService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
