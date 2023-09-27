import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeLoadAllVideosPageComponent } from './youtube-load-all-videos-page.component';

describe('YoutubeLoadAllVideosPageComponent', () => {
  let component: YoutubeLoadAllVideosPageComponent;
  let fixture: ComponentFixture<YoutubeLoadAllVideosPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [YoutubeLoadAllVideosPageComponent]
    });
    fixture = TestBed.createComponent(YoutubeLoadAllVideosPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
