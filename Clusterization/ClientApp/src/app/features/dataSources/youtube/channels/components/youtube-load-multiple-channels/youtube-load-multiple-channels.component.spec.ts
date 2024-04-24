import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeLoadMultipleChannelsComponent } from './youtube-load-multiple-channels.component';

describe('YoutubeLoadMultipleChannelsComponent', () => {
  let component: YoutubeLoadMultipleChannelsComponent;
  let fixture: ComponentFixture<YoutubeLoadMultipleChannelsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YoutubeLoadMultipleChannelsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YoutubeLoadMultipleChannelsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
