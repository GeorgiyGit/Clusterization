import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskListPageComponent } from './task-list-page.component';

describe('TaskListPageComponent', () => {
  let component: TaskListPageComponent;
  let fixture: ComponentFixture<TaskListPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TaskListPageComponent]
    });
    fixture = TestBed.createComponent(TaskListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
