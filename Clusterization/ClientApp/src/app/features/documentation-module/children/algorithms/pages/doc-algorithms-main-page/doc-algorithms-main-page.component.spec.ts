import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocAlgorithmsMainPageComponent } from './doc-algorithms-main-page.component';

describe('DocAlgorithmsMainPageComponent', () => {
  let component: DocAlgorithmsMainPageComponent;
  let fixture: ComponentFixture<DocAlgorithmsMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocAlgorithmsMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocAlgorithmsMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
