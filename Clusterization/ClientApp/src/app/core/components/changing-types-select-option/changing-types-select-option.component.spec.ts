import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangingTypesSelectOptionComponent } from './changing-types-select-option.component';

describe('ChangingTypesSelectOptionComponent', () => {
  let component: ChangingTypesSelectOptionComponent;
  let fixture: ComponentFixture<ChangingTypesSelectOptionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChangingTypesSelectOptionComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ChangingTypesSelectOptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
