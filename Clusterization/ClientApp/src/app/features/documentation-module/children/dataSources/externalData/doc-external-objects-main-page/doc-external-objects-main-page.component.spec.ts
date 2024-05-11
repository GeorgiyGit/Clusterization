import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocExternalObjectsMainPageComponent } from './doc-external-objects-main-page.component';

describe('DocExternalObjectsMainPageComponent', () => {
  let component: DocExternalObjectsMainPageComponent;
  let fixture: ComponentFixture<DocExternalObjectsMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocExternalObjectsMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocExternalObjectsMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
