import { TestBed } from '@angular/core/testing';

import { GaussianMixtureService } from './gaussian-mixture.service';

describe('GaussianMixtureService', () => {
  let service: GaussianMixtureService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GaussianMixtureService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
