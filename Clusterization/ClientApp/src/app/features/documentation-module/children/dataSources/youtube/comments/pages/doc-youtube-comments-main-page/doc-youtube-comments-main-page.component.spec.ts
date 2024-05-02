import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocYoutubeCommentsMainPageComponent } from './doc-youtube-comments-main-page.component';

describe('DocYoutubeCommentsMainPageComponent', () => {
  let component: DocYoutubeCommentsMainPageComponent;
  let fixture: ComponentFixture<DocYoutubeCommentsMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocYoutubeCommentsMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocYoutubeCommentsMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
