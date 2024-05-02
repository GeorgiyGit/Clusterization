import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuotasPackLogsCardComponent } from './quotas-pack-logs-card.component';

describe('QuotasPackLogsCardComponent', () => {
  let component: QuotasPackLogsCardComponent;
  let fixture: ComponentFixture<QuotasPackLogsCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuotasPackLogsCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(QuotasPackLogsCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
