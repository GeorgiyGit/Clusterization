import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramLoadMultipleMessagesComponent } from './telegram-load-multiple-messages.component';

describe('TelegramLoadMultipleMessagesComponent', () => {
  let component: TelegramLoadMultipleMessagesComponent;
  let fixture: ComponentFixture<TelegramLoadMultipleMessagesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramLoadMultipleMessagesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramLoadMultipleMessagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
