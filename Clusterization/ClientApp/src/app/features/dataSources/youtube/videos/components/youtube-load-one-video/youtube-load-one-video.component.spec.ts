import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeLoadOneVideoComponent } from './youtube-load-one-video.component';

describe('YoutubeLoadOneVideoComponent', () => {
  let component: YoutubeLoadOneVideoComponent;
  let fixture: ComponentFixture<YoutubeLoadOneVideoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [YoutubeLoadOneVideoComponent]
    });
    fixture = TestBed.createComponent(YoutubeLoadOneVideoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
