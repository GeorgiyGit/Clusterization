import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddChannelCommentsToWorkspacePageComponent } from './add-channel-comments-to-workspace-page.component';

describe('AddChannelCommentsToWorkspacePageComponent', () => {
  let component: AddChannelCommentsToWorkspacePageComponent;
  let fixture: ComponentFixture<AddChannelCommentsToWorkspacePageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddChannelCommentsToWorkspacePageComponent]
    });
    fixture = TestBed.createComponent(AddChannelCommentsToWorkspacePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
