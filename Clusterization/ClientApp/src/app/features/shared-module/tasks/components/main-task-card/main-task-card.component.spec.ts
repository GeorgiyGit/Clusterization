import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainTaskCardComponent } from './main-task-card.component';

describe('MainTaskCardComponent', () => {
  let component: MainTaskCardComponent;
  let fixture: ComponentFixture<MainTaskCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MainTaskCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MainTaskCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
