import { TestBed } from '@angular/core/testing';

import { GeneralClusterizationAlgorithmsService } from './general-clusterization-algorithms.service';

describe('GeneralClusterizationAlgorithmsService', () => {
  let service: GeneralClusterizationAlgorithmsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GeneralClusterizationAlgorithmsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
