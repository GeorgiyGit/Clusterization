import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramMessageCardComponent } from './telegram-message-card.component';

describe('TelegramMessageCardComponent', () => {
  let component: TelegramMessageCardComponent;
  let fixture: ComponentFixture<TelegramMessageCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramMessageCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramMessageCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
