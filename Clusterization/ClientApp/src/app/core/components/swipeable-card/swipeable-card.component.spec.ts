import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SwipeableCardComponent } from './swipeable-card.component';

describe('SwipeableCardComponent', () => {
  let component: SwipeableCardComponent;
  let fixture: ComponentFixture<SwipeableCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SwipeableCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SwipeableCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
