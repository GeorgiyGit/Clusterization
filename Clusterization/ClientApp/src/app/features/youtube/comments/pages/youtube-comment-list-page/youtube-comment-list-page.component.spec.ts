import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeCommentListPageComponent } from './youtube-comment-list-page.component';

describe('YoutubeCommentListPageComponent', () => {
  let component: YoutubeCommentListPageComponent;
  let fixture: ComponentFixture<YoutubeCommentListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YoutubeCommentListPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YoutubeCommentListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
