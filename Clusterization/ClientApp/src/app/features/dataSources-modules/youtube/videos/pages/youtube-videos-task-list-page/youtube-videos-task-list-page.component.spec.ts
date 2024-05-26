import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeVideosTaskListPageComponent } from './youtube-videos-task-list-page.component';

describe('YoutubeVideosTaskListPageComponent', () => {
  let component: YoutubeVideosTaskListPageComponent;
  let fixture: ComponentFixture<YoutubeVideosTaskListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [YoutubeVideosTaskListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(YoutubeVideosTaskListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
