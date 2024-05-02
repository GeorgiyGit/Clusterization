import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeLoadMultipleVideosComponent } from './youtube-load-multiple-videos.component';

describe('YoutubeLoadMultipleVideosComponent', () => {
  let component: YoutubeLoadMultipleVideosComponent;
  let fixture: ComponentFixture<YoutubeLoadMultipleVideosComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [YoutubeLoadMultipleVideosComponent]
    });
    fixture = TestBed.createComponent(YoutubeLoadMultipleVideosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
