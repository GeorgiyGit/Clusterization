import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocCustomerQuotasPageComponent } from './doc-customer-quotas-page.component';

describe('DocCustomerQuotasPageComponent', () => {
  let component: DocCustomerQuotasPageComponent;
  let fixture: ComponentFixture<DocCustomerQuotasPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocCustomerQuotasPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocCustomerQuotasPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
