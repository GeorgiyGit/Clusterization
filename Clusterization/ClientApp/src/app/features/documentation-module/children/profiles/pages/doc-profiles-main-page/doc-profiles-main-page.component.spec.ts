import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocProfilesMainPageComponent } from './doc-profiles-main-page.component';

describe('DocProfilesMainPageComponent', () => {
  let component: DocProfilesMainPageComponent;
  let fixture: ComponentFixture<DocProfilesMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocProfilesMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocProfilesMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
