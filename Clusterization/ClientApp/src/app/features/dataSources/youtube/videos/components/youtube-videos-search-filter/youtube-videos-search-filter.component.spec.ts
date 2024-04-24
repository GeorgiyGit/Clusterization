import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeVideosSearchFilterComponent } from './youtube-videos-search-filter.component';

describe('YoutubeVideosSearchFilterComponent', () => {
  let component: YoutubeVideosSearchFilterComponent;
  let fixture: ComponentFixture<YoutubeVideosSearchFilterComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [YoutubeVideosSearchFilterComponent]
    });
    fixture = TestBed.createComponent(YoutubeVideosSearchFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
