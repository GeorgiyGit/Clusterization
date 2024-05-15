import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerTelegramMainPageComponent } from './customer-telegram-main-page.component';

describe('CustomerTelegramMainPageComponent', () => {
  let component: CustomerTelegramMainPageComponent;
  let fixture: ComponentFixture<CustomerTelegramMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerTelegramMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerTelegramMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
