import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramMessagePhoneCardComponent } from './telegram-message-phone-card.component';

describe('TelegramMessagePhoneCardComponent', () => {
  let component: TelegramMessagePhoneCardComponent;
  let fixture: ComponentFixture<TelegramMessagePhoneCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramMessagePhoneCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramMessagePhoneCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
