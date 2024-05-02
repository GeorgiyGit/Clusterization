import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeCommentListComponent } from './youtube-comment-list.component';

describe('YoutubeCommentListComponent', () => {
  let component: YoutubeCommentListComponent;
  let fixture: ComponentFixture<YoutubeCommentListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YoutubeCommentListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YoutubeCommentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
