import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeLoadNewChannelPageComponent } from './youtube-load-new-channel-page.component';

describe('YoutubeLoadNewChannelPageComponent', () => {
  let component: YoutubeLoadNewChannelPageComponent;
  let fixture: ComponentFixture<YoutubeLoadNewChannelPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YoutubeLoadNewChannelPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YoutubeLoadNewChannelPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
