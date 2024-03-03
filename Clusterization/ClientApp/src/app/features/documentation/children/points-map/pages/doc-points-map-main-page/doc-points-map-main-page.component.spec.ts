import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocPointsMapMainPageComponent } from './doc-points-map-main-page.component';

describe('DocPointsMapMainPageComponent', () => {
  let component: DocPointsMapMainPageComponent;
  let fixture: ComponentFixture<DocPointsMapMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DocPointsMapMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DocPointsMapMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
