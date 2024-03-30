import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuotasPackCardComponent } from './quotas-pack-card.component';

describe('QuotasPackCardComponent', () => {
  let component: QuotasPackCardComponent;
  let fixture: ComponentFixture<QuotasPackCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuotasPackCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(QuotasPackCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
