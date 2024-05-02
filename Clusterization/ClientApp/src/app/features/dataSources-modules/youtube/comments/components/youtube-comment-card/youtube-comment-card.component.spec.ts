import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeCommentCardComponent } from './youtube-comment-card.component';

describe('YoutubeCommentCardComponent', () => {
  let component: YoutubeCommentCardComponent;
  let fixture: ComponentFixture<YoutubeCommentCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YoutubeCommentCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(YoutubeCommentCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
