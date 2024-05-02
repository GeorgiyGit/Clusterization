import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeVideoListComponent } from './youtube-video-list.component';

describe('YoutubeVideoListComponent', () => {
  let component: YoutubeVideoListComponent;
  let fixture: ComponentFixture<YoutubeVideoListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YoutubeVideoListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YoutubeVideoListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
