import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocAlgorithmsGaussianMixturePageComponent } from './doc-algorithms-gaussian-mixture-page.component';

describe('DocAlgorithmsGaussianMixturePageComponent', () => {
  let component: DocAlgorithmsGaussianMixturePageComponent;
  let fixture: ComponentFixture<DocAlgorithmsGaussianMixturePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocAlgorithmsGaussianMixturePageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocAlgorithmsGaussianMixturePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
