import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocProfilesCreationPageComponent } from './doc-profiles-creation-page.component';

describe('DocProfilesCreationPageComponent', () => {
  let component: DocProfilesCreationPageComponent;
  let fixture: ComponentFixture<DocProfilesCreationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocProfilesCreationPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocProfilesCreationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
