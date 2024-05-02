import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocAlgorithmsSpectralClusteringPageComponent } from './doc-algorithms-spectral-clustering-page.component';

describe('DocAlgorithmsSpectralClusteringPageComponent', () => {
  let component: DocAlgorithmsSpectralClusteringPageComponent;
  let fixture: ComponentFixture<DocAlgorithmsSpectralClusteringPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocAlgorithmsSpectralClusteringPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocAlgorithmsSpectralClusteringPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
