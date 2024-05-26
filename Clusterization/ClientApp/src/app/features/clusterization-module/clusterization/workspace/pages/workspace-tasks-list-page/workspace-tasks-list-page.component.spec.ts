import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkspaceTasksListPageComponent } from './workspace-tasks-list-page.component';

describe('WorkspaceTasksListPageComponent', () => {
  let component: WorkspaceTasksListPageComponent;
  let fixture: ComponentFixture<WorkspaceTasksListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WorkspaceTasksListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WorkspaceTasksListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
