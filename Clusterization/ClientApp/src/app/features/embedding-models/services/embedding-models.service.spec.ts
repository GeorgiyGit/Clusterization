import { TestBed } from '@angular/core/testing';

import { EmbeddingModelsService } from './embedding-models.service';

describe('EmbeddingModelsService', () => {
  let service: EmbeddingModelsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmbeddingModelsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
