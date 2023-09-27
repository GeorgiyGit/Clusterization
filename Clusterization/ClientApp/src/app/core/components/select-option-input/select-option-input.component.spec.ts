import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectOptionInputComponent } from './select-option-input.component';

describe('SelectOptionInputComponent', () => {
  let component: SelectOptionInputComponent;
  let fixture: ComponentFixture<SelectOptionInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelectOptionInputComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SelectOptionInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
