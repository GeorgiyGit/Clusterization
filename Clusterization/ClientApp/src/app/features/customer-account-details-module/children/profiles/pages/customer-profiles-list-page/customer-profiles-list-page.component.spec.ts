import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerProfilesListPageComponent } from './customer-profiles-list-page.component';

describe('CustomerProfilesListPageComponent', () => {
  let component: CustomerProfilesListPageComponent;
  let fixture: ComponentFixture<CustomerProfilesListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerProfilesListPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerProfilesListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
