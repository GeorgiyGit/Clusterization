import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocYoutubeVideosMainPageComponent } from './doc-youtube-videos-main-page.component';

describe('DocYoutubeVideosMainPageComponent', () => {
  let component: DocYoutubeVideosMainPageComponent;
  let fixture: ComponentFixture<DocYoutubeVideosMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocYoutubeVideosMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocYoutubeVideosMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
