import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskStatusesSelectComponent } from './task-statuses-select.component';

describe('TaskStatusesSelectComponent', () => {
  let component: TaskStatusesSelectComponent;
  let fixture: ComponentFixture<TaskStatusesSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TaskStatusesSelectComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TaskStatusesSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
