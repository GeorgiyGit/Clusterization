import { TestBed } from '@angular/core/testing';

import { MyDataObjectsService } from './my-data-objects.service';

describe('MyDataObjectsService', () => {
  let service: MyDataObjectsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MyDataObjectsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
