import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerTelegramChannelsLoadedListPageComponent } from './customer-telegram-channels-loaded-list-page.component';

describe('CustomerTelegramChannelsLoadedListPageComponent', () => {
  let component: CustomerTelegramChannelsLoadedListPageComponent;
  let fixture: ComponentFixture<CustomerTelegramChannelsLoadedListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerTelegramChannelsLoadedListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerTelegramChannelsLoadedListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
