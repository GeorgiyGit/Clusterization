import { TestBed } from '@angular/core/testing';

import { MyLocalStorageService } from './my-local-storage.service';

describe('MyLocalStorageService', () => {
  let service: MyLocalStorageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MyLocalStorageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
