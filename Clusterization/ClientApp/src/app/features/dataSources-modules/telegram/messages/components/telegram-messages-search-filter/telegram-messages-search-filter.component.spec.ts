import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TelegramMessagesSearchFilterComponent } from './telegram-messages-search-filter.component';

describe('TelegramMessagesSearchFilterComponent', () => {
  let component: TelegramMessagesSearchFilterComponent;
  let fixture: ComponentFixture<TelegramMessagesSearchFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TelegramMessagesSearchFilterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TelegramMessagesSearchFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
