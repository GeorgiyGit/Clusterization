import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkspaceFullPageComponent } from './workspace-full-page.component';

describe('WorkspaceFullPageComponent', () => {
  let component: WorkspaceFullPageComponent;
  let fixture: ComponentFixture<WorkspaceFullPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WorkspaceFullPageComponent]
    });
    fixture = TestBed.createComponent(WorkspaceFullPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
