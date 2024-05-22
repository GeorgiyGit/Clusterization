import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModeratorTaskListPageComponent } from './moderator-task-list-page.component';

describe('ModeratorTaskListPageComponent', () => {
  let component: ModeratorTaskListPageComponent;
  let fixture: ComponentFixture<ModeratorTaskListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModeratorTaskListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ModeratorTaskListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
