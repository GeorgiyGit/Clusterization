import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocTelegramAddDataObjectsMainPageComponent } from './doc-telegram-add-data-objects-main-page.component';

describe('DocTelegramAddDataObjectsMainPageComponent', () => {
  let component: DocTelegramAddDataObjectsMainPageComponent;
  let fixture: ComponentFixture<DocTelegramAddDataObjectsMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocTelegramAddDataObjectsMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocTelegramAddDataObjectsMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
