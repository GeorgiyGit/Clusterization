import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerPersonalInformationPageComponent } from './customer-personal-information-page.component';

describe('CustomerPersonalInformationPageComponent', () => {
  let component: CustomerPersonalInformationPageComponent;
  let fixture: ComponentFixture<CustomerPersonalInformationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerPersonalInformationPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerPersonalInformationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
