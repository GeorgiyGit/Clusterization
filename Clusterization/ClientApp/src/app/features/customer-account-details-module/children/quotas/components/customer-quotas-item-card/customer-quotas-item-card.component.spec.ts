import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerQuotasItemCardComponent } from './customer-quotas-item-card.component';

describe('CustomerQuotasItemCardComponent', () => {
  let component: CustomerQuotasItemCardComponent;
  let fixture: ComponentFixture<CustomerQuotasItemCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerQuotasItemCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerQuotasItemCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
