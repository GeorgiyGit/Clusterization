import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocEmbeddingsMainPageComponent } from './doc-embeddings-main-page.component';

describe('DocEmbeddingsMainPageComponent', () => {
  let component: DocEmbeddingsMainPageComponent;
  let fixture: ComponentFixture<DocEmbeddingsMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocEmbeddingsMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocEmbeddingsMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
