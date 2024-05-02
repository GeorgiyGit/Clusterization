import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerQuotasPackLogsPageComponent } from './customer-quotas-pack-logs-page.component';

describe('CustomerQuotasPackLogsPageComponent', () => {
  let component: CustomerQuotasPackLogsPageComponent;
  let fixture: ComponentFixture<CustomerQuotasPackLogsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerQuotasPackLogsPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerQuotasPackLogsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
