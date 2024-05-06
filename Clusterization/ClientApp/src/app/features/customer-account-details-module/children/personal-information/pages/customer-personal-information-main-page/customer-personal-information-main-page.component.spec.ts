import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerPersonalInformationMainPageComponent } from './customer-personal-information-main-page.component';

describe('CustomerPersonalInformationMainPageComponent', () => {
  let component: CustomerPersonalInformationMainPageComponent;
  let fixture: ComponentFixture<CustomerPersonalInformationMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerPersonalInformationMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerPersonalInformationMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
