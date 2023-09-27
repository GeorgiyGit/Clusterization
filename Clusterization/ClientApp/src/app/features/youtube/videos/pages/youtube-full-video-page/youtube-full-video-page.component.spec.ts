import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeFullVideoPageComponent } from './youtube-full-video-page.component';

describe('YoutubeFullVideoPageComponent', () => {
  let component: YoutubeFullVideoPageComponent;
  let fixture: ComponentFixture<YoutubeFullVideoPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [YoutubeFullVideoPageComponent]
    });
    fixture = TestBed.createComponent(YoutubeFullVideoPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
