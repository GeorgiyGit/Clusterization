import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModeratorSubTaskCardComponent } from './moderator-sub-task-card.component';

describe('ModeratorSubTaskCardComponent', () => {
  let component: ModeratorSubTaskCardComponent;
  let fixture: ComponentFixture<ModeratorSubTaskCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModeratorSubTaskCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ModeratorSubTaskCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
