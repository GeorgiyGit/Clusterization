import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmbeddingModelsSelectComponent } from './embedding-models-select.component';

describe('EmbeddingModelsSelectComponent', () => {
  let component: EmbeddingModelsSelectComponent;
  let fixture: ComponentFixture<EmbeddingModelsSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmbeddingModelsSelectComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmbeddingModelsSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
