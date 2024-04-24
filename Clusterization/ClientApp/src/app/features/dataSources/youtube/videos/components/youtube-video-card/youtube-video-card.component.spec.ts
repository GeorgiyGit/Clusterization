import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeVideoCardComponent } from './youtube-video-card.component';

describe('YoutubeVideoCardComponent', () => {
  let component: YoutubeVideoCardComponent;
  let fixture: ComponentFixture<YoutubeVideoCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YoutubeVideoCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YoutubeVideoCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
