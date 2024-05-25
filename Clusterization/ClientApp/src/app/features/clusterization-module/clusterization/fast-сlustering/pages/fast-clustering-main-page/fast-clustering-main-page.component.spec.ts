import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FastClusteringMainPageComponent } from './fast-clustering-main-page.component';

describe('FastClusteringMainPageComponent', () => {
  let component: FastClusteringMainPageComponent;
  let fixture: ComponentFixture<FastClusteringMainPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FastClusteringMainPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FastClusteringMainPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
