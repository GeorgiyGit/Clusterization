import { TestBed } from '@angular/core/testing';

import { EmbeddingsLoadingService } from './embeddings-loading.service';

describe('EmbeddingsLoadingService', () => {
  let service: EmbeddingsLoadingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmbeddingsLoadingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
