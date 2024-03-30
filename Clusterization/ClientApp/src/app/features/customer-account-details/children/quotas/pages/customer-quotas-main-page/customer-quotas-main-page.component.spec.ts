import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerQuotasMainPageComponent } from './customer-quotas-main-page.component';

describe('CustomerQuotasMainPageComponent', () => {
  let component: CustomerQuotasMainPageComponent;
  let fixture: ComponentFixture<CustomerQuotasMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerQuotasMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerQuotasMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
