import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmbeddingLoadingStateCardComponent } from './embedding-loading-state-card.component';

describe('EmbeddingLoadingStateCardComponent', () => {
  let component: EmbeddingLoadingStateCardComponent;
  let fixture: ComponentFixture<EmbeddingLoadingStateCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmbeddingLoadingStateCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmbeddingLoadingStateCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
