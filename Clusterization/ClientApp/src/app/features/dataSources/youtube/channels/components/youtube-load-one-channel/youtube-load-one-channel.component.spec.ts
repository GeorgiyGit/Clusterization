import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeLoadOneChannelComponent } from './youtube-load-one-channel.component';

describe('YoutubeLoadOneChannelComponent', () => {
  let component: YoutubeLoadOneChannelComponent;
  let fixture: ComponentFixture<YoutubeLoadOneChannelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YoutubeLoadOneChannelComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YoutubeLoadOneChannelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
