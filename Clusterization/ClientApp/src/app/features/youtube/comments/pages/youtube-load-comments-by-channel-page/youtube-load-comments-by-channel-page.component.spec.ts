import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeLoadCommentsByChannelPageComponent } from './youtube-load-comments-by-channel-page.component';

describe('YoutubeLoadCommentsByChannelPageComponent', () => {
  let component: YoutubeLoadCommentsByChannelPageComponent;
  let fixture: ComponentFixture<YoutubeLoadCommentsByChannelPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [YoutubeLoadCommentsByChannelPageComponent]
    });
    fixture = TestBed.createComponent(YoutubeLoadCommentsByChannelPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
