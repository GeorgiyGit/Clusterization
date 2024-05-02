import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocPointsMapDynamicLoadingPageComponent } from './doc-points-map-dynamic-loading-page.component';

describe('DocPointsMapDynamicLoadingPageComponent', () => {
  let component: DocPointsMapDynamicLoadingPageComponent;
  let fixture: ComponentFixture<DocPointsMapDynamicLoadingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocPointsMapDynamicLoadingPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocPointsMapDynamicLoadingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
