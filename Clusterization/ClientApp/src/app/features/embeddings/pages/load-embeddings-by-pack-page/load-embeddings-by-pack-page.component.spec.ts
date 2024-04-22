import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadEmbeddingsByPackPageComponent } from './load-embeddings-by-pack-page.component';

describe('LoadEmbeddingsByPackPageComponent', () => {
  let component: LoadEmbeddingsByPackPageComponent;
  let fixture: ComponentFixture<LoadEmbeddingsByPackPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoadEmbeddingsByPackPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LoadEmbeddingsByPackPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
