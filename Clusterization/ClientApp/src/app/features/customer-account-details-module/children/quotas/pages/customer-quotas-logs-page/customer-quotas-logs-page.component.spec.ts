import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerQuotasLogsPageComponent } from './customer-quotas-logs-page.component';

describe('CustomerQuotasLogsPageComponent', () => {
  let component: CustomerQuotasLogsPageComponent;
  let fixture: ComponentFixture<CustomerQuotasLogsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerQuotasLogsPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerQuotasLogsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
