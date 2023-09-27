import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeVideoListPageComponent } from './youtube-video-list-page.component';

describe('YoutubeVideoListPageComponent', () => {
  let component: YoutubeVideoListPageComponent;
  let fixture: ComponentFixture<YoutubeVideoListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YoutubeVideoListPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YoutubeVideoListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
