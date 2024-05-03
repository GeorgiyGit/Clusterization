import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramLoadOneChannelComponent } from './telegram-load-one-channel.component';

describe('TelegramLoadOneChannelComponent', () => {
  let component: TelegramLoadOneChannelComponent;
  let fixture: ComponentFixture<TelegramLoadOneChannelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramLoadOneChannelComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramLoadOneChannelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
