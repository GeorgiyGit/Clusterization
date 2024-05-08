import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocQuotasLogsPageComponent } from './doc-quotas-logs-page.component';

describe('DocQuotasLogsPageComponent', () => {
  let component: DocQuotasLogsPageComponent;
  let fixture: ComponentFixture<DocQuotasLogsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocQuotasLogsPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocQuotasLogsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
