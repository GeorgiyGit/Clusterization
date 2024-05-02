import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerListPageComponent } from './customer-list-page.component';

describe('CustomerListPageComponent', () => {
  let component: CustomerListPageComponent;
  let fixture: ComponentFixture<CustomerListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
