import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramChannelPhoneCardComponent } from './telegram-channel-phone-card.component';

describe('TelegramChannelPhoneCardComponent', () => {
  let component: TelegramChannelPhoneCardComponent;
  let fixture: ComponentFixture<TelegramChannelPhoneCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramChannelPhoneCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramChannelPhoneCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
