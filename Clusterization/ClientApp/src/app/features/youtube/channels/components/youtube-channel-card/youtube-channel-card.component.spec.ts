import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeChannelCardComponent } from './youtube-channel-card.component';

describe('YoutubeChannelCardComponent', () => {
  let component: YoutubeChannelCardComponent;
  let fixture: ComponentFixture<YoutubeChannelCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YoutubeChannelCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YoutubeChannelCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
