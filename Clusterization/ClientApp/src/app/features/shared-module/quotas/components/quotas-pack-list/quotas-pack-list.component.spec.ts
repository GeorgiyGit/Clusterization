import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuotasPackListComponent } from './quotas-pack-list.component';

describe('QuotasPackListComponent', () => {
  let component: QuotasPackListComponent;
  let fixture: ComponentFixture<QuotasPackListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuotasPackListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(QuotasPackListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
