import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeFullChannelPageComponent } from './youtube-full-channel-page.component';

describe('YoutubeFullChannelPageComponent', () => {
  let component: YoutubeFullChannelPageComponent;
  let fixture: ComponentFixture<YoutubeFullChannelPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YoutubeFullChannelPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YoutubeFullChannelPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
