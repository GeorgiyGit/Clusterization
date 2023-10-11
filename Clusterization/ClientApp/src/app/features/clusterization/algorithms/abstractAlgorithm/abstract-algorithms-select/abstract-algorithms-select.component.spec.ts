import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AbstractAlgorithmsSelectComponent } from './abstract-algorithms-select.component';

describe('AbstractAlgorithmsSelectComponent', () => {
  let component: AbstractAlgorithmsSelectComponent;
  let fixture: ComponentFixture<AbstractAlgorithmsSelectComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AbstractAlgorithmsSelectComponent]
    });
    fixture = TestBed.createComponent(AbstractAlgorithmsSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
