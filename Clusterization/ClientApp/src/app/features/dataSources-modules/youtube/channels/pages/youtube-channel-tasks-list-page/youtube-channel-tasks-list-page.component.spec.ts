import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeChannelTasksListPageComponent } from './youtube-channel-tasks-list-page.component';

describe('YoutubeChannelTasksListPageComponent', () => {
  let component: YoutubeChannelTasksListPageComponent;
  let fixture: ComponentFixture<YoutubeChannelTasksListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [YoutubeChannelTasksListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(YoutubeChannelTasksListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
