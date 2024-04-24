import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeChannelsSearchFilterComponent } from './youtube-channels-search-filter.component';

describe('YoutubeChannelsSearchFilterComponent', () => {
  let component: YoutubeChannelsSearchFilterComponent;
  let fixture: ComponentFixture<YoutubeChannelsSearchFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YoutubeChannelsSearchFilterComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YoutubeChannelsSearchFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
