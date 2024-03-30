import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerAccountDetailsNavPageComponent } from './customer-account-details-nav-page.component';

describe('CustomerAccountDetailsNavPageComponent', () => {
  let component: CustomerAccountDetailsNavPageComponent;
  let fixture: ComponentFixture<CustomerAccountDetailsNavPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerAccountDetailsNavPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerAccountDetailsNavPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
