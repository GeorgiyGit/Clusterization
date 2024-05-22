import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModeratorMainTaskCardComponent } from './moderator-main-task-card.component';

describe('ModeratorMainTaskCardComponent', () => {
  let component: ModeratorMainTaskCardComponent;
  let fixture: ComponentFixture<ModeratorMainTaskCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModeratorMainTaskCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ModeratorMainTaskCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
