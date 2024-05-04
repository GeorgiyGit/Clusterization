import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramMessageListComponent } from './telegram-message-list.component';

describe('TelegramMessageListComponent', () => {
  let component: TelegramMessageListComponent;
  let fixture: ComponentFixture<TelegramMessageListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramMessageListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramMessageListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
