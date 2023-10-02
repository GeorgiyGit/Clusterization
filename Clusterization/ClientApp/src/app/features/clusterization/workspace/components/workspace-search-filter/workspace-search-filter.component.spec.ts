import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WorkspaceSearchFilterComponent } from './workspace-search-filter.component';

describe('WorkspaceSearchFilterComponent', () => {
  let component: WorkspaceSearchFilterComponent;
  let fixture: ComponentFixture<WorkspaceSearchFilterComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WorkspaceSearchFilterComponent]
    });
    fixture = TestBed.createComponent(WorkspaceSearchFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
