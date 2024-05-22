import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubTaskCardComponent } from './sub-task-card.component';

describe('SubTaskCardComponent', () => {
  let component: SubTaskCardComponent;
  let fixture: ComponentFixture<SubTaskCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SubTaskCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SubTaskCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
