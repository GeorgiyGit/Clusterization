import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocProfilesDisplayingPageComponent } from './doc-profiles-displaying-page.component';

describe('DocProfilesDisplayingPageComponent', () => {
  let component: DocProfilesDisplayingPageComponent;
  let fixture: ComponentFixture<DocProfilesDisplayingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocProfilesDisplayingPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocProfilesDisplayingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
