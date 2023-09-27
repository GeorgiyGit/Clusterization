import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeChannelListComponent } from './youtube-channel-list.component';

describe('YoutubeChannelListComponent', () => {
  let component: YoutubeChannelListComponent;
  let fixture: ComponentFixture<YoutubeChannelListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YoutubeChannelListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YoutubeChannelListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
