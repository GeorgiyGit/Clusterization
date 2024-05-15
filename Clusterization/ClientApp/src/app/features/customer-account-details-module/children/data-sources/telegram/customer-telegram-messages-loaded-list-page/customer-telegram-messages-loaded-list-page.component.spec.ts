import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerTelegramMessagesLoadedListPageComponent } from './customer-telegram-messages-loaded-list-page.component';

describe('CustomerTelegramMessagesLoadedListPageComponent', () => {
  let component: CustomerTelegramMessagesLoadedListPageComponent;
  let fixture: ComponentFixture<CustomerTelegramMessagesLoadedListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerTelegramMessagesLoadedListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerTelegramMessagesLoadedListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
