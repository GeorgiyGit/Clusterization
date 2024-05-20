import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YoutubeCommentDataObjectComponent } from './youtube-comment-data-object.component';

describe('YoutubeCommentDataObjectComponent', () => {
  let component: YoutubeCommentDataObjectComponent;
  let fixture: ComponentFixture<YoutubeCommentDataObjectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [YoutubeCommentDataObjectComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(YoutubeCommentDataObjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
