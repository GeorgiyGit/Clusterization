import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModeratorSubTaskListComponent } from './moderator-sub-task-list.component';

describe('ModeratorSubTaskListComponent', () => {
  let component: ModeratorSubTaskListComponent;
  let fixture: ComponentFixture<ModeratorSubTaskListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModeratorSubTaskListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ModeratorSubTaskListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
