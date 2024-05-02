import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocYoutubeVideosLoadingManyPageComponent } from './doc-youtube-videos-loading-many-page.component';

describe('DocYoutubeVideosLoadingManyPageComponent', () => {
  let component: DocYoutubeVideosLoadingManyPageComponent;
  let fixture: ComponentFixture<DocYoutubeVideosLoadingManyPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocYoutubeVideosLoadingManyPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocYoutubeVideosLoadingManyPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
