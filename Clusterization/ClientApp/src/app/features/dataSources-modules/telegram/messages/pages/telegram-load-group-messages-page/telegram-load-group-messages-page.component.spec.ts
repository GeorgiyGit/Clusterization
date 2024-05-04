import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramLoadGroupMessagesPageComponent } from './telegram-load-group-messages-page.component';

describe('TelegramLoadGroupMessagesPageComponent', () => {
  let component: TelegramLoadGroupMessagesPageComponent;
  let fixture: ComponentFixture<TelegramLoadGroupMessagesPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramLoadGroupMessagesPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramLoadGroupMessagesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
