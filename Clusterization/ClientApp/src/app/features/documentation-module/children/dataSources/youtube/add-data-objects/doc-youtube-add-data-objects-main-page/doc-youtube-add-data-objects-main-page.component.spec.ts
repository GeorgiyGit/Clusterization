import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocYoutubeAddDataObjectsMainPageComponent } from './doc-youtube-add-data-objects-main-page.component';

describe('DocYoutubeAddDataObjectsMainPageComponent', () => {
  let component: DocYoutubeAddDataObjectsMainPageComponent;
  let fixture: ComponentFixture<DocYoutubeAddDataObjectsMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocYoutubeAddDataObjectsMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocYoutubeAddDataObjectsMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
