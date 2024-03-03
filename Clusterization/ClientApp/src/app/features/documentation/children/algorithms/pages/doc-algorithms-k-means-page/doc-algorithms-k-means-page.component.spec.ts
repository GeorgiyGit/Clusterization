import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocAlgorithmsKMeansPageComponent } from './doc-algorithms-k-means-page.component';

describe('DocAlgorithmsKMeansPageComponent', () => {
  let component: DocAlgorithmsKMeansPageComponent;
  let fixture: ComponentFixture<DocAlgorithmsKMeansPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocAlgorithmsKMeansPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocAlgorithmsKMeansPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
