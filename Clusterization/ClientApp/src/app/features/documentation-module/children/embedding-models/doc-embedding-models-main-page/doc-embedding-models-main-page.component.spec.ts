import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocEmbeddingModelsMainPageComponent } from './doc-embedding-models-main-page.component';

describe('DocEmbeddingModelsMainPageComponent', () => {
  let component: DocEmbeddingModelsMainPageComponent;
  let fixture: ComponentFixture<DocEmbeddingModelsMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocEmbeddingModelsMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocEmbeddingModelsMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
