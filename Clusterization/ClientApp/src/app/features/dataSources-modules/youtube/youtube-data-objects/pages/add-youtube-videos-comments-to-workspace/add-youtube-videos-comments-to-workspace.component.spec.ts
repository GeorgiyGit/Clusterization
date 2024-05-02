import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddYoutubeVideosCommentsToWorkspaceComponent } from './add-youtube-videos-comments-to-workspace.component';

describe('AddYoutubeVideosCommentsToWorkspaceComponent', () => {
  let component: AddYoutubeVideosCommentsToWorkspaceComponent;
  let fixture: ComponentFixture<AddYoutubeVideosCommentsToWorkspaceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddYoutubeVideosCommentsToWorkspaceComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddYoutubeVideosCommentsToWorkspaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
