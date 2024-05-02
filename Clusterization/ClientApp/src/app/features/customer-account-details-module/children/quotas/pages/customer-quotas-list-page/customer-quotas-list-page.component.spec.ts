import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerQuotasListPageComponent } from './customer-quotas-list-page.component';

describe('CustomerQuotasListPageComponent', () => {
  let component: CustomerQuotasListPageComponent;
  let fixture: ComponentFixture<CustomerQuotasListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerQuotasListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerQuotasListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
