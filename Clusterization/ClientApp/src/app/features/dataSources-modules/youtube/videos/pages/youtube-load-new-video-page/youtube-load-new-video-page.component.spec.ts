import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeLoadNewVideoPageComponent } from './youtube-load-new-video-page.component';

describe('YoutubeLoadNewVideoPageComponent', () => {
  let component: YoutubeLoadNewVideoPageComponent;
  let fixture: ComponentFixture<YoutubeLoadNewVideoPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [YoutubeLoadNewVideoPageComponent]
    });
    fixture = TestBed.createComponent(YoutubeLoadNewVideoPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
