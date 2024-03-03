import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocYoutubeVideosLoadingByIdsPageComponent } from './doc-youtube-videos-loading-by-ids-page.component';

describe('DocYoutubeVideosLoadingByIdsPageComponent', () => {
  let component: DocYoutubeVideosLoadingByIdsPageComponent;
  let fixture: ComponentFixture<DocYoutubeVideosLoadingByIdsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocYoutubeVideosLoadingByIdsPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocYoutubeVideosLoadingByIdsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
