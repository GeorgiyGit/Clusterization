import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocQuotasMainPageComponent } from './doc-quotas-main-page.component';

describe('DocQuotasMainPageComponent', () => {
  let component: DocQuotasMainPageComponent;
  let fixture: ComponentFixture<DocQuotasMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocQuotasMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocQuotasMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
