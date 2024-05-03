import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramChannelCardComponent } from './telegram-channel-card.component';

describe('TelegramChannelCardComponent', () => {
  let component: TelegramChannelCardComponent;
  let fixture: ComponentFixture<TelegramChannelCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramChannelCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramChannelCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
