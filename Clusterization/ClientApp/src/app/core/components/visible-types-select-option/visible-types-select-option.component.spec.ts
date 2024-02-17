import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VisibleTypesSelectOptionComponent } from './visible-types-select-option.component';

describe('VisibleTypesSelectOptionComponent', () => {
  let component: VisibleTypesSelectOptionComponent;
  let fixture: ComponentFixture<VisibleTypesSelectOptionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VisibleTypesSelectOptionComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VisibleTypesSelectOptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
