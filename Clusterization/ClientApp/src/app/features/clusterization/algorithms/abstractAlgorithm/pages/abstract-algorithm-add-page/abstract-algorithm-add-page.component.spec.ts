import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AbstractAlgorithmAddPageComponent } from './abstract-algorithm-add-page.component';

describe('AbstractAlgorithmAddPageComponent', () => {
  let component: AbstractAlgorithmAddPageComponent;
  let fixture: ComponentFixture<AbstractAlgorithmAddPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AbstractAlgorithmAddPageComponent]
    });
    fixture = TestBed.createComponent(AbstractAlgorithmAddPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
