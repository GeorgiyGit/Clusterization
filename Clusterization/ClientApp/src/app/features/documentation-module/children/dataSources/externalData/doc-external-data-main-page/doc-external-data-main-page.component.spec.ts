import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocExternalDataMainPageComponent } from './doc-external-data-main-page.component';

describe('DocExternalDataMainPageComponent', () => {
  let component: DocExternalDataMainPageComponent;
  let fixture: ComponentFixture<DocExternalDataMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocExternalDataMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocExternalDataMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
