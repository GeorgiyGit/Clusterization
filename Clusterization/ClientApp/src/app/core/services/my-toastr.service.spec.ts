import { TestBed } from '@angular/core/testing';

import { MyToastrService } from './my-toastr.service';

describe('MyToastrService', () => {
  let service: MyToastrService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MyToastrService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
