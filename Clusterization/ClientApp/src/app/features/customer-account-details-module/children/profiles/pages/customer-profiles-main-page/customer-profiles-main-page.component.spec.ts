import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerProfilesMainPageComponent } from './customer-profiles-main-page.component';

describe('CustomerProfilesMainPageComponent', () => {
  let component: CustomerProfilesMainPageComponent;
  let fixture: ComponentFixture<CustomerProfilesMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerProfilesMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerProfilesMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
