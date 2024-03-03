import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocYoutubeVideosDisplayingPageComponent } from './doc-youtube-videos-displaying-page.component';

describe('DocYoutubeVideosDisplayingPageComponent', () => {
  let component: DocYoutubeVideosDisplayingPageComponent;
  let fixture: ComponentFixture<DocYoutubeVideosDisplayingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocYoutubeVideosDisplayingPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocYoutubeVideosDisplayingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
