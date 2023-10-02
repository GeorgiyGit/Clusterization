import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkspaceListPageComponent } from './workspace-list-page.component';

describe('WorkspaceListPageComponent', () => {
  let component: WorkspaceListPageComponent;
  let fixture: ComponentFixture<WorkspaceListPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WorkspaceListPageComponent]
    });
    fixture = TestBed.createComponent(WorkspaceListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
