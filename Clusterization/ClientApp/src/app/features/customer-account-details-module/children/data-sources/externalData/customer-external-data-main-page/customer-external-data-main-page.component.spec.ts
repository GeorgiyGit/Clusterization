import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerExternalDataMainPageComponent } from './customer-external-data-main-page.component';

describe('CustomerExternalDataMainPageComponent', () => {
  let component: CustomerExternalDataMainPageComponent;
  let fixture: ComponentFixture<CustomerExternalDataMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CustomerExternalDataMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerExternalDataMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
