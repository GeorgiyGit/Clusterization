import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuotasTypesSelectComponent } from './quotas-types-select.component';

describe('QuotasTypesSelectComponent', () => {
  let component: QuotasTypesSelectComponent;
  let fixture: ComponentFixture<QuotasTypesSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuotasTypesSelectComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(QuotasTypesSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
