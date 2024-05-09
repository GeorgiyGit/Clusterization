import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocQuotasPricesPageComponent } from './doc-quotas-prices-page.component';

describe('DocQuotasPricesPageComponent', () => {
  let component: DocQuotasPricesPageComponent;
  let fixture: ComponentFixture<DocQuotasPricesPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocQuotasPricesPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocQuotasPricesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
