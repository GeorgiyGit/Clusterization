import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddVideoCommentsToWorkspaceComponent } from './add-video-comments-to-workspace.component';

describe('AddVideoCommentsToWorkspaceComponent', () => {
  let component: AddVideoCommentsToWorkspaceComponent;
  let fixture: ComponentFixture<AddVideoCommentsToWorkspaceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddVideoCommentsToWorkspaceComponent]
    });
    fixture = TestBed.createComponent(AddVideoCommentsToWorkspaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
