import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocYoutubeChannelsDisplayingPageComponent } from './doc-youtube-channels-displaying-page.component';

describe('DocYoutubeChannelsDisplayingPageComponent', () => {
  let component: DocYoutubeChannelsDisplayingPageComponent;
  let fixture: ComponentFixture<DocYoutubeChannelsDisplayingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocYoutubeChannelsDisplayingPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocYoutubeChannelsDisplayingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
