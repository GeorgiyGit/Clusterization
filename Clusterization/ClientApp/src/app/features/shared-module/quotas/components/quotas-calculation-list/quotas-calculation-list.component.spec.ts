import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuotasCalculationListComponent } from './quotas-calculation-list.component';

describe('QuotasCalculationListComponent', () => {
  let component: QuotasCalculationListComponent;
  let fixture: ComponentFixture<QuotasCalculationListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuotasCalculationListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(QuotasCalculationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
