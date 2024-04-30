import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocYoutubeCommentsLoadFromVideoPageComponent } from './doc-youtube-comments-load-from-video-page.component';

describe('DocYoutubeCommentsLoadFromVideoPageComponent', () => {
  let component: DocYoutubeCommentsLoadFromVideoPageComponent;
  let fixture: ComponentFixture<DocYoutubeCommentsLoadFromVideoPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocYoutubeCommentsLoadFromVideoPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocYoutubeCommentsLoadFromVideoPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
