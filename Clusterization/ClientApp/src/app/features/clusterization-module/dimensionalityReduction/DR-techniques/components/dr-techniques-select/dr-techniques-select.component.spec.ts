import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DRTechniquesSelectComponent } from './dr-techniques-select.component';

describe('DRTechniquesSelectComponent', () => {
  let component: DRTechniquesSelectComponent;
  let fixture: ComponentFixture<DRTechniquesSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DRTechniquesSelectComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DRTechniquesSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
