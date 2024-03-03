import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocYoutubeCommentsLoadFromChannelPageComponent } from './doc-youtube-comments-load-from-channel-page.component';

describe('DocYoutubeCommentsLoadFromChannelPageComponent', () => {
  let component: DocYoutubeCommentsLoadFromChannelPageComponent;
  let fixture: ComponentFixture<DocYoutubeCommentsLoadFromChannelPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocYoutubeCommentsLoadFromChannelPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocYoutubeCommentsLoadFromChannelPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
