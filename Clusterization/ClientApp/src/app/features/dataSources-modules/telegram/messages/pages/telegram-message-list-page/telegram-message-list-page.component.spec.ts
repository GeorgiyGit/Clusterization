import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramMessageListPageComponent } from './telegram-message-list-page.component';

describe('TelegramMessageListPageComponent', () => {
  let component: TelegramMessageListPageComponent;
  let fixture: ComponentFixture<TelegramMessageListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramMessageListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramMessageListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
