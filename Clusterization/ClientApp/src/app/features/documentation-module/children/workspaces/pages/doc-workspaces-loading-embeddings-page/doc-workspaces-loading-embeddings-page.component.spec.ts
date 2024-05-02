import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocWorkspacesLoadingEmbeddingsPageComponent } from './doc-workspaces-loading-embeddings-page.component';

describe('DocWorkspacesLoadingEmbeddingsPageComponent', () => {
  let component: DocWorkspacesLoadingEmbeddingsPageComponent;
  let fixture: ComponentFixture<DocWorkspacesLoadingEmbeddingsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocWorkspacesLoadingEmbeddingsPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocWorkspacesLoadingEmbeddingsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
