import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocExternalObjectsPacksMainPageComponent } from './doc-external-objects-packs-main-page.component';

describe('DocExternalObjectsPacksMainPageComponent', () => {
  let component: DocExternalObjectsPacksMainPageComponent;
  let fixture: ComponentFixture<DocExternalObjectsPacksMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocExternalObjectsPacksMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocExternalObjectsPacksMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
