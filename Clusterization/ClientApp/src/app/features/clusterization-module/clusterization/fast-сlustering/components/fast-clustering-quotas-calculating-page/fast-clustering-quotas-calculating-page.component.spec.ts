import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FastClusteringQuotasCalculatingPageComponent } from './fast-clustering-quotas-calculating-page.component';

describe('FastClusteringQuotasCalculatingPageComponent', () => {
  let component: FastClusteringQuotasCalculatingPageComponent;
  let fixture: ComponentFixture<FastClusteringQuotasCalculatingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FastClusteringQuotasCalculatingPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FastClusteringQuotasCalculatingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
