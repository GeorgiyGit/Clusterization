import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddYoutubeChannelCommentsToWorkspacePageComponent } from './add-youtube-channel-comments-to-workspace-page.component';

describe('AddYoutubeChannelCommentsToWorkspacePageComponent', () => {
  let component: AddYoutubeChannelCommentsToWorkspacePageComponent;
  let fixture: ComponentFixture<AddYoutubeChannelCommentsToWorkspacePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddYoutubeChannelCommentsToWorkspacePageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddYoutubeChannelCommentsToWorkspacePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
