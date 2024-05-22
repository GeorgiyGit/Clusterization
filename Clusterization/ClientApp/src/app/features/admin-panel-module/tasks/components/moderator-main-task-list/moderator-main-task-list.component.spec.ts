import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModeratorMainTaskListComponent } from './moderator-main-task-list.component';

describe('ModeratorMainTaskListComponent', () => {
  let component: ModeratorMainTaskListComponent;
  let fixture: ComponentFixture<ModeratorMainTaskListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModeratorMainTaskListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ModeratorMainTaskListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
