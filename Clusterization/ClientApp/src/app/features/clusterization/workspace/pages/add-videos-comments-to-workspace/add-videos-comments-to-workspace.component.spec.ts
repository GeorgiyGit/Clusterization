import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddVideosCommentsToWorkspaceComponent } from './add-videos-comments-to-workspace.component';

describe('AddVideosCommentsToWorkspaceComponent', () => {
  let component: AddVideosCommentsToWorkspaceComponent;
  let fixture: ComponentFixture<AddVideosCommentsToWorkspaceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddVideosCommentsToWorkspaceComponent]
    });
    fixture = TestBed.createComponent(AddVideosCommentsToWorkspaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
