import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocQuotasPacksPageComponent } from './doc-quotas-packs-page.component';

describe('DocQuotasPacksPageComponent', () => {
  let component: DocQuotasPacksPageComponent;
  let fixture: ComponentFixture<DocQuotasPacksPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocQuotasPacksPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocQuotasPacksPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
