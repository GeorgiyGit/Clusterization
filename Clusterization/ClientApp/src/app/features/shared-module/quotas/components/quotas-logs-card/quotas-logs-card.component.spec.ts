import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuotasLogsCardComponent } from './quotas-logs-card.component';

describe('QuotasLogsCardComponent', () => {
  let component: QuotasLogsCardComponent;
  let fixture: ComponentFixture<QuotasLogsCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuotasLogsCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(QuotasLogsCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
