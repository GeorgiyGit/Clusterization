import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeChannelListPageComponent } from './youtube-channel-list-page.component';

describe('YoutubeChannelListPageComponent', () => {
  let component: YoutubeChannelListPageComponent;
  let fixture: ComponentFixture<YoutubeChannelListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YoutubeChannelListPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YoutubeChannelListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
